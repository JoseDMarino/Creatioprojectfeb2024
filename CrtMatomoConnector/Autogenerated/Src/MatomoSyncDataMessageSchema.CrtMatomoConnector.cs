namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoSyncDataMessageSchema

	/// <exclude/>
	public class MatomoSyncDataMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoSyncDataMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoSyncDataMessageSchema(MatomoSyncDataMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec");
			Name = "MatomoSyncDataMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,146,77,110,194,48,16,133,215,32,113,135,17,221,180,155,100,209,93,129,108,64,234,42,85,11,92,192,56,19,176,68,60,233,216,174,26,161,222,189,19,135,240,211,82,169,139,40,242,243,155,231,249,198,6,171,42,116,181,210,8,107,100,86,142,74,159,204,201,150,102,27,88,121,67,118,52,60,140,134,163,225,224,142,113,43,75,152,239,149,115,79,144,43,79,21,173,26,171,87,232,156,108,228,242,83,91,140,222,52,77,97,234,66,85,41,110,178,227,58,214,129,39,96,172,25,29,90,15,85,204,0,39,33,80,40,175,64,233,246,68,168,186,168,164,79,74,47,162,234,176,217,27,13,58,166,157,155,88,72,249,177,3,232,155,91,83,208,187,183,128,1,79,189,13,58,150,51,12,89,231,57,104,79,44,76,175,49,187,115,252,68,232,24,206,118,40,229,155,58,68,208,140,229,108,124,179,149,113,154,37,167,176,75,138,30,227,102,213,253,3,28,90,207,96,221,212,8,51,248,197,209,234,73,91,52,137,182,37,190,7,35,35,93,96,17,106,73,141,215,38,117,210,41,70,199,215,17,26,109,209,113,95,15,33,71,191,163,226,63,252,11,44,141,69,7,54,84,27,100,160,82,46,211,179,17,197,88,208,202,97,43,105,121,69,59,144,233,200,123,34,254,131,63,42,82,28,216,186,236,229,103,92,50,77,251,189,139,97,209,135,36,154,2,229,48,15,207,232,115,245,185,20,127,51,167,96,189,12,109,150,193,227,228,38,106,55,128,107,81,180,111,189,127,150,122,253,2,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("9fcd1454-9111-4bd8-1172-03341e16d756"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec"),
				ModifiedInSchemaUId = new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("c3de1b6f-2793-89f3-a694-65861d1a02fb"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec"),
				ModifiedInSchemaUId = new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0819bdf9-1032-4f16-9e3f-1dc2ab1075ec"));
		}

		#endregion

	}

	#endregion

}

