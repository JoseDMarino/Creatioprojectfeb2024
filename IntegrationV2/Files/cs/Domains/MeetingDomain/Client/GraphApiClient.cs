namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Client
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.IO;
	using System.Net;
	using Newtonsoft.Json.Linq;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Logger;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core.Factories;
	using Terrasoft.Social.OAuth;
	using System.Web;

	#region Class: GraphApiClient
	/// <summary>
	/// PoC implementation of graph API usage.
	/// </summary>
	public class GraphApiClient
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="OAuthAppSettings"/> instance.
		/// </summary
		private readonly OAuthAppSettings _oauthApplicationSettings;

		/// <summary>
		/// Current office 365 user name.
		/// </summary>
		private readonly string _userName;

		/// <summary>
		/// <see cref="ICalendarLogger"/> implementation instance.
		/// </summary
		private readonly ICalendarLogger _log;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctror.
		/// </summary>
		/// <param name="calendar"><see cref="CalendarSettings"/> instance.</param>
		/// <param name="log"><see cref="ICalendarLogger"/> implementation instance.</param>
		public GraphApiClient(CalendarSettings calendarSettings, ICalendarLogger log) {
			_oauthApplicationSettings = calendarSettings.OauthApplicationSettings;
			_userName = calendarSettings.Login;
			_log = log;
			ValidateInstance();
		}

		#endregion

		#region Methods: Private

		private void ValidateInstance() {
			if (_oauthApplicationSettings != null
					&& _oauthApplicationSettings.ClientId.IsNotNullOrEmpty()
					&& _oauthApplicationSettings.ClientSecret.IsNotNullOrEmpty()
					&& _oauthApplicationSettings.TenantId.IsNotNullOrEmpty()
					&& _userName.IsNotNullOrEmpty()) {
				return;
			}
			throw new InvalidObjectStateException("OAuth parameters are not filled for Graph API. " +
				"Check OAuthApplication ClientId, SecretKey and TenantId columns.");
		}

		private async Task<string> SendRequest(string serviceUri, string method, string contentType, byte[] data,
				string token = null) {
			var requestFactory = ClassFactory.Get<IHttpWebRequestFactory>();
			WebRequest request = requestFactory.Create(serviceUri);
			request.Method = method;
			request.ContentType = contentType;
			if (token != null) {
				request.Headers.Add("Authorization", $"Bearer {token}");
			}
			request.Timeout = 5 * 60 * 1000;
			request.ContentLength = data.Length;
			WebResponse response = null;
			try {
				using (Stream stream = request.GetRequestStream()) {
					stream.Write(data, 0, data.Length);
				}
				response = request.GetResponse();
				using (Stream dataStream = response.GetResponseStream()) {
					StreamReader reader = new StreamReader(dataStream);
					return await reader.ReadToEndAsync();
				}
			} catch (Exception ex) {
				var logMessage = $"Error calling {serviceUri} (method {method}).";
				var responseContent = ex.GetExceptionContent();
				if (responseContent.IsNotNullOrEmpty()) {
					logMessage += $"\r\nResponse content '{responseContent}'";
				}
				_log?.LogError(logMessage, ex);
				throw;
			}
			finally {
				response?.Close();
			}
		}

		private async Task<string> GetToken() {
			var serviceUri = $"https://login.microsoftonline.com/{_oauthApplicationSettings.TenantId}/oauth2/v2.0/token";
			var collection = new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("grant_type", "client_credentials"),
				new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
				new KeyValuePair<string, string>("client_id", _oauthApplicationSettings.ClientId),
				new KeyValuePair<string, string>("client_secret", _oauthApplicationSettings.ClientSecret),
			};
			var content = new FormUrlEncodedContent(collection);
			var data = await content.ReadAsByteArrayAsync();
			var result = await SendRequest(serviceUri, "POST", "application/x-www-form-urlencoded", data);
			JObject resultObj = Json.Deserialize(result) as JObject;
			return resultObj.Value<string>("access_token");
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Converts meeting with <paramref name="meetingId"/> to Teems meeting in current user calendar.
		/// </summary>
		/// <param name="meetingId">Exchange appointment unique identifier.</param>
		/// <returns>Updated meeting in JSON format.</returns>
		public async Task<string> ConvertToTeamsMeeting(string meetingId) {
			string token = await GetToken();
			var serviceUri = $"https://graph.microsoft.com/v1.0/users/{_userName}/calendar/events/" +
				$"{HttpUtility.UrlEncode(HttpUtility.UrlEncode(meetingId))}";
			var data = Encoding.UTF8.GetBytes("{\"isOnlineMeeting\": true}");
			return await SendRequest(serviceUri, "PATCH", "application/json; charset=utf-8", data, token);
		}

		#endregion

	}

	#endregion

}
