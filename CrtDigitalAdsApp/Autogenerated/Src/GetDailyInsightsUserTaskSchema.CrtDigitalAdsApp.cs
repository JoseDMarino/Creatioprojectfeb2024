namespace Terrasoft.Core.Process.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Process;

	#region Class: GetDailyInsightsUserTask

	/// <exclude/>
	public partial class GetDailyInsightsUserTask : ProcessUserTask
	{

		#region Constructors: Public

		public GetDailyInsightsUserTask(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("ed7b02c1-95a6-4033-b2a4-b1836a846f8d");
		}

		#endregion

		#region Methods: Public

		public override void WritePropertiesData(DataWriter writer) {
			writer.WriteStartObject(Name);
			base.WritePropertiesData(writer);
			if (Status == Core.Process.ProcessStatus.Inactive) {
				writer.WriteFinishObject();
				return;
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
			}
		}

		#endregion

	}

	#endregion

}

