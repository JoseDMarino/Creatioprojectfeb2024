namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{
	using System;

	[Serializable]
	public class OAuthAppSettings
	{

		public string TenantId { get; set; }

		public string ClientId { get; set; }

		public string ClientSecret { get; set; }
	}
}
