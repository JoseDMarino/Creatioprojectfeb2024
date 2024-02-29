namespace CrtDigitalAdsApp.Repositories
{
	using System;
	using System.Collections.Generic;
	using Common.Logging;
	using CrtDigitalAdsApp.Models.Responses;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	/// <summary>
	/// Represent the interface for the saving ad accounts.
	/// </summary>
	public interface IAdAccountRepository
	{

		#region Methods: Public		
		/// <summary>
		/// Saves the ad accounts.
		/// </summary>
		/// <param name="adAccounts">The ad accounts.</param>
		/// <param name="platform">The platform.</param>
		/// <returns>True when at least one account added.</returns>
		bool SaveAdAccounts(IEnumerable<AdAccountResponse> adAccounts, string platform);

		#endregion

	}

	/// <summary>
	/// Implements the interface for the saving ad accounts.
	/// </summary>
	/// <seealso cref="IAdAccountRepository" />
	[DefaultBinding(typeof(IAdAccountRepository))]
	public class AdAccountRepository : IAdAccountRepository
	{

		#region Fields: Private

		private readonly Guid _connectionStatusId = Guid.Parse("9f00f7c6-749b-442a-a794-02b17e81d9d4");
		private readonly UserConnection _userConnection;
		private readonly ILog _logger;
		private readonly AdPlatformRepository _adPlatformRepository = new AdPlatformRepository();

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Initializes a new instance of the <see cref="AdAccountRepository"/> class.
		/// </summary>
		/// <param name="userConnection">The user connection.</param>
		/// <param name="logger">The logger.</param>
		public AdAccountRepository(UserConnection userConnection, ILog logger) {
			_userConnection = userConnection;
			_logger = logger;
		}

		#endregion

		#region Methods: Private

		private EntityCollection GetExistingAccount(AdAccountResponse adAccount) {
			var esqResult = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "AdAccount") {
				PrimaryQueryColumn = { IsVisible = true }
			};
			esqResult.AddColumn("AccountId");
			esqResult.AddColumn("Name");
			var filterByName =
				esqResult.CreateFilterWithParameters(FilterComparisonType.Equal, "Name", adAccount.Name);
			esqResult.Filters.Add(filterByName);
			var filterByAdAccountId =
				esqResult.CreateFilterWithParameters(FilterComparisonType.Equal, "AccountId",
					adAccount.PlatformAdAccountId);
			esqResult.Filters.Add(filterByAdAccountId);
			var entities = esqResult.GetEntityCollection(_userConnection);
			return entities;
		}

		private bool SaveNewAccount(string platform, AdAccountResponse adAccount) {
			var platformId = _adPlatformRepository.GetPlatformId(platform, _userConnection);
			var adAccountEntity = _userConnection.EntitySchemaManager.GetInstanceByName("AdAccount")
				.CreateEntity(_userConnection);
			adAccountEntity.SetDefColumnValues();
			adAccountEntity.SetColumnValue("AccountId", adAccount.PlatformAdAccountId);
			adAccountEntity.SetColumnValue("Name", adAccount.Name);
			adAccountEntity.SetColumnValue("AdAccountUrl", adAccount.AdAccountUrl);
			adAccountEntity.SetColumnValue("AdPlatformId", platformId);
			adAccountEntity.SetColumnValue("ConnectionStatusId", _connectionStatusId);
			var accountAdded = adAccountEntity.Save();
			return accountAdded;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Saves the ad accounts.
		/// </summary>
		/// <param name="adAccounts">The ad accounts.</param>
		/// <param name="platform">The platform.</param>
		/// <returns>
		/// True when at least one account added.
		/// </returns>
		public bool SaveAdAccounts(IEnumerable<AdAccountResponse> adAccounts, string platform) {
			var atLeastOneAccountAdded = false;
			try {
				foreach (var adAccount in adAccounts) {
					var entities = GetExistingAccount(adAccount);
					if (entities.Count != 0) {
						continue;
					}
					var accountAdded = SaveNewAccount(platform, adAccount);
					if (atLeastOneAccountAdded == false && accountAdded) {
						atLeastOneAccountAdded = true;
					}
				}
			} catch (Exception e) {
				_logger.Error(e.Message);
				throw;
			}
			return atLeastOneAccountAdded;
		}

		#endregion

	}
}