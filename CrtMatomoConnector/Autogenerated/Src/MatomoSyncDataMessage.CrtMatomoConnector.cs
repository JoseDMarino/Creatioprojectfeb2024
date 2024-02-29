 namespace Terrasoft.Configuration
{

	#region Class: MatomoSyncSessionMessage

	/// <summary>
	/// Class to represent matomo sync data action message.
	/// </summary>
	public class MatomoSyncDataMessage : MatomoTouchQueueMessage
	{

		#region Constructors: Public

		/// <summary>
		/// Constructor for <see cref="MatomoSyncDataMessage"/>.
		/// </summary>
		public MatomoSyncDataMessage() {
			Type = TouchQueueMessageType.Sync;
			RequiresDeduplication = true;
		}

		#endregion

		#region Methods: Public

		/// <summary>
		/// Defines number of retries in case of crash or error.
		/// </summary>
		/// <returns>Number of retries.</returns>
		public override int GetMaxRetryCount() => 3;

		#endregion

	}

	#endregion

}

