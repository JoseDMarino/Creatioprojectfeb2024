namespace CrtDigitalAdsApp.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.Logging;
	using CrtDigitalAdsApp.Models.Requests;
	using CrtDigitalAdsApp.Models.Responses;
	using CrtDigitalAdsApp.Providers;
	using CrtDigitalAdsApp.Repositories;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	public class DailyInsightProvider
	{

		#region Constants: Private

		private const string Application = "digital_ads";
		private const int ShiftDaysToSync = 365;

		#endregion

		#region Fields: Private

		private IDigitalAdsAdAccountsProvider _digitalAdsAdAccountsProvider;
		private ILog _logger;
		private IDailyInsightRepository _repository;
		private readonly UserConnection _userConnection;
		private AdPlatformRepository _adPlatformRepository;

		#endregion

		#region Constructors: Public

		public DailyInsightProvider() {
			_userConnection = ClassFactory.Get<UserConnection>();
		}

		#endregion

		#region Properties: Private

		private AdPlatformRepository PlatformProvider =>
			_adPlatformRepository ?? (_adPlatformRepository = new AdPlatformRepository());

		#endregion

		#region Properties: Internal

		internal IDigitalAdsAdAccountsProvider DigitalAdsAdAccountsProvider {
			get =>
				_digitalAdsAdAccountsProvider ?? (_digitalAdsAdAccountsProvider =
					ClassFactory.Get<IDigitalAdsAdAccountsProvider>(new ConstructorArgument("userConnection",
						_userConnection)));
			set => _digitalAdsAdAccountsProvider = value;
		}

		internal ILog Logger {
			get =>
				_logger ?? (_logger =
					LogManager.GetLogger("GetDailyInsightsUserTask"));
			set => _logger = value;
		}

		internal IDailyInsightRepository Repository {
			get => _repository ?? (_repository = new DailyInsightRepository(_userConnection));
			set => _repository = value;
		}

		#endregion

		#region Methods: Private

		private (DateTime dateFrom, DateTime dateTo) GetNextSyncDateRange(DateTime lastSyncDate) {
			return lastSyncDate == default ? (DateTime.UtcNow.AddDays(-ShiftDaysToSync), DateTime.UtcNow)
				: (DateTime.UtcNow.AddDays(-60), DateTime.UtcNow);
		}

		private string GetPlatformAdAccountId(Guid adAccountId) {
			var esqResult = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "AdAccount") {
				PrimaryQueryColumn = { IsVisible = true }
			};
			esqResult.AddColumn("AccountId");
			esqResult.AddColumn("Name");
			var filterById =
				esqResult.CreateFilterWithParameters(FilterComparisonType.Equal, "Id", adAccountId);
			esqResult.Filters.Add(filterById);
			var entities = esqResult.GetEntityCollection(_userConnection);
			return entities.First().GetTypedColumnValue<string>("AccountId");
		}

		private void SyncCampaignDailyInsights(Entity campaign) {
			var campaignId = campaign.GetTypedColumnValue<Guid>("Id");
			var adCampaignId = campaign.GetTypedColumnValue<string>("AdCampaignId");
			var lastSyncDate = Repository.GetLastSyncDate(campaignId);
			var (dateFrom, dateTo) = GetNextSyncDateRange(lastSyncDate);
			var adCampaignsInsightRequest = new GetAdCampaignsDailyInsightsRequest {
				AdCampaignId = adCampaignId,
				PlatformName =
					PlatformProvider.GetPlatformName(campaign.GetTypedColumnValue<Guid>("PlatformId"), _userConnection),
				Application = Application,
				DateFrom = dateFrom,
				DateTo = dateTo,
				AdAccountId = GetPlatformAdAccountId(campaign.GetTypedColumnValue<Guid>("AdAccountId"))
			};
			var dailyInsights = DigitalAdsAdAccountsProvider.GetCampaignsDailyInsights(adCampaignsInsightRequest)
				.Result;
			UpdateDailyInsights(dailyInsights, campaignId);
		}

		private void UpdateDailyInsights(IEnumerable<AdCampaignDailyInsightsResponse> dailyInsights,
			Guid campaignId) {
			foreach (var dailyInsight in dailyInsights) {
				if (Repository.TryGetIsDailyInsight(campaignId, dailyInsight, out var entity)) {
					Repository.SavesDailyInsightData(dailyInsight, campaignId, entity);
				} else {
					Repository.InsertDailyInsight(dailyInsight, campaignId);
				}
			}
		}

		#endregion

		#region Methods: Internal

		internal bool Actualize() {
			var campaigns = Repository.GetAdCampaigns();
			foreach (var campaign in campaigns) {
				try {
					SyncCampaignDailyInsights(campaign);
				} catch (Exception e) {
					Logger.Warn($"Failed to process daily insights. Exception: {e}");
				}
			}
			var notifier = ClassFactory.Get<IUINotifier>();
			notifier.Notify($"refresh.all", "Data was actualized");
            return true;
		}

		#endregion

	}
}