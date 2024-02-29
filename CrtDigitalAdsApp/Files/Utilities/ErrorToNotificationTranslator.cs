namespace CrtDigitalAdsApp.Utilities
{
	using System;
	using CrtDigitalAdsApp.Dto;
	using CrtDigitalAdsApp.Utilities.Errors;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// Implements the translator from errors to user friendly messages.
	/// </summary>
	internal class ErrorToNotificationTranslator
	{

		#region Constants: Private

		private const string ResourceManagerName = "Translations";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorToNotificationTranslator"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		public ErrorToNotificationTranslator(UserConnection userConnection = null) {
			_userConnection = userConnection ?? ClassFactory.Get<UserConnection>();
		}

		#endregion

		#region Methods: Private

		private string GetLocalizableValue(string code, string lczKey) {
			string lczString = new LocalizableString(
				_userConnection.Workspace.ResourceStorage,
				ResourceManagerName, $"LocalizableStrings.{lczKey}.Value");
			return string.Format(lczString ?? "Localizable value missing for {0}", code);
		}

		private WebSocketDto TranslateGenericError(GenericError error) {
			var webSocketDto = new WebSocketDto {
				IsSuccess = false,
				Command = "show.ErrorScreen",
				Description = error.Description,
				ErrorCode = error.ErrorCode
			};
			return webSocketDto;
		}

		private WebSocketDto TranslateKnownError(KnownError error) {
			switch (error) {
				case PlatformServicesUnavailable platformServicesUnavailable:
					return new WebSocketDto {
						IsSuccess = false,
						Command = "show.ErrorScreen",
						Description = GetLocalizableValue(nameof(PlatformServicesUnavailable),
							platformServicesUnavailable.DescriptionLcz),
						ErrorCode = string.Empty
					};
				case CouldNotGetAdAccountError couldNotGetAdAccountError:
					return new WebSocketDto {
						IsSuccess = false,
						Command = "show.ErrorScreen",
						Description = GetLocalizableValue(nameof(CouldNotGetAdAccountError),
							couldNotGetAdAccountError.DescriptionLcz),
						ErrorCode = string.Empty
					};
				case CouldNotAuthenticateToPlatformError couldNotGetAdAccountError:
					return new WebSocketDto {
						IsSuccess = false,
						Command = "show.ErrorScreen",
						Description = GetLocalizableValue(nameof(CouldNotAuthenticateToPlatformError),
							couldNotGetAdAccountError.DescriptionLcz),
						ErrorCode = string.Empty
					};
				case AccessTokenDoesNotHaveRequiredScopes couldNotGetAdAccountError:
					return new WebSocketDto {
						IsSuccess = false,
						Command = "show.ErrorScreen",
						Description = GetLocalizableValue(nameof(AccessTokenDoesNotHaveRequiredScopes),
							$"{nameof(AccessTokenDoesNotHaveRequiredScopes)}_Description"),
						ErrorCode = string.Empty
					};

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private WebSocketDto TranslateSysSettingError(SysSettingError error) {
			var titleLcz = $"SysSetting_{error.SysSettingGroup}InvalidValue_Title";
			var descriptionLcz = $"SysSetting_{error.SysSettingGroup}InvalidValue_Description";
			var webSocketDto = new WebSocketDto {
				IsSuccess = false,
				Command = "show.ErrorScreen",
				Description = GetLocalizableValue(error.SysSettingCode,
					descriptionLcz),
				ErrorCode = string.Empty
			};
			return webSocketDto;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Translates the error to <see cref="WebSocketDto"/>.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns><see cref="WebSocketDto"/></returns>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		public WebSocketDto TranslateError(IError error) {
			switch (error) {
				case SysSettingError sysSettingError:
					return TranslateSysSettingError(sysSettingError);
				case KnownError knownError:
					return TranslateKnownError(knownError);
				case GenericError genericError:
					return TranslateGenericError(genericError);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion

	}
}