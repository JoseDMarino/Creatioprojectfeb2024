namespace IntegrationV2.Files.cs.Domains.MeetingDomain.Repository
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Repository.Interfaces;
	using IntegrationV2.Files.cs.Domains.MeetingDomain.Model;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Store;
	using Terrasoft.IntegrationV2.Utils;
	using Terrasoft.EmailDomain;
	using Terrasoft.Common;
	using IntegrationApi.MailboxDomain.Interfaces;

	#region Class: CalendarRepository

	/// <summary>
	/// <see cref="ICalendarRepository"/> implementation.
	/// </summary>
	[DefaultBinding(typeof(ICalendarRepository), Name = nameof(CalendarRepository))]
	public class CalendarRepository : ICalendarRepository
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="UserConnection"/> instance.
		/// </summary>
		private readonly UserConnection _userConnection;

		/// <summary>
		/// Synchronization session identifier.
		private readonly string _sessionId;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// .ctor.
		/// </summary>
		/// <param name="uc"><see cref="UserConnection"/> instance.</param>
		/// <param name="sessionId">Session identifier.</param>
		public CalendarRepository(UserConnection uc, string sessionId) {
			_userConnection = uc;
			_sessionId = sessionId;
		}

		#endregion

		#region Methods: Private

		private List<Calendar> GetCalendarsFromCache(ICacheStore applicationCache) {
			object store = applicationCache[IntegrationConsts.Calendar.CacheItemName];
			if (ListenerUtils.GetIsFeatureEnabled(_userConnection, "IsCalendarCached") && store != null) {
				return store as List<Calendar>;
			}
			return null;
		}

		private List<Calendar> ConvertEntitiesToModels(EntityCollection calendarsEntities) {
			var result = new List<Calendar>();
			var mailboxService = ClassFactory.Get<IMailboxService>(new ConstructorArgument("uc", _userConnection));
			foreach (var calendarEntity in calendarsEntities) {
				var settingsId = calendarEntity.GetTypedColumnValue<Guid>("SettingsId");
				var emails = mailboxService.GetEmails(settingsId);
				var calendar = new Calendar(calendarEntity, _sessionId, emails);
				var senderEmailAddress = calendarEntity.GetTypedColumnValue<string>("SenderEmailAddress");
				var sysAdminUnitId = calendarEntity.GetTypedColumnValue<Guid>("SysAdminUnitId");
				var calendarSettings = new CalendarSettings(calendarEntity.GetTypedColumnValue<string>("OAuthClassName")) {
					Id = settingsId,
					OAuthApplicationId = calendarEntity.GetTypedColumnValue<Guid>("OAuthApplicationId"),
					Login = calendarEntity.GetTypedColumnValue<string>("Login"),
					Password = calendarEntity.GetTypedColumnValue<string>("Password"),
					UserId = sysAdminUnitId,
					UserName = calendarEntity.GetTypedColumnValue<string>("SysAdminUnitName"),
					SenderEmailAddress = senderEmailAddress,
					ServiceUrl = calendarEntity.GetTypedColumnValue<string>("ServiceUrl"),
					SyncEnabled = (calendarEntity.GetTypedColumnValue<bool>("ImportAppointments") ||
						calendarEntity.GetTypedColumnValue<bool>("ExportActivities")) && 
						calendar.SyncSinceDate != DateTime.MinValue && calendar.SyncTillDate != DateTime.MinValue &&
						!calendarEntity.GetTypedColumnValue<bool>("SynchronizationStopped"),
					WarningCodeId = calendarEntity.GetTypedColumnValue<Guid>("WarningCodeId")
				};
				calendar.OldSyncEnabled = ListenerUtils.GetIsFeatureDisabled(_userConnection, "NewMeetingIntegration", sysAdminUnitId);
				calendar.SetOwner(calendarEntity.GetTypedColumnValue<Guid>("CreatedById"));
				calendar.SetCalendarSettings(calendarSettings);
				result.Add(calendar);
			}
			return result;
		}

		private EntitySchemaQuery GetCalendarEsq() {
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, "ActivitySyncSettings");
			esq.UseAdminRights = false;
			esq.PrimaryQueryColumn.IsAlwaysSelect = true;
			esq.AddColumn("ActivitySyncPeriod");
			esq.AddColumn("ExportActivities");
			esq.AddColumn("ImportAppointments");
			esq.AddColumn("AppointmentLastSyncDate");
			esq.AddColumn("CreatedBy");
			esq.AddColumn("MailboxSyncSettings").Name = "Settings";
			esq.AddColumn("MailboxSyncSettings.SenderEmailAddress").Name = "SenderEmailAddress";
			esq.AddColumn("MailboxSyncSettings.UserName").Name = "Login";
			esq.AddColumn("MailboxSyncSettings.UserPassword").Name = "Password";
			esq.AddColumn("MailboxSyncSettings.SysAdminUnit").Name = "SysAdminUnit";
			esq.AddColumn("MailboxSyncSettings.MailServer.ExchangeEmailAddress").Name = "ServiceUrl";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication").Name = "OAuthApplication";
			esq.AddColumn("MailboxSyncSettings.MailServer.OAuthApplication.ClientClassName").Name = "OAuthClassName";
			esq.AddColumn("MailboxSyncSettings.SynchronizationStopped").Name = "SynchronizationStopped";
			esq.AddColumn("MailboxSyncSettings.WarningCode").Name = "WarningCode";
			return esq;
		}

		/// <summary>
		/// Updates appointments last synchronization date.
		/// </summary>
		/// <param name="calendar"><see cref="Calendar"/> instance.</param>
		private void UpdateLastSyncDate(Calendar calendar) {
			var update = new Update(_userConnection, "ActivitySyncSettings")
				.Set("AppointmentLastSyncDate", Column.Parameter(calendar.LastSyncDateUtc))
				.Where("Id").IsEqual(Column.Parameter(calendar.Id));
			update.Execute();
		}

		private void UpdateCache(Calendar calendar) {
			ICacheStore applicationCache = _userConnection.ApplicationCache;
			var calendars = GetCalendarsFromCache(applicationCache);
			if (calendars != null) {
				calendars.RemoveAll(c => c.Id == calendar.Id);
				calendars.Add(calendar);
				applicationCache[IntegrationConsts.Calendar.CacheItemName] = calendars;
			}
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Filter by enabled calendars.
		/// </summary>
		/// <param name="calendars">Collection of <see cref="Calendar"/>`s.</param>
		/// <returns>Filtered collection of <see cref="Calendar"/>`s by enabled.</returns>
		protected List<Calendar> FilterEnabledCalendars(List<Calendar> calendars) {
			return calendars.Where(c => c.Settings.SyncEnabled).ToList();
		}

		/// <summary>
		/// Getting a collection of users calendars.
		/// </summary>
		/// <param name="importRequired">Import must be enabled.</param>
		/// <param name="exportRequired">Export must be enabled.</param>
		/// <returns>Collection of <see cref="Calendar"/>`s</returns>
		protected virtual List<Calendar> GetCalendars(bool importRequired = true, bool exportRequired = true) {
			ICacheStore applicationCache = _userConnection.ApplicationCache;
			var calendars = GetCalendarsFromCache(applicationCache);
			if (calendars == null) {
				var esq = GetCalendarEsq();
				var calendarsEntities = esq.GetEntityCollection(_userConnection);
				calendars = ConvertEntitiesToModels(calendarsEntities);
				if (ListenerUtils.GetIsFeatureEnabled(_userConnection, "IsCalendarCached")) {
					applicationCache[IntegrationConsts.Calendar.CacheItemName] = calendars;
				}
			}
			if (importRequired || exportRequired) {
				calendars = FilterEnabledCalendars(calendars);
			}
			return calendars;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ICalendarRepository.GetAllCalendars()"/>
		public List<Calendar> GetAllCalendars() {
			return GetCalendars(false, false);
		}

		/// <inheritdoc cref="ICalendarRepository.GetEnabledCalendars()"/>
		public List<Calendar> GetEnabledCalendars() {
			return GetCalendars(true, true);
		}

		/// <inheritdoc cref="ICalendarRepository.GetOwnerCalendar(string)"/>
		public Calendar GetOwnerCalendar(string email) {
			if (email.IsNullOrEmpty()) {
				return default;
			}
			var calendars = GetCalendars(true, true);
			return calendars.FirstOrDefault(c => c.IsOwnerCalendar(email));
		}

		/// <inheritdoc cref="ICalendarRepository.GetOwnerCalendar(Guid)"/>
		public Calendar GetOwnerCalendar(Guid ownerId) {
			var calendars = GetCalendars(true, true);
			return calendars.FirstOrDefault(c => c.OwnerId == ownerId);
		}

		/// <inheritdoc cref="ICalendarRepository.Save(Calendar)"/>
		public void Save(Calendar calendar) {
			UpdateLastSyncDate(calendar);
			UpdateCache(calendar);
		}

		#endregion

	}

	#endregion

}
