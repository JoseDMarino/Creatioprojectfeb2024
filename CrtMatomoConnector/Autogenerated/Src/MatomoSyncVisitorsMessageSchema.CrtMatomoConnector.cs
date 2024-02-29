namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoSyncVisitorsMessageSchema

	/// <exclude/>
	public class MatomoSyncVisitorsMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoSyncVisitorsMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoSyncVisitorsMessageSchema(MatomoSyncVisitorsMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd");
			Name = "MatomoSyncVisitorsMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,85,61,111,219,48,16,157,29,32,255,225,224,46,54,16,72,91,135,216,214,146,20,169,11,184,112,17,35,75,209,129,166,78,50,1,137,20,200,99,12,195,200,127,47,73,125,57,174,132,102,204,66,72,119,239,222,189,251,160,36,89,137,166,98,28,97,135,90,51,163,50,138,30,148,204,68,110,53,35,161,228,237,205,249,246,102,98,141,144,57,60,159,12,97,185,184,122,119,248,162,64,238,193,38,122,66,137,90,240,30,243,19,143,228,28,158,247,135,81,210,57,156,235,139,198,220,193,225,161,96,198,220,195,134,145,42,213,243,73,242,23,97,4,41,109,54,104,12,203,49,128,227,56,134,165,177,101,201,244,41,105,222,67,32,144,2,141,149,70,131,146,160,12,36,96,28,11,164,140,24,176,32,9,202,154,10,50,165,225,181,161,7,83,33,23,153,192,52,106,19,196,23,25,42,187,47,4,7,30,146,140,138,131,86,248,78,89,126,248,101,209,98,39,123,114,14,210,251,66,93,11,72,91,238,195,239,97,27,248,107,196,117,117,117,121,61,60,232,94,26,68,224,26,179,213,116,84,206,52,78,162,142,48,190,102,92,86,76,179,18,164,27,247,106,218,182,97,154,108,84,138,133,1,149,245,173,161,186,135,209,50,14,33,195,12,134,152,166,71,70,56,77,252,217,6,65,166,85,249,159,72,65,184,78,93,106,119,248,188,237,216,156,121,56,119,51,140,209,186,103,235,111,210,150,168,217,190,192,101,24,69,40,42,233,42,186,3,47,113,39,74,132,78,246,157,103,158,76,46,67,133,164,4,26,117,115,56,7,192,238,84,33,172,224,159,1,123,123,228,181,44,2,172,85,228,160,109,210,218,241,92,211,57,123,67,220,152,91,25,222,209,62,7,215,91,179,54,40,211,122,115,222,175,209,86,171,10,53,9,252,200,18,13,47,195,111,127,11,27,158,211,236,17,51,102,11,122,97,133,197,239,76,166,133,191,178,43,24,50,71,235,92,42,141,243,63,23,67,25,107,125,215,144,51,228,72,11,48,254,120,251,60,90,195,172,219,225,124,34,141,221,162,246,27,50,160,110,108,59,54,72,7,149,126,100,53,156,30,33,221,125,171,180,80,90,208,201,95,68,110,181,14,223,209,122,197,35,104,118,221,192,81,208,1,14,34,63,160,161,62,228,40,138,2,246,232,12,138,59,36,166,144,9,109,104,228,27,212,20,168,94,221,95,70,164,8,110,2,240,132,180,109,216,102,115,88,37,240,117,49,88,96,93,246,123,163,179,253,5,215,231,12,191,187,6,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("c1d5437e-f888-dc21-3319-d6a9fbfb09e1"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd"),
				ModifiedInSchemaUId = new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("9fe6ebe2-99ee-3a5c-c1c6-952230c33d0a"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd"),
				ModifiedInSchemaUId = new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("72210611-e9cf-47ba-b0c3-465f09cb4cdd"));
		}

		#endregion

	}

	#endregion

}

