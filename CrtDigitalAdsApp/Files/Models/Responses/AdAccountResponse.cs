namespace CrtDigitalAdsApp.Models.Responses
{
	using Newtonsoft.Json;

	/// <summary>
	/// Model of response, which contains properties of
	/// ad account which was retrieved from ad platform
	/// </summary>
	public class AdAccountResponse
	{

		#region Properties: Public

		/// <summary>
		/// Ad account url
		/// </summary>
		[JsonProperty("adAccountUrl")]
		public string AdAccountUrl { get; set; }

		/// <summary>
		/// Name of ad account
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Ad account identifier
		/// </summary>
		[JsonProperty("platformAdAccountId")]
		public string PlatformAdAccountId { get; set; }

		/// <summary>
		/// Platform name
		/// </summary>
		[JsonProperty("platformName")]
		public string PlatformName { get; set; }

		#endregion

	}
}