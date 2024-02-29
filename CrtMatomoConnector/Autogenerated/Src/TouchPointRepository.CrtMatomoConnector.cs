namespace Terrasoft.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MatomoConnector.API;
	using Newtonsoft.Json;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	using global::Common.Logging;
	using Terrasoft.Core.Entities;

	#region Class: InsertQueryExtensions

	public static class InsertQueryExtensions
	{
		public static Insert SetIntColumn(this Insert query, string columnAlias, string value, int defaultValue) {
			var intValue = value == null
				? defaultValue
				: int.Parse(value);
			return query.Set(columnAlias, Column.Parameter(intValue));
		}

		public static Insert TrySetLookupValue(this Insert query, string column,
				Dictionary<string, Guid> lookupValues, string lookupKey) {
			var existedColumn = query.ColumnValues.FirstOrDefault(x => x.SourceColumnAlias == column);
			if (string.IsNullOrWhiteSpace(lookupKey) || !lookupValues.TryGetValue(lookupKey, out var value)) {
				if (existedColumn == default(ModifyQueryColumnValue)) {
					var nullGuid = Column.Parameter(DBNull.Value,
						new GuidDataValueType(query.UserConnection.DataValueTypeManager));
					query.Set(column, nullGuid);
				}
				return query;
			}
			if (existedColumn != default(ModifyQueryColumnValue)) {
				query.ColumnValues.Remove(existedColumn);
			}
			query.Set(column, Column.Parameter(value));
			return query;
		}
	}

	#endregion

	#region Class: TouchModel

	/// <summary>
	/// Class to represent serializable touch model to process.
	/// </summary>
	public class TouchModel
	{

		#region Properties: Public

		[JsonIgnore]
		public Guid Id { get; set; }

		[JsonIgnore]
		public int Duration { get; set; }

		[JsonIgnore]
		public DateTime LastActionDateTime { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VisitorId { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Guid ContactId { get; set; }
		
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public int MatomoSiteId { get; set; }

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public override string ToString() {
			return Json.Serialize(this, true);
		}

		#endregion

	}

	#endregion

	#region Class: TouchPointRepository

	/// <summary>
	/// Repository to work with Touch points entities.
	/// </summary>
	public class TouchPointRepository
	{

		#region Class: VisitByCityComparer

		private class VisitByCityComparer : IEqualityComparer<VisitDetails>
		{
			public bool Equals(VisitDetails x, VisitDetails y) => x.city == y.city;
			public int GetHashCode(VisitDetails obj) => $"{obj.city}".GetHashCode();
		}

		#endregion

		#region Class: ActionDetailsClicksComparer

		private class ActionDetailsClicksComparer : IEqualityComparer<ActionDetails>
		{
			public bool Equals(ActionDetails x, ActionDetails y) => x.url == y.url && x.pageTitle == y.pageTitle;
			public int GetHashCode(ActionDetails obj) => $"{obj.url}{obj.pageTitle}".GetHashCode();
		}

		#endregion

		#region Class: VisitByReferrerTypeComparer

		private class VisitByReferrerTypeComparer : IEqualityComparer<VisitDetails>
		{
			public bool Equals(VisitDetails x, VisitDetails y) => x.referrerType == y.referrerType;
			public int GetHashCode(VisitDetails obj) => obj.referrerType.GetHashCode();
		}

		#endregion

		#region Fields: Private

		private static readonly ILog _logger = LogManager.GetLogger("MatomoConnector");

		private readonly Dictionary<string, Dictionary<string, Guid>> _lookup =
			new Dictionary<string, Dictionary<string, Guid>>();

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor for <see cref="TouchPointRepository"/>.
		/// </summary>
		/// <param name="userConnection"></param>
		public TouchPointRepository(UserConnection userConnection) => UserConnection = userConnection;

		#endregion

		#region Properties: Public

		public UserConnection UserConnection { get; set; }

		/// <summary>
		/// Instance of <see cref="ITouchSource"/> to define touch channel and source.
		/// </summary>
		private ITouchSource _sourceHelper;
		public ITouchSource SourceHelper {
			get => _sourceHelper ?? (_sourceHelper =
				ClassFactory.Get<ITouchSource>(new ConstructorArgument("userConnection", UserConnection)));
			set => _sourceHelper = value;
		}

		#endregion

		#region Methods: Private

		private Dictionary<string, (Guid Source, Guid Medium)> DefineVisitSourceAndChannel(
				IEnumerable<VisitDetails> visits) {
			var result = new Dictionary<string, (Guid Source, Guid Medium)>();
			visits.ForEach(visit => {
				var parameters = new Dictionary<string, string> {
					{ "bpmRef", visit.referrerUrl },
					{ "utm_medium", visit.campaignMedium },
					{ "utm_source", visit.campaignSource },
					{ "utm_campaign", visit.campaignName }
				};
				var extendedData = SourceHelper.ComputeMediumAndSource(parameters);
				if (extendedData.Medium.IsNotEmpty() || extendedData.Source.IsNotEmpty()) {
					result.Add(visit.idVisit, (extendedData.Source, extendedData.Medium));
				}
			});
			return result;
		}

		private Dictionary<Guid, VisitDetails> InsertTouches(Guid contactId, string userId,
				IEnumerable<VisitDetails> newVisits) {
			var result = new Dictionary<Guid, VisitDetails>();
			var visitExtendedData = DefineVisitSourceAndChannel(newVisits);
			var nullGuid = Column.Parameter(DBNull.Value, new GuidDataValueType(UserConnection.DataValueTypeManager));
			var insert = new Insert(UserConnection).Into(nameof(Touch));
			foreach (var visit in newVisits) {
				var id = Guid.NewGuid();
				var sessionStart = ConvertTimestampToDateTime(visit.firstActionTimestamp);
				var cityCode = GetCityCode(visit.countryCode, visit.city, visit.regionCode);
				var shortCityCode = GetCityCode(visit.countryCode, visit.city);
				insert.Values()
					.Set(nameof(Touch.Id), Column.Parameter(id))
					.Set(nameof(Touch.ContactId), Column.Parameter(contactId))
					.SetIntColumn(nameof(Touch.Duration), visit.visitDuration, 0)
					.Set(nameof(Touch.SessionId), Column.Parameter(visit.idVisit ?? ""))
					.Set(nameof(Touch.PageReferrer), Column.Parameter(CleanupUrl(visit.referrerUrl) ?? visit.referrerTypeName ?? ""))
					.Set(nameof(Touch.IP), Column.Parameter(visit.visitIp ?? ""))
					.Set(nameof(Touch.UtmSourceStr), Column.Parameter(visit.campaignSource ?? ""))
					.Set(nameof(Touch.UtmMediumStr), Column.Parameter(visit.campaignMedium ?? ""))
					.Set(nameof(Touch.UtmContentStr), Column.Parameter(visit.campaignContent ?? ""))
					.Set(nameof(Touch.UtmCampaignStr), Column.Parameter(visit.campaignName ?? ""))
					.Set(nameof(Touch.UtmTermStr), Column.Parameter(visit.campaignKeyword ?? ""))
					.Set(nameof(Touch.UtmIdStr), Column.Parameter(visit.campaignId ?? ""))
					.Set(nameof(Touch.CityStr), Column.Parameter(visit.city ?? ""))
					.Set(nameof(Touch.CountryStr), Column.Parameter(visit.country ?? ""))
					.Set(nameof(Touch.LanguageStr), Column.Parameter(visit.languageCode ?? ""))
					.Set(nameof(Touch.DeviceStr), Column.Parameter(visit.deviceType + " " + visit.deviceBrand + " " + visit.deviceModel))
					.Set(nameof(Touch.PlatformStr), Column.Parameter(visit.operatingSystemName + " " + visit.operatingSystemVersion))
					.Set(nameof(Touch.MatomoUserId), Column.Parameter(string.IsNullOrWhiteSpace(userId)
						? visit.userId ?? string.Empty
						: userId))
					.Set(nameof(Touch.MatomoVisitorId), Column.Parameter(visit.visitorId ?? ""))
					.Set(nameof(Touch.MatomoSiteId), Column.Parameter(visit.idSite ?? ""))
					.Set(nameof(Touch.StartDate), Column.Parameter(sessionStart))
					.Set(nameof(Touch.CountryCode), Column.Parameter(visit.countryCode ?? ""))
					.Set(nameof(Touch.RegionCode), Column.Parameter(visit.regionCode ?? ""))
					.Set(nameof(Touch.RegionStr), Column.Parameter(visit.region ?? ""))
					.Set(nameof(Touch.Location), Column.Parameter($"{visit.location}: {visit.latitude}, {visit.longitude}"))
					.Set(nameof(Touch.ReferrerNameStr), Column.Parameter(visit.referrerName ?? ""))
					.Set(nameof(Touch.ReferrerTypeStr), Column.Parameter(visit.referrerType ?? ""))
					.Set(nameof(Touch.ReferrerKeyword), Column.Parameter(visit.referrerKeyword ?? ""))
					.Set(nameof(Touch.ReferrerUrl), Column.Parameter(CleanupUrl(visit.referrerUrl) ?? ""))
					.TrySetLookupValue(nameof(Touch.ReferrerTypeId), _lookup["ReferrerType"], visit.referrerType)
					.TrySetLookupValue(nameof(Touch.CountryId), _lookup["Country"], visit.countryCode)
					.TrySetLookupValue(nameof(Touch.RegionId), _lookup["Region"], visit.regionCode)
					.TrySetLookupValue(nameof(Touch.CityId), _lookup["City"], shortCityCode)
					.TrySetLookupValue(nameof(Touch.CityId), _lookup["City"], cityCode)
					.TrySetLookupValue(nameof(Touch.LanguageId), _lookup["Language"], visit.languageCode);
				if (DateTime.TryParse(visit.lastActionDateTime, out var lastActionDate)) {
					insert.Set(nameof(Touch.LastActionDateTime), Column.Parameter(lastActionDate));
				} else {
					insert.Set(nameof(Touch.LastActionDateTime), Column.Parameter(DateTime.MinValue));
				}
				if (visitExtendedData.ContainsKey(visit.idVisit)
						&& visitExtendedData[visit.idVisit].Medium.IsNotEmpty()) {
					insert.Set(nameof(Touch.ChannelId), Column.Parameter(visitExtendedData[visit.idVisit].Medium));
				} else {
					insert.Set(nameof(Touch.ChannelId), nullGuid);
				}
				if (visitExtendedData.ContainsKey(visit.idVisit)
						&& visitExtendedData[visit.idVisit].Source.IsNotEmpty()) {
					insert.Set(nameof(Touch.SourceId), Column.Parameter(visitExtendedData[visit.idVisit].Source));
				} else {
					insert.Set(nameof(Touch.SourceId), nullGuid);
				}
				result.Add(id, visit);
			}
			if (insert.ColumnValues.IsNotEmpty()) {
				insert.Execute();
			}
			return result;
		}

		private DateTime ConvertTimestampToDateTime(int timestamp) {
			return new DateTime(1970, 1, 1).AddSeconds(timestamp);
		}

		private void AddLookupFilterCondition(Select select, string codeColumn, IEnumerable<string> codes,
				bool isUpperCase) {
			if (isUpperCase) {
				select.Where(Func.Upper(codeColumn)).In(Column.Parameters(codes));
			} else {
				select.Where(codeColumn).In(Column.Parameters(codes));
			}
		}

		private void AddIfNotExists<K, V>(Dictionary<K, V> dictionary, K key, V value) {
			if (dictionary.ContainsKey(key)) {
				return;
			}
			dictionary.Add(key, value);
		}

		private Dictionary<string, Guid> GetLookupIdByCode(string lookup, string codeColumn, IEnumerable<string> codes,
				bool isUpperCase = false) {
			var result = new Dictionary<string, Guid>();
			var chunks = codes.SplitOnChunks(chunkSize: 500);
			foreach (var chunk in chunks) {
				var select = new Select(UserConnection)
					.Cols("Id", codeColumn)
					.From(lookup);
				AddLookupFilterCondition(select, codeColumn, chunk, isUpperCase);
				select.SpecifyNoLockHints();
				select.ExecuteReader(r => {
					var id = r.GetColumnValue<Guid>("Id");
					var code = r.GetColumnValue<string>(codeColumn);
					AddIfNotExists(result, code.ToLower(), id);
				});
			}
			return result;
		}

		private string GetCityCode(string countryCode, string cityName, string regionCode) =>
			$"{GetCityCode(countryCode, cityName)}{regionCode ?? ""}";

		private string GetCityCode(string countryCode, string cityName) =>
			$"{countryCode}:{cityName}:";

		private Dictionary<string, Guid> GetCityIds(Dictionary<string, IEnumerable<VisitDetails>> countries) {
			var result = new Dictionary<string, Guid>();
			foreach (var country in countries) {
				var countryCode = country.Key;
				if (!_lookup["Country"].TryGetValue(countryCode, out var countryId)) {
					continue;
				}
				var visits = country.Value;
				var cityNames = visits.Select(x => x.city).Distinct();
				var chunks = cityNames.SplitOnChunks(chunkSize: 500);
				foreach (var chunk in chunks) {
					var citiesSelect = new Select(UserConnection)
						.Column("c", "Id").As("Id")
						.Column("c", "Name").As("Name")
						.Column("r", "Code").As("RegionCode")
					.From("City", "c")
					.LeftOuterJoin("Region").As("r").On("c", "RegionId").IsEqual("r", "Id")
					.Where("c", "CountryId").IsEqual(Column.Parameter(countryId))
						.And("c", "Name").In(Column.Parameters(chunk)) as Select;
					citiesSelect.SpecifyNoLockHints();
					citiesSelect.ExecuteReader(r => {
						var cityId = r.GetColumnValue<Guid>("Id");
						var cityName = r.GetColumnValue<string>("Name");
						var regionCode = r.GetColumnValue<string>("RegionCode");
						var cityCode = GetCityCode(countryCode, cityName, regionCode);
						AddIfNotExists(result, cityCode, cityId);
					});
				}
			}
			return result;
		}

		private int InsertActionDetails(IEnumerable<ActionDetails> actions, Guid touchId, TimeSpan offset) {
			var insert = new Insert(UserConnection).Into(nameof(TouchAction));
			foreach (var action in actions) {
				var actionDate = ConvertTimestampToDateTime(action.timestamp);
				var pageCode = GetWebPageCode(GetPageTitle(action), action.url);
				var actionDateUtc = actionDate.Add(-offset);
				var nullGuid = Column.Parameter(DBNull.Value, new GuidDataValueType(UserConnection.DataValueTypeManager));
				insert.Values()
					.Set(nameof(TouchAction.ActionId), Column.Parameter(action.idpageview ?? ""))
					.Set(nameof(TouchAction.Title), Column.Parameter(action.title ?? action.pageTitle ?? ""))
					.Set(nameof(TouchAction.Url), Column.Parameter(action.url))
					.Set(nameof(TouchAction.ActionDate), Column.Parameter(actionDateUtc))
					.Set(nameof(TouchAction.TouchId), Column.Parameter(touchId))
					.Set(nameof(TouchAction.TypeStr), Column.Parameter(action.type ?? ""))
					.TrySetLookupValue(nameof(TouchAction.WebPageId), _lookup["WebPage"], pageCode)
					.TrySetLookupValue(nameof(TouchAction.WebPageId), _lookup["TouchActionWebPage"], action.idpageview)
					.TrySetLookupValue(nameof(TouchAction.TypeId), _lookup["TouchActionType"], action.type);
			}
			if (insert.ColumnValues.IsNotEmpty()) {
				return insert.Execute();
			}
			return 0;
		}

		private TimeSpan GetVisitOffset(VisitDetails visit) {
			if (!string.IsNullOrWhiteSpace(visit.serverTimePretty)
					&& !string.IsNullOrWhiteSpace(visit.serverDate)) {
				var result = DateTime.TryParse($"{visit.serverDate} {visit.serverTimePretty}",
					out var serverDateTime);
				if (result) {
					var utc = ConvertTimestampToDateTime(visit.serverTimestamp);
					return serverDateTime - utc;
				}
			}
			return new TimeSpan(0);
		}

		private string GetActionCode(string actionId, int timestamp, VisitDetails visit) {
			var offset = GetVisitOffset(visit);
			return $"{actionId}:{ConvertTimestampToDateTime(timestamp).Add(-offset)}";
		}
		private string GetActionCode(string actionId, DateTime actionDate) =>
			$"{actionId}:{actionDate}";

		private int InsertTouchActions(Dictionary<Guid, VisitDetails> visits) {
			var rowsCount = 0;
			var actionDetails = visits.Values
				.SelectMany(visit => visit.actionDetails.Select(action => (action, visit)));
			foreach (var visit in visits) {
				var newActionDetails = actionDetails
					.Where(x => x.visit.idVisit == visit.Value.idVisit)
					.Where(x => {
						var actionCode = GetActionCode(x.action.idpageview, x.action.timestamp, x.visit);
						return !_lookup["TouchAction"].ContainsKey(actionCode);
					});
				var newActions = newActionDetails.Select(x => {
					var pageCode = GetWebPageCode(GetPageTitle(x.action), x.action.url);
					var actionCode = GetActionCode(x.action.idpageview, x.action.timestamp, x.visit);
					if (_lookup["WebPage"].TryGetValue(pageCode, out var pageId)) {
						AddIfNotExists(_lookup["TouchAction"], actionCode, pageId);
						AddIfNotExists(_lookup["TouchActionWebPage"], x.action.idpageview, pageId);
					}
					return x.action;
				}).ToList();
				var offset = GetVisitOffset(visit.Value);
				if (newActions.Any()) {
					var chunks = newActions.SplitOnChunks(chunkSize: 50);
					foreach (var chunk in chunks) {
						rowsCount += InsertActionDetails(chunk, visit.Key, offset);
					}
				}
			}
			return rowsCount;
		}

		private Dictionary<string, TouchModel> GetExistedTouchIdsForVisits(IEnumerable<string> visitIds) {
			var touchIds = new Dictionary<string, TouchModel>();
			if (!visitIds.Any()) {
				return touchIds;
			}
			var select = new Select(UserConnection)
				.Column(nameof(Touch.SessionId))
				.Column(nameof(Touch.Id))
				.Column(nameof(Touch.Duration))
				.Column(nameof(Touch.LastActionDateTime))
				.From(nameof(Touch))
				.Where(nameof(Touch.SessionId)).In(Column.Parameters(visitIds)) as Select;
			select.SpecifyNoLockHints();
			select.ExecuteReader(reader => {
				var visitId = reader.GetColumnValue<string>(nameof(Touch.SessionId));
				var touchId = reader.GetColumnValue<Guid>(nameof(Touch.Id));
				var duration = reader.GetColumnValue<int>(nameof(Touch.Duration));
				var lastActionDate = reader.GetColumnValue<DateTime>(nameof(Touch.LastActionDateTime));
				touchIds.Add(visitId, new TouchModel {
					Id = touchId,
					Duration = duration,
					LastActionDateTime = lastActionDate
				});
			});
			return touchIds;
		}

		private Dictionary<string, Guid> GetExistingLookupValues(string lookupName) {
			var select = new Select(UserConnection)
				.Column("Id")
				.Column("MatomoCode")
				.From(lookupName)
				.Where("MatomoCode").Not().IsNull() as Select;
			select.SpecifyNoLockHints();
			var result = new Dictionary<string, Guid>();
			select.ExecuteReader(reader => {
				var code = reader.GetColumnValue<string>("MatomoCode");
				var id = reader.GetColumnValue<Guid>("Id");
				AddIfNotExists(result, code, id);
			});
			return result;
		}

		private string GetWebPageCode(string title, string url) => $"{title}{url}";

		private Dictionary<string, Guid> GetExistingPages(Dictionary<string, ActionDetails> webPages) {
			var pages = new Dictionary<string, Guid>();
			var pageTitles = webPages.Values
				.Select(x => GetPageTitle(x))
				.Where(x => !string.IsNullOrWhiteSpace(x));
			var pageUrls = webPages.Values
				.Where(x => !string.IsNullOrWhiteSpace(x.url))
				.Select(x => x.url);
			if (!pageTitles.Any() && !pageUrls.Any()) {
				return pages;
			}
			var esq = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "WebPage") {
				UseAdminRights = false,
				IsDistinct = true,
			};
			esq.AddColumn("Id");
			esq.AddColumn("Name");
			esq.AddColumn("Url");
			var rootFilter = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.And);
			var innerUrlsFilter = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.Or);
			if (pageTitles.Any()) {
				var titleFilter = esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Name", pageTitles);
				rootFilter.Add(titleFilter);
			}
			if (pageUrls.Any()) {
				foreach (var url in pageUrls) {
					var urlFilter = esq.CreateFilterWithParameters(FilterComparisonType.Contain, "Url", url);
					innerUrlsFilter.Add(urlFilter);
				}
			}
			rootFilter.Add(innerUrlsFilter);
			esq.Filters.Add(rootFilter);
			var entities = esq.GetEntityCollection(UserConnection);
			if (entities.Count > 0) {
				foreach (var entity in entities) {
					var pageId = entity.GetTypedColumnValue<Guid>("Id1");
					var pageTitle = entity.GetTypedColumnValue<string>("Name");
					var pageUrl = entity.GetTypedColumnValue<string>("Url");
					var pageCode = GetWebPageCode(pageTitle, pageUrl);
					pages.Add(pageCode, pageId);
				}
			}
			return pages;
		}

		private Dictionary<string, Guid> SaveNewTouchActionTypes(IEnumerable<string> newTypes) {
			var result = new Dictionary<string, Guid>();
			var insert = new Insert(UserConnection).Into(nameof(TouchActionType));
			foreach (var type in newTypes) {
				var id = Guid.NewGuid();
				insert.Values()
					.Set(nameof(TouchActionType.Id), Column.Parameter(id))
					.Set(nameof(TouchActionType.Name), Column.Parameter(type))
					.Set(nameof(TouchActionType.MatomoCode), Column.Parameter(type));
				result.Add(type, id);
			}
			if (insert.ColumnValues.IsNotEmpty()) {
				insert.Execute();
				return result;
			}
			return new Dictionary<string, Guid>();
		}

		private Dictionary<string, Guid> SaveNewReferrerTypes(IEnumerable<(string code, string name)> newTypes) {
			var result = new Dictionary<string, Guid>();
			var insert = new Insert(UserConnection).Into(nameof(ReferrerType));
			foreach (var type in newTypes) {
				var id = Guid.NewGuid();
				insert.Values()
					.Set(nameof(TouchActionType.Id), Column.Parameter(id))
					.Set(nameof(TouchActionType.Name), Column.Parameter(type.name))
					.Set(nameof(TouchActionType.MatomoCode), Column.Parameter(type.code));
				result.Add(type.code, id);
			}
			if (insert.ColumnValues.IsNotEmpty()) {
				insert.Execute();
				return result;
			}
			return new Dictionary<string, Guid>();
		}

		private string CleanupUrl(string url) {
			if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)) {
				return url;
			}
			if (string.IsNullOrWhiteSpace(uri.Query)) {
				return uri.AbsoluteUri;
			}
			return uri.AbsoluteUri.Replace(uri.Query, "");
		}

		private void CleanupVisitUrls(ref IEnumerable<VisitDetails> visitDetails) =>
			visitDetails.ForEach(v => v.actionDetails.ForEach(a => a.url = CleanupUrl(a.url ?? "")));

		private string GetPageTitle(ActionDetails page) {
			if (!string.IsNullOrWhiteSpace(page.pageTitle)) {
				return page.pageTitle;
			}
			if (!Uri.TryCreate(page.url, UriKind.Absolute, out var uri)) {
				return string.Empty;
			}
			var domain = uri.Authority.StartsWith("www.") ? uri.Authority.Substring(4) : uri.Authority;
			return domain + uri.LocalPath;
		}

		private Dictionary<string, Guid> SaveNewWebPages(IEnumerable<ActionDetails> newPages) {
			var result = new Dictionary<string, Guid>();
			var chunks = newPages.SplitOnChunks(200);
			var total = 0;
			foreach (var chunk in chunks) {
				var insert = new Insert(UserConnection).Into("WebPage");
				foreach (var page in chunk) {
					var id = Guid.NewGuid();
					var pageTitle = GetPageTitle(page);
					insert.Values()
						.Set("Id", Column.Parameter(id))
						.Set("Name", Column.Parameter(pageTitle))
						.Set("Url", Column.Parameter(page.url));
					var pageCode = GetWebPageCode(pageTitle, page.url);
					result.Add(pageCode, id);
				}
				total += insert.Execute();
			}
			return total > 0
				? result
				: new Dictionary<string, Guid>();
		}

		private Dictionary<string, Guid> ActualizeTouchActionTypes(IEnumerable<VisitDetails> visits) {
			var actionTypes = visits
				.SelectMany(x => x.actionDetails)
				.Select(x => x.type)
				.Distinct()
				.Where(x => !string.IsNullOrWhiteSpace(x));
			var existingActionTypes = GetExistingLookupValues(nameof(TouchActionType));
			var newTypes = actionTypes.Where(x => !existingActionTypes.Keys.Contains(x));
			if (newTypes.Any()) {
				var result = SaveNewTouchActionTypes(newTypes);
				existingActionTypes.AddRange(result);
			}
			return existingActionTypes;
		}

		private Dictionary<string, Guid> ActualizeReferrerTypes(IEnumerable<VisitDetails> visits) {
			var referrerTypes = visits
				.Where(x => !string.IsNullOrWhiteSpace(x.referrerType))
				.Distinct(new VisitByReferrerTypeComparer())
				.Select(x => (x.referrerType, x.referrerTypeName));
			var existingReferrerTypes = GetExistingLookupValues(nameof(ReferrerType));
			var newTypes = referrerTypes
				.Where(x => !existingReferrerTypes.Keys.Contains(x.referrerType))
				.Select(x => (x.referrerType, x.referrerTypeName));
			if (newTypes.Any()) {
				var result = SaveNewReferrerTypes(newTypes);
				existingReferrerTypes.AddRange(result);
			}
			return existingReferrerTypes;
		}

		private Dictionary<string, Guid> ActualizeCountries(IEnumerable<VisitDetails> visits) {
			var countryCodes = visits
				.Where(x => !string.IsNullOrWhiteSpace(x.countryCode))
				.Select(x => x.countryCode.ToUpper())
				.Distinct();
			if (!countryCodes.Any()) {
				return new Dictionary<string, Guid>();
			}
			var countries = GetLookupIdByCode("Country", "Alpha2Code", countryCodes);
			return countries;
		}

		private Dictionary<string, Guid> ActualizeRegions(IEnumerable<VisitDetails> visits) {
			var regionCodes = visits
				.Where(x => !string.IsNullOrWhiteSpace(x.regionCode))
				.Select(x => x.regionCode.ToUpper())
				.Distinct();
			if (!regionCodes.Any()) {
				return new Dictionary<string, Guid>();
			}
			var regions = GetLookupIdByCode("Region", "Code", regionCodes)
				.ToDictionary(x => x.Key.ToUpper(), x => x.Value);
			return regions;
		}

		private Dictionary<string, Guid> ActualizeCities(IEnumerable<VisitDetails> visits) {
			var countries = visits
				.Where(x => !string.IsNullOrWhiteSpace(x.city))
				.Select(x => x.countryCode)
				.Distinct()
				.Select(code => (code, visits.Where(x => x.countryCode == code).Distinct(new VisitByCityComparer())))
				.ToDictionary(x => x.code, x => x.Item2);
			return GetCityIds(countries);
		}

		private Dictionary<string, Guid> ActualizeWebPage(IEnumerable<VisitDetails> visits) {
			var webPages = visits
				.SelectMany(x => x.actionDetails)
				.Distinct(new ActionDetailsClicksComparer())
				.Where(x => !string.IsNullOrWhiteSpace(x.url) || !string.IsNullOrWhiteSpace(x.pageTitle))
				.ToDictionary(x => GetWebPageCode(GetPageTitle(x), x.url));
			var chunks = webPages.SplitOnChunks(chunkSize: 100);
			var result = new Dictionary<string, Guid>();
			foreach (var chunk in chunks) {
				var pages = chunk.ToDictionary(x => x.Key, x => x.Value);
				var existingPages = GetExistingPages(pages);
				var newPages = pages.Where(x => !existingPages.Keys.Contains(x.Key));
				if (newPages.Any()) {
					var savedPages = SaveNewWebPages(newPages.Select(x => x.Value));
					existingPages.AddRange(savedPages);
				}
				result.AddRange(existingPages);
			}
			return result;
		}

		private Dictionary<string, Guid> ActualizeExistedTouchAction(IEnumerable<VisitDetails> visits) {
			var result = new Dictionary<string, Guid>();
			var actionIds = visits
				.SelectMany(visit => visit.actionDetails)
				.Where(x => !string.IsNullOrWhiteSpace(x.idpageview))
				.Select(x => x.idpageview).Distinct();
			var chunks = actionIds.SplitOnChunks(chunkSize: 200);
			foreach (var chunk in chunks) {
				var select = new Select(UserConnection)
					.Column(nameof(TouchAction.WebPageId))
					.Column(nameof(TouchAction.ActionId))
					.Column(nameof(TouchAction.ActionDate))
				.From(nameof(TouchAction))
				.Where(nameof(TouchAction.ActionId)).In(Column.Parameters(chunk))
					.And(nameof(TouchAction.TypeStr)).IsEqual(Column.Parameter("action")) as Select;
				select.SpecifyNoLockHints();
				select.ExecuteReader(x => {
					var webPageId = x.GetColumnValue<Guid>(nameof(TouchAction.WebPageId));
					if (!webPageId.IsEmpty()) {
						var actionId = x.GetColumnValue<string>(nameof(TouchAction.ActionId));
						var actionDate = x.GetColumnValue<DateTime>(nameof(TouchAction.ActionDate));
						var actionCode = GetActionCode(actionId, actionDate);
						AddIfNotExists(result, actionCode, webPageId);
					}
				});
			}
			return result;
		}

		private Dictionary<string, Guid> ActualizeExistedTouchActionWebPage(IEnumerable<VisitDetails> visits) {
			var result = new Dictionary<string, Guid>();
			_lookup["TouchAction"].ForEach(x => {
				var actionId = x.Key.Split(':')[0];
				result.Add(actionId, x.Value);
			});
			return result;
		}

		private Dictionary<string, Guid> ActualizeLanguages(IEnumerable<VisitDetails> visits) {
			var languageCodes = visits
				.Where(x => !string.IsNullOrWhiteSpace(x.languageCode))
				.Select(x => x.languageCode.ToUpper())
				.Distinct();
			return GetLookupIdByCode("SysLanguage", "Code", languageCodes, true);
		}

		private void ActualizeLookups(IEnumerable<VisitDetails> visits) {
			_lookup["TouchActionType"] = ActualizeTouchActionTypes(visits);
			_lookup["ReferrerType"] = ActualizeReferrerTypes(visits);
			_lookup["Country"] = ActualizeCountries(visits);
			_lookup["Region"] = ActualizeRegions(visits);
			_lookup["City"] = ActualizeCities(visits);
			_lookup["WebPage"] = ActualizeWebPage(visits);
			_lookup["TouchAction"] = ActualizeExistedTouchAction(visits);
			_lookup["TouchActionWebPage"] = ActualizeExistedTouchActionWebPage(visits);
			_lookup["Language"] = ActualizeLanguages(visits);
		}

		private bool IsTouchModified(TouchModel touch, int duration, DateTime actionDate) {
			return touch.Duration != duration || touch.LastActionDateTime != actionDate;
		}

		private void UpdateTouch(Guid id, int duration, DateTime lastActionDate) {
			var update = new Update(UserConnection, nameof(Touch))
				.Set(nameof(Touch.Duration), Column.Parameter(duration))
				.Set(nameof(Touch.LastActionDateTime), Column.Parameter(lastActionDate))
				.Where(nameof(Touch.Id)).IsEqual(Column.Parameter(id)) as Update;
			update.WithHints(new RowLockHint());
			update.Execute();
		}

		private void UpdateTouches(Dictionary<Guid, VisitDetails> existedVisits, IEnumerable<TouchModel> touches) {
			foreach (var visit in existedVisits) {
				var touch = touches.FirstOrDefault(x => x.Id == visit.Key);
				var duration = int.Parse(visit.Value.visitDuration ?? "0");
				DateTime lastActionDate;
				if (touch != null
						&& DateTime.TryParse(visit.Value.lastActionDateTime, out lastActionDate)
						&& IsTouchModified(touch, duration, lastActionDate)) {
					UpdateTouch(touch.Id, duration, lastActionDate);
				}
			}
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Saves list of <paramref name="visitDetails"/> into the DB with specified user id.
		/// Depends on already saved records.
		/// </summary>
		public int SaveVisitDetails(Guid contactId, string userId, IEnumerable<VisitDetails> visitDetails) {
			CleanupVisitUrls(ref visitDetails);
			ActualizeLookups(visitDetails);
			var visits = visitDetails.ToDictionary(x => x.idVisit);
			var result = 0;
			using (var executor = UserConnection.EnsureDBConnection()) {
				var chunks = visits.SplitOnChunks(chunkSize: 10);
				foreach (var chunk in chunks) {
					var rowsCount = 0;
					executor.StartTransaction();
					try {
						var touchModels = GetExistedTouchIdsForVisits(chunk.Select(x => x.Key));
						var existedVisits = chunk
							.Where(x => touchModels.Select(y => y.Key).Contains(x.Key))
							.ToDictionary(x => touchModels[x.Key].Id, y => y.Value);
						var newVisits = chunk
							.Where(x => !touchModels.Select(y => y.Key).Contains(x.Key))
							.Select(x => x.Value);
						UpdateTouches(existedVisits, touchModels.Values);
						var newTouches = InsertTouches(contactId, userId, newVisits);
						rowsCount += newTouches.Count;
						existedVisits.AddRange(newTouches);
						rowsCount += InsertTouchActions(existedVisits);
						executor.CommitTransaction();
					} catch (Exception ex) {
						executor.RollbackTransaction();
						_logger.Error($"MatomoConnector.{nameof(TouchPointRepository)}.SaveVisitDetails. Exception. ", ex);
						rowsCount = 0;
					}
					result += rowsCount;
				}
			}
			return result;
		}

		/// <summary>
		/// Saves list of <paramref name="visitDetails"/> into the DB for all users. Depends on already saved records.
		/// </summary>
		public int SaveVisitDetails(Guid contactId, IEnumerable<VisitDetails> visitDetails) {
			return SaveVisitDetails(contactId, null, visitDetails);
		}

		#endregion

	}

	#endregion

}

