namespace Terrasoft.Configuration
{
	using System;

	#region Class: MatomoTouchQueueMessage

	/// <summary>
	/// Class to represent base matomo touch queue action message. 
	/// </summary>
	public class MatomoTouchQueueMessage : TouchQueueMessage
	{

		#region Constants: Private

		/// <summary>
		/// Unique identifier of Matomo Analytics tracking system <see cref="TouchEventTracking"/> record.
		/// </summary>
		private const string MatomoTrackingId = "8BAF47F2-4DD9-4AB1-BB27-EF949D03E594";

		#endregion

		#region Fields: Private

		private Guid _matomoTrackingId;

		#endregion

		#region Constructors: Public

		/// <summary>
		/// Constructor for <see cref="MatomoTouchQueueMessage"/>.
		/// </summary>
		public MatomoTouchQueueMessage() {
			_matomoTrackingId = Guid.Parse(MatomoTrackingId);
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc/>
		public override Guid GetTracking() => _matomoTrackingId;

		#endregion

	}

	#endregion

}

