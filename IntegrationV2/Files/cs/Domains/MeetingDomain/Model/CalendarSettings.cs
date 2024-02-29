namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Model
{
	using System;
	using IntegrationApi.Interfaces;
	using IntegrationApi.MailboxDomain.Exceptions;
	using IntegrationApi.Utils;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: CalendarSettings

	/// <summary>
	/// Calendar settings domain model.
	/// </summary>
	[Serializable]
	public class CalendarSettings
	{

		#region Fields: Private

		private readonly string _oauthClassName;

		#endregion

		#region Properties: Public

		public Guid Id { get; set; }

		public string SenderEmailAddress { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Guid UserId { get; set; }

		public string UserName { get; set; }

		public string ServiceUrl { get; set; }

		public Guid OAuthApplicationId { get; set; }

		public string AccessToken { get; private set; }

		public bool SyncEnabled { get; set; }

		public bool UseOAuth {
			get {
				return OAuthApplicationId != Guid.Empty;
			}
		}

		public Guid WarningCodeId { get; set; }

		public OAuthAppSettings OauthApplicationSettings { get; set; }

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="oauthClassName">Oauth class name.</param>
		public CalendarSettings(string oauthClassName = null) {
			_oauthClassName = oauthClassName;
		}

		#endregion

		#region Methods: Private

		private string DecryptSecretKey(string encryptValue, UserConnection uc) {
			var secureTextDataValueType = uc.DataValueTypeManager.GetInstanceByName("SecureText");
			return (string)secureTextDataValueType.GetValueForLoad(uc, encryptValue);
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Refresh calendar access token.
		/// </summary>
		/// <param name="oauthClassName"><see cref="UserConnection"/> instance.</param>
		/// <exception cref="ArgumentException">When <paramref name="uc"/>, <see cref="UseOAuth"/> 
		/// or <see cref="SenderEmailAddress"/> has invalid states.</exception>
		public void RefreshAccessToken(UserConnection uc) {
			if (!UseOAuth) {
				return;
			}
			if (string.IsNullOrEmpty(_oauthClassName) || uc == null || string.IsNullOrEmpty(SenderEmailAddress)) {
				throw new ArgumentException("Failed refresh calendar token, invalid arguments states.");
			}
			try {
				AccessToken = OAuthUtils.RefreshAccessToken(_oauthClassName, SenderEmailAddress, uc);
			}
			catch (Exception e) {
				var ex = e is AggregateException ? e.InnerException : e;
				ex = UseOAuth ? (Exception)new OAuthSyncException(ex) : new BasicAuthSyncException(ex);
				var syncErrorHelper = ClassFactory.Get<ISynchronizationErrorHelper>(new ConstructorArgument("userConnection", uc));
				syncErrorHelper.ProcessSynchronizationError(SenderEmailAddress, ex);
				throw;
			}
		}

		/// <inheritdoc cref="object.ToString"/>
		public override string ToString() {
			return $"[Calendar settings => \"{Id}\" \"{ServiceUrl}\" \"{(UseOAuth ? "Oauth" : "Login")}\"]";
		}
		/// <summary>
		/// Temporary implementation without core change. 
		/// Todo move to OAuthUtils.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		public void LoadOauthAppParams(UserConnection uc) {
			if (!UseOAuth) {
				return;
			}
			var appSchema = uc.EntitySchemaManager.GetInstanceByName("OAuthApplications");
			Entity application = appSchema.CreateEntity(uc);
			if (application.FetchFromDB(OAuthApplicationId)) {
				OauthApplicationSettings = new OAuthAppSettings() {
					ClientId = application.GetTypedColumnValue<string>("ClientId"),
					ClientSecret = application.GetTypedColumnValue<string>("SecretKey"),
					TenantId = application.GetTypedColumnValue<string>("TenantId"),
				};
			}
		}

		#endregion
	}

	#endregion

}
