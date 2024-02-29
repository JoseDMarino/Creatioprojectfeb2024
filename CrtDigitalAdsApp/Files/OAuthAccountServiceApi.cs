namespace CrtDigitalAdsApp
{
	using CrtDigitalAdsApp.Models.Responses;
	using CrtDigitalAdsApp.Models.Requests;
	using Terrasoft.Configuration;
	using Terrasoft.Core;

	#region Class: OAuthAccountServiceApi

	/// <inheritdoc cref="IOAuthAccountServiceApi"/>
	public class OAuthAccountServiceApi : AccountServiceApi, IOAuthAccountServiceApi
	{

		#region Constructors: Public

		/// <summary>
		/// Constructor of the class.
		/// </summary>
		/// <param name="userConnection">Instance of UserConnection.</param>
		/// <param name="scope">Identity scope.</param>
		public OAuthAccountServiceApi(UserConnection userConnection, string scope)
			: base(userConnection, scope) {
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IOAuthAccountServiceApi.StartFlow"/>
		public StartOAuthFlowResponse StartFlow(StartFlowRequest request) {
			string url = $"{AccountServiceUrl}/api/OAuth/startFlow";
			StartOAuthFlowResponse response = SendPostRequest<StartOAuthFlowResponse>(url, request);
			return response;
		}

		#endregion

	}

	#endregion
}