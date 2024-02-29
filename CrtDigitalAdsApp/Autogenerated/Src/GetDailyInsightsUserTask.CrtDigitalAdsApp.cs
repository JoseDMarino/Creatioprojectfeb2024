namespace Terrasoft.Core.Process.Configuration
{
	using CrtDigitalAdsApp.Utilities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;

	#region Class: GetDailyInsights

	/// <exclude/>
	public partial class GetDailyInsightsUserTask
	{

		#region Methods: Protected

		protected override bool InternalExecute(ProcessExecutingContext context) {
			var provider = ClassFactory.Get<DailyInsightProvider>();
			return provider.Actualize();
		}

		#endregion

		#region Methods: Public

		public override void CancelExecuting(params object[] parameters) {
			base.CancelExecuting(parameters);
		}

		public override bool CompleteExecuting(params object[] parameters) {
			return base.CompleteExecuting(parameters);
		}

		public override string GetExecutionData() {
			return string.Empty;
		}

		public override ProcessElementNotification GetNotificationData() {
			return base.GetNotificationData();
		}

		#endregion

	}

	#endregion
}