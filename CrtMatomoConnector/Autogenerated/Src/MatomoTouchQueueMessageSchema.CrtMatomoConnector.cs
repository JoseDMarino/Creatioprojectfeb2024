namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoTouchQueueMessageSchema

	/// <exclude/>
	public class MatomoTouchQueueMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoTouchQueueMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoTouchQueueMessageSchema(MatomoTouchQueueMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4");
			Name = "MatomoTouchQueueMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,147,65,111,26,49,16,133,207,139,196,127,24,209,75,114,128,77,211,173,82,66,65,130,0,81,14,72,169,74,207,149,227,157,5,43,172,77,199,54,18,138,242,223,51,246,110,210,13,9,105,15,123,240,216,243,252,222,231,89,45,74,180,91,33,17,150,72,36,172,41,92,239,202,232,66,173,60,9,167,140,110,183,30,218,173,196,91,165,87,240,115,111,29,150,131,118,139,43,159,8,87,188,13,87,27,97,237,37,44,132,51,165,89,26,47,215,63,60,122,92,160,181,98,133,241,104,154,166,240,221,250,178,20,180,31,213,235,216,6,206,0,225,150,208,162,118,112,39,44,66,25,117,120,131,133,224,79,80,2,33,131,15,40,43,197,30,60,43,166,13,201,173,191,219,40,9,50,170,30,241,2,151,240,142,191,228,33,122,252,155,199,104,235,132,118,156,233,150,212,78,184,42,195,155,16,177,240,75,43,246,8,42,103,255,170,80,72,96,138,250,122,24,107,177,217,59,37,57,37,9,121,31,248,217,200,143,101,16,65,18,22,195,78,52,52,219,113,251,178,62,212,73,71,204,68,26,202,123,47,215,54,147,38,219,202,21,200,96,20,172,163,160,92,71,174,53,110,114,24,66,231,219,100,60,207,46,230,231,221,108,58,237,119,179,241,228,115,119,50,57,191,232,206,230,253,172,63,61,251,50,251,218,207,58,131,58,61,234,188,2,240,154,198,92,225,38,63,68,241,236,224,218,171,28,126,151,7,87,255,67,49,242,37,47,157,161,160,27,223,237,3,194,141,227,80,240,215,96,119,228,153,25,224,49,114,213,144,28,233,59,57,133,48,233,73,242,38,17,195,12,81,123,183,130,44,158,28,162,62,29,132,174,199,143,83,47,208,173,77,254,94,96,165,215,72,202,229,70,166,77,147,102,199,191,35,207,85,5,249,26,95,230,131,109,14,71,255,75,189,114,245,186,200,181,39,8,160,123,150,245,3,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("18e1b0ac-5d75-1d51-20ac-4733f64038f1"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4"),
				ModifiedInSchemaUId = new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("be1c70d6-2cc2-dcdb-93c6-69816a39f88a"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4"),
				ModifiedInSchemaUId = new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("6f1be96b-e617-4f38-98f2-c9ab7b7083f4"));
		}

		#endregion

	}

	#endregion

}

