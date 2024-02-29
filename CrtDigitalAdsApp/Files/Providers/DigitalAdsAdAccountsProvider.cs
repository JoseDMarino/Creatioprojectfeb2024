namespace CrtDigitalAdsApp.Providers
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using Common.Logging;
	using CrtDigitalAdsApp.Models.Requests;
	using CrtDigitalAdsApp.Models.Responses;
	using Newtonsoft.Json;
	using Terrasoft.Configuration;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process.Configuration;
	using Terrasoft.OAuthIntegration;
	using SysSettings = Terrasoft.Core.Configuration.SysSettings;

	/// <inheritdoc cref="IDigitalAdsAdAccountsProvider"/>
	[DefaultBinding(typeof(IDigitalAdsAdAccountsProvider))]
	public class DigitalAdsAdAccountsProvider : IDigitalAdsAdAccountsProvider
	{

		#region Constants: Private

		private const string GetAdAccountsUrl = "api/digitalads/getAdAccounts";
		private const string GetCampaignDailyInsights = "api/digitalads/getCampaignDailyInsights";
		private const string GetCampaignInsights = "api/digitalads/getCampaignInsights";
		private const string GetCampaignsInfosUrl = "api/digitalads/getCampaignsInfos";
		private const string Scopes = "digital_ads.service.full_access";
		private const string SmPlatformUrlSysSettingCode = "SocialPlatformServiceUrl";

		#endregion

		#region Fields: Private

		private HttpClient _httpClient;
		private ILog _logger;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Creates instance of <see cref="DigitalAdsAdAccountsProvider"/>
		/// </summary>
		/// <param name="userConnection">User connection</param>
		public DigitalAdsAdAccountsProvider(UserConnection userConnection) {
			var platformServiceUrl = SysSettings.GetValue(userConnection, SmPlatformUrlSysSettingCode);
			var baseAddress = new Uri(platformServiceUrl.ToString());
			HttpClient = new HttpClient {
				BaseAddress = baseAddress
			};
		}

		#endregion

		#region Properties: Private

		private ILog Logger => _logger ?? (_logger = LogManager.GetLogger(nameof(DigitalAdsAdAccountsProvider)));

		#endregion

		#region Properties: Public

		/// <summary>
		/// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
		/// </summary>
		public HttpClient HttpClient {
			get => _httpClient ?? (_httpClient = new HttpClient());
			set => _httpClient = value;
		}

		#endregion

		#region Methods: Private

		private static string GetAccessToken() {
			var identityServiceWrapper = GlobalAppSettings.FeatureUseSeparateSettingsForOAuth20
				? ClassFactory.Get<IIdentityServiceWrapper>("ExternalAccess")
				: ClassFactory.Get<IIdentityServiceWrapper>();
			var token = identityServiceWrapper.GetAccessToken(Scopes);
			return token;
		}

		private static HttpRequestMessage GetHttpRequestMessage(BaseDigitalAdsRequest request, string requestUrl) {
			var token = GetAccessToken();
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
			httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(request));
			httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			return httpRequestMessage;
		}

		private async Task<string> SendRequest(HttpRequestMessage httpRequestMessage) {
			HttpResponseMessage response = null;
			try {
				response = await HttpClient.SendAsync(httpRequestMessage);
				response.EnsureSuccessStatusCode();
				string content = await response.Content.ReadAsStringAsync();
				return content;
			} catch (Exception e) {
				Logger.Warn(
					"DigitalAdsAdAccountsProvider. Send request failed. " +
					$"RequestUri: {httpRequestMessage.RequestUri}, ResponseStatusCode: {response?.StatusCode}, " +
					$"ResponseContent: {response?.Content}, Exception: {e}");
				return string.Empty;
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IDigitalAdsAdAccountsProvider.GetAdAccounts"/>
		public async Task<List<AdAccountResponse>> GetAdAccounts(GetAdAccountsRequest getAdAccountsRequest) {
			var httpRequestMessage = GetHttpRequestMessage(getAdAccountsRequest, GetAdAccountsUrl);
			var content = await SendRequest(httpRequestMessage);
			var adAccounts = JsonConvert.DeserializeObject<List<AdAccountResponse>>(content);
			return adAccounts ?? new List<AdAccountResponse>();
		}

		/// <inheritdoc cref="IDigitalAdsAdAccountsProvider.GetCampaignsDailyInsights"/>
		public async Task<IEnumerable<AdCampaignDailyInsightsResponse>> GetCampaignsDailyInsights(
			GetAdCampaignsDailyInsightsRequest campaignsDailyInsightRequest) {
			var httpRequestMessage = GetHttpRequestMessage(campaignsDailyInsightRequest, GetCampaignDailyInsights);
			var content = await SendRequest(httpRequestMessage);
			var result = JsonConvert.DeserializeObject<IEnumerable<AdCampaignDailyInsightsResponse>>(content);
			return result ?? new List<AdCampaignDailyInsightsResponse>();
		}

		/// <inheritdoc cref="IDigitalAdsAdAccountsProvider.GetCampaignsInfos"/>
		public async Task<List<AdCampaignInfoResponse>> GetCampaignsInfos(
			GetAdCampaignsInfosRequest getAdCampaignsInfosRequest) {
			var httpRequestMessage = GetHttpRequestMessage(getAdCampaignsInfosRequest, GetCampaignsInfosUrl);
			var content = await SendRequest(httpRequestMessage);
			var adCampaignInfo = JsonConvert.DeserializeObject<List<AdCampaignInfoResponse>>(content);
			return adCampaignInfo ?? new List<AdCampaignInfoResponse>();
		}

		/// <inheritdoc cref="IDigitalAdsAdAccountsProvider.GetCampaignsInsight"/>
		public async Task<AdCampaignInsightsResponse> GetCampaignsInsight(
			GetAdCampaignsInsightsRequest getAdCampaignsInsightsRequest) {
			var httpRequestMessage = GetHttpRequestMessage(getAdCampaignsInsightsRequest, GetCampaignInsights);
			var content = await SendRequest(httpRequestMessage);
			var adCampaignInsights = JsonConvert.DeserializeObject<AdCampaignInsightsResponse>(content);
			return adCampaignInsights ?? new AdCampaignInsightsResponse();
		}

		#endregion

	}
}