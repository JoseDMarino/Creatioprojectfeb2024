namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoContactsRequestMessageSchema

	/// <exclude/>
	public class MatomoContactsRequestMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoContactsRequestMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoContactsRequestMessageSchema(MatomoContactsRequestMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca");
			Name = "MatomoContactsRequestMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,203,78,195,48,16,60,183,82,255,97,85,46,112,73,14,220,232,227,210,74,168,135,32,168,250,3,174,179,105,45,53,118,240,174,17,81,197,191,179,113,154,190,128,138,131,15,30,207,142,103,198,182,170,68,170,148,70,88,161,247,138,92,193,201,204,217,194,108,130,87,108,156,29,244,247,131,254,160,223,187,243,184,145,45,204,118,138,232,9,50,197,174,116,194,100,165,153,150,248,30,144,56,67,34,181,193,200,79,211,20,198,20,202,82,249,122,122,216,199,89,96,7,30,43,143,132,150,161,140,58,160,15,66,205,33,213,86,195,186,134,64,232,23,185,112,163,54,148,173,120,210,105,167,103,226,85,88,239,140,6,29,245,111,89,131,206,249,202,5,189,125,11,24,240,104,186,215,6,61,37,117,150,216,7,205,206,75,224,215,120,69,203,184,206,214,134,59,209,161,144,53,38,68,208,30,139,201,240,150,163,97,58,77,142,154,231,153,186,80,183,134,239,31,96,223,80,123,171,186,66,152,192,143,84,13,158,44,202,202,121,30,69,98,51,111,164,251,57,230,161,18,249,248,198,50,41,206,49,50,190,14,37,160,205,219,30,46,75,201,144,183,46,255,79,31,115,44,140,69,2,27,202,53,122,112,133,188,36,123,35,136,177,160,21,97,3,105,249,114,91,144,182,228,243,57,255,71,17,17,145,225,224,45,77,95,174,229,146,113,218,157,157,181,230,62,68,209,228,40,151,49,60,35,103,234,115,41,252,122,230,130,101,169,109,50,133,199,209,175,81,219,2,46,65,193,190,1,239,92,227,179,41,3,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("5301d476-7fad-f422-6cc1-af9010fb0c89"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca"),
				ModifiedInSchemaUId = new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("1c577ef4-80ae-df02-f56c-a0fd421199f0"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca"),
				ModifiedInSchemaUId = new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("5dfa5bae-0220-4b3b-8719-21bfb00da2ca"));
		}

		#endregion

	}

	#endregion

}

