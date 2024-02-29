namespace Terrasoft.Configuration.SsoSettings
{
	using System;
	using System.Linq;
	using Terrasoft.ComponentSpace.Interfaces;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Entities.Events;
	using Terrasoft.Web.Common;
	using Terrasoft.Web.Http.Abstractions;

	#region Class: SsoSamlProviderEventListener

	[EntityEventListener(SchemaName = "SsoSamlProvider")]
	public class SsoSamlProviderEventListener: BaseSsoVirtualSettingsEventListener
	{

		#region Constructors: Public

		public SsoSamlProviderEventListener() : base(settingsSchemaName: "SsoSamlSettings") {}

		#endregion

		#region Method: Private

		private string GetBaseApplicationUrl() {
			return WebUtilities.GetBaseApplicationUrl(HttpContext.Current.Request)
				.Replace("/0", string.Empty);
		}

		#endregion

		#region Methods: Public

		public override void OnSaved(object sender, EntityAfterEventArgs e) {
			base.OnSaved(sender, e);
			var entity = (Entity) sender;
			var schema = entity.EntitySchemaManager.GetInstanceByName("SsoServiceProvider");
			var esq = new EntitySchemaQuery(schema) {
				PrimaryQueryColumn = {
					IsAlwaysSelect = true
				}
			};
			var serviceProvider = esq.GetEntityCollection(entity.UserConnection).FirstOrDefault();
			if (serviceProvider?.PrimaryColumnValue != null) {
				return;
			}
			serviceProvider = schema.CreateEntity(entity.UserConnection);
			var baseUrl = GetBaseApplicationUrl();
			var ssoLoginEndpoint = new Uri(baseUrl + SamlConsts.AssertionConsumerServicePath);
			var ssoLogoutEndpoint = new Uri(baseUrl + SamlConsts.SingleLogoutServicePath);
			serviceProvider.SetDefColumnValues();
			serviceProvider.SetColumnValue("AssertionConsumerServiceUrl", ssoLoginEndpoint.ToString());
			serviceProvider.SetColumnValue("SingleLogoutServiceUrl", ssoLogoutEndpoint.ToString());
			serviceProvider.SetColumnValue("Name", baseUrl);
			serviceProvider.Save();
		}

		#endregion

	}

	#endregion

}
 
