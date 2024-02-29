namespace CrtDigitalAdsApp.Services
{
	using System;
	using System.Collections.Generic;
	using Common.Logging;
	using CrtDigitalAdsApp.Models.Requests;
	using CrtDigitalAdsApp.Models.Responses;
	using CrtDigitalAdsApp.Providers;
	using CrtDigitalAdsApp.Repositories;
	using CrtDigitalAdsApp.Utilities;
	using CrtDigitalAdsApp.Utilities.Errors;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// Represents the interface for the handling callback request. 
	/// </summary>
	public interface IAuthServiceCallbackHandlerWorker
	{

		#region Methods: Public		
		/// <summary>
		/// Handles callback request for the specified platform.
		/// </summary>
		/// <param name="platform">The platform.</param>
		/// <param name="platformUserId">The platform user identifier.</param>
		/// <param name="application">The application.</param>
		void Handle(string platform, string platformUserId, string application);

		#endregion

	}

	[DefaultBinding(typeof(IAuthServiceCallbackHandlerWorker))]
	public class AuthServiceCallbackHandlerWorker : IAuthServiceCallbackHandlerWorker
	{

		#region Fields: Private

		private static readonly ILog Logger = LogManager.GetLogger("AuthServiceCallbackHandler");
		private readonly UserConnection _userConnection;
		private IDigitalAdsAdAccountsProvider _digitalAdsAdAccountsProvider;
		private readonly IUINotifier _uiNotifier;
		private readonly IAdAccountRepository _adAccountRepository;

		#endregion

		#region Constructors: Public		
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthServiceCallbackHandlerWorker"/> class.
		/// </summary>
		public AuthServiceCallbackHandlerWorker() {
			_userConnection = ClassFactory.Get<UserConnection>();
			_uiNotifier = ClassFactory.Get<IUINotifier>();
			_adAccountRepository = ClassFactory.Get<IAdAccountRepository>(new[] {
				new ConstructorArgument("userConnection", _userConnection), new ConstructorArgument("logger", Logger)
			});
		}

		#endregion

		#region Properties: Public		
		/// <summary>
		/// Gets or sets the digital ads ad accounts provider.
		/// </summary>
		/// <value>
		/// The digital ads ad accounts provider.
		/// </value>
		public IDigitalAdsAdAccountsProvider DigitalAdsAdAccountsProvider {
			get =>
				_digitalAdsAdAccountsProvider ??
				(_digitalAdsAdAccountsProvider = new DigitalAdsAdAccountsProvider(_userConnection));
			set => _digitalAdsAdAccountsProvider = value;
		}

		#endregion

		#region Methods: Private

		private void RunSyncProcess() {
			_userConnection.ProcessEngine.ProcessExecutor.Execute("SynchronizeAdCampaignData");
		}

		private void SaveAdAccounts(IEnumerable<AdAccountResponse> adAccounts, string platform) {
			var isAnyAdded = _adAccountRepository.SaveAdAccounts(adAccounts, platform);
			if (isAnyAdded) {
				_uiNotifier.Notify($"refresh.adAccount.{platform}",
					"Account added successfully");
			}
		}

		#endregion

		#region Methods: Public		
		/// <summary>
		/// Handles callback request for the specified platform.
		/// </summary>
		/// <param name="platform">The platform.</param>
		/// <param name="platformUserId">The platform user identifier.</param>
		/// <param name="application">The application.</param>
		public void Handle(string platform, string platformUserId, string application) {
			List<AdAccountResponse> adAccounts;
			try {
				adAccounts = DigitalAdsAdAccountsProvider.GetAdAccounts(new GetAdAccountsRequest {
					PlatformName = platform,
					PlatformUserId = platformUserId,
					Application = application
				}).Result;
			} catch (Exception e) {
				Logger.Error("AuthServiceCallbackHandlerWorker Handle failed. Exception:", e);
				var error = new CouldNotGetAdAccountError();
				_uiNotifier.ReportError(error);
				return;
			}
			SaveAdAccounts(adAccounts, platform);
			RunSyncProcess();
		}

		#endregion

	}
}