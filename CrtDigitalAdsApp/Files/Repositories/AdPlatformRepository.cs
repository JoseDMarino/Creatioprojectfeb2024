namespace CrtDigitalAdsApp.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;

	/// <summary>
	/// Provides the data about AdPlatforms.
	/// </summary>
	internal class AdPlatformRepository
	{

		#region Fields: Private

		private static Dictionary<Guid, string> _platformNames = new Dictionary<Guid, string>();

		#endregion

		#region Methods: Private

		private Dictionary<Guid, string> GetExistingPlatforms(UserConnection userConnection) {
			var esqResult = new EntitySchemaQuery(userConnection.EntitySchemaManager, "AdPlatform") {
				PrimaryQueryColumn = { IsVisible = true }
			};
			esqResult.AddColumn("Id");
			esqResult.AddColumn("Code");
			var entities = esqResult.GetEntityCollection(userConnection);
			var resultDictionary = new Dictionary<Guid, string>();
			foreach (var entity in entities) {
				var id = entity.GetTypedColumnValue<Guid>("Id");
				var name = entity.GetTypedColumnValue<string>("Code");
				resultDictionary[id] = name.ToLower();
			}
			return resultDictionary;
		}

		#endregion

		#region Methods: Public		
		/// <summary>
		/// Gets the platform identifier.
		/// </summary>
		/// <param name="platform">The platform.</param>
		/// <param name="userConnection">The user connection.</param>
		/// <returns>Id of platform by name.</returns>
		public Guid GetPlatformId(string platform, UserConnection userConnection) {
			if (!_platformNames.Any()) {
				_platformNames = GetExistingPlatforms(userConnection);
			}
			var platformName = platform.ToLower();
			return _platformNames.Any(x => x.Value == platformName)
				? _platformNames.FirstOrDefault(x => x.Value == platform.ToLower()).Key
				: Guid.Empty;
		}

		/// <summary>
		/// Gets the name of the platform by Id.
		/// </summary>
		/// <param name="platformId">The platform identifier.</param>
		/// <param name="userConnection">The user connection.</param>
		/// <returns>Name of platform by id.</returns>
		public string GetPlatformName(Guid platformId, UserConnection userConnection) {
			if (!_platformNames.Any()) {
				_platformNames = GetExistingPlatforms(userConnection);
			}
			return !_platformNames.ContainsKey(platformId) ? string.Empty : _platformNames[platformId];
		}

		#endregion

	}
}