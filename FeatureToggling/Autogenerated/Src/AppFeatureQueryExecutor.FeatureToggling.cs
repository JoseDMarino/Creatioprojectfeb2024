namespace Terrasoft.AppFeatures
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Creatio.FeatureToggling.Configuration;
	using Creatio.FeatureToggling.Providers;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Store;

	#region Class: AppFeatureQueryExecutor

	[DefaultBinding(typeof(IEntityQueryExecutor), Name = "AppFeatureQueryExecutor")]
	internal class AppFeatureQueryExecutor : BaseQueryExecutor, IEntityQueryExecutor
	{

		#region Fields: Private

		private static readonly ObjectQuery<FeatureDescriptor> _objectQuery = new ObjectQuery<FeatureDescriptor>(
			new Dictionary<string, string> {
				{"Code", nameof(FeatureDescriptor.Name) },
				{"Name", nameof(FeatureDescriptor.Name) },
				{"Description", nameof(FeatureDescriptor.Description) },
				{"Source", nameof(FeatureDescriptor.Source) }
			}, descriptor => descriptor.Name);

		internal static string FeatureIdToNameMapCacheKey => "FeatureToggle_FeatureIdToNameMap";

		#endregion

		#region Constructors: Public

		public AppFeatureQueryExecutor(UserConnection userConnection)
			: base(userConnection, "AppFeature") { }

		#endregion

		#region Methods: Private

		private static ICacheStore GetCacheStore(UserConnection userConnection) => userConnection.SessionCache;

		private static void SaveNameToIdMap(UserConnection userConnection, Dictionary<string, Guid> featureIdToNameMap) {
			ICacheStore store = GetCacheStore(userConnection);
			store[FeatureIdToNameMapCacheKey] = featureIdToNameMap;
		}

		private Entity FindFeatureById(Dictionary<string, Guid> featureIdToNameMap, Guid featureId,
				IList<FeatureDescriptor> descriptors) {
			string featureName = featureIdToNameMap.FirstOrDefault(kvp => kvp.Value == featureId).Key;
			foreach (FeatureDescriptor descriptor in descriptors) {
				if (GetFeatureId(descriptor) == featureId) {
					Entity entity = CreateFeatureEntity(descriptor, featureId);
					entity.StoringState = StoringObjectState.NotChanged;
					return entity;
				}
				if (descriptor.Name == featureName) {
					return CreateFeatureEntity(descriptor, featureId);
				}
			}
			Entity CreateFeatureEntity(FeatureDescriptor descriptor, Guid id) {
				Entity row = MapDescriptorToEntity(descriptor, out bool _);
				row.PrimaryColumnValue = id;
				return row;
			}
			return null;
		}

		private Guid? GetFeatureId(FeatureDescriptor descriptor) {
			if (descriptor.AdditionalProperties != null &&
					descriptor.AdditionalProperties.TryGetValue("FeatureId", out object id) && id is Guid featureId) {
				return featureId;
			}
			return null;
		}

		private Entity MapDescriptorToEntity(FeatureDescriptor descriptor, out bool featureExistInDb) {
			Entity row = EntitySchema.CreateEntity(UserConnection);
			row.SetDefColumnValues();
			row.SetColumnValue("Name", descriptor.Name);
			row.SetColumnValue("Code", descriptor.Name);
			row.SetColumnValue("Description", descriptor.Description);
			row.SetColumnValue("Source", descriptor.Source);
			row.SetColumnValue("State", descriptor.IsEnabled);
			row.SetColumnValue("StateForCurrentUser", descriptor.IsEnabled);
			featureExistInDb = false;
			if (descriptor.AdditionalProperties == null) {
				return row;
			}
			Guid? featureId = GetFeatureId(descriptor);
			if (featureId.HasValue) {
				row.PrimaryColumnValue = featureId.Value;
				featureExistInDb = true;
			}
			if (descriptor.AdditionalProperties.TryGetValue("IsEnabledByDefault", out object enabled) &&
				enabled is bool isEnabledByDefault) {
				row.SetColumnValue("State", isEnabledByDefault);
			}
			return row;
		}

		#endregion

		#region Methods: Public

		public static Dictionary<string, Guid> GetNameToIdMap(UserConnection userConnection) {
			ICacheStore store = GetCacheStore(userConnection);
			object cache = store[FeatureIdToNameMapCacheKey];
			var featureIdToNameMap = cache as Dictionary<string, Guid> ?? new Dictionary<string, Guid>();
			return featureIdToNameMap;
		}

		public EntityCollection GetEntityCollection(EntitySchemaQuery esq) {
			var collection = new EntityCollection(UserConnection, EntitySchema);
			IList<FeatureDescriptor> allDescriptors = FeatureService.Instance.GetFeatures();
			Dictionary<string, Guid> featureIdToNameMap = GetNameToIdMap(UserConnection);
			QueryFilterInfo filterInfo = esq.Filters.ParseFilters();
			if (GetIsPrimaryColumnValueFilter(filterInfo, out Guid currentFeatureId)) {
				Entity featureEntity = FindFeatureById(featureIdToNameMap, currentFeatureId, allDescriptors);
				if (featureEntity != null) {
					collection.Add(featureEntity);
				}
				return collection;
			}
			EntitySchemaQuery parentQuery = esq.Filters.ParentQuery;
			IEnumerable<FeatureDescriptor> filteredItems = _objectQuery.GetItems(allDescriptors, parentQuery, filterInfo);
			foreach (FeatureDescriptor descriptor in filteredItems) {
				Entity row = MapDescriptorToEntity(descriptor, out bool featureExistInDb);
				if (!featureExistInDb) {
					if (featureIdToNameMap.TryGetValue(descriptor.Name, out var featureId)) {
						row.PrimaryColumnValue = featureId;
					} else {
						featureIdToNameMap[descriptor.Name] = row.PrimaryColumnValue;
					}
				}
				collection.Add(row);
			}
			SaveNameToIdMap(UserConnection, featureIdToNameMap);
			return collection;
		}

		public static void RemoveFeatureIdFromCache(UserConnection userConnection, string featureName) {
			Dictionary<string, Guid> cache = GetNameToIdMap(userConnection);
			if (cache.Remove(featureName)) {
				SaveNameToIdMap(userConnection, cache);
			}
		}

		#endregion

	}

	#endregion

}
