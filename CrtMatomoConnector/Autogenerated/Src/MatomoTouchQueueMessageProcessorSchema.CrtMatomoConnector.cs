namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoTouchQueueMessageProcessorSchema

	/// <exclude/>
	public class MatomoTouchQueueMessageProcessorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoTouchQueueMessageProcessorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoTouchQueueMessageProcessorSchema(MatomoTouchQueueMessageProcessorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967");
			Name = "MatomoTouchQueueMessageProcessor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,87,91,111,26,57,20,126,158,74,253,15,71,244,97,7,9,193,123,3,84,106,210,84,145,154,54,187,208,221,135,170,90,153,153,3,88,153,177,169,47,180,108,148,255,190,199,30,155,203,64,152,208,72,188,36,112,108,159,239,59,223,185,216,8,86,162,94,176,12,97,140,74,49,45,167,166,123,41,197,148,207,172,98,134,75,241,250,213,195,235,87,137,213,92,204,96,180,210,6,203,139,218,119,218,95,20,152,185,205,186,251,17,5,42,158,109,246,108,187,85,72,118,90,121,163,112,70,187,225,178,96,90,191,133,91,102,100,41,199,210,102,243,63,45,90,188,69,173,217,12,239,148,204,232,147,84,254,76,175,215,131,190,182,101,201,212,106,24,190,135,29,168,193,65,21,8,102,181,192,28,202,234,60,76,149,44,233,12,34,100,10,167,131,214,6,161,213,27,118,163,207,222,150,211,111,155,45,107,244,239,100,95,216,73,193,51,200,28,223,70,186,240,22,222,51,141,199,226,73,30,124,76,27,33,72,58,163,108,102,164,34,61,238,60,90,181,163,30,182,55,220,8,110,56,43,248,127,20,57,3,129,63,129,211,121,38,40,139,114,10,102,142,219,81,55,209,37,45,170,192,186,107,188,94,29,176,191,96,138,149,32,168,88,6,45,171,81,17,97,81,165,188,53,188,180,74,161,48,224,236,144,173,23,186,253,158,63,229,157,4,1,155,184,164,95,119,124,195,46,84,155,148,157,144,178,105,221,252,0,143,65,79,20,121,37,233,174,190,4,176,64,101,56,214,212,93,40,190,100,6,3,175,43,102,216,104,37,178,91,38,136,148,130,127,203,67,230,139,189,120,234,231,14,91,93,27,37,201,12,13,12,134,79,184,134,119,239,32,125,98,105,224,243,124,208,117,77,181,118,219,83,76,244,81,168,1,44,89,97,209,111,109,80,239,22,205,92,230,78,58,37,13,97,96,190,174,205,62,23,115,234,119,147,203,12,122,85,166,227,30,144,75,106,125,158,35,80,105,187,73,240,17,205,23,202,130,159,42,35,155,185,140,143,241,151,73,125,179,48,125,191,93,17,177,135,59,84,217,134,138,202,10,211,14,10,46,153,2,67,231,40,130,221,184,105,246,152,79,50,115,125,193,38,5,142,60,106,234,106,86,78,211,166,202,107,119,188,243,164,117,83,46,164,50,213,246,17,45,237,146,109,5,109,21,26,171,68,136,172,123,45,21,137,156,58,86,157,64,118,91,215,23,232,116,205,120,129,121,163,76,81,155,64,235,76,186,108,200,181,126,43,222,137,148,5,92,50,241,89,26,62,93,185,90,157,178,66,135,59,162,154,59,91,110,170,105,118,124,178,182,122,195,103,13,77,82,154,218,126,182,130,41,141,107,150,231,78,118,102,115,142,110,132,6,77,245,31,176,168,252,186,20,31,158,141,7,130,90,74,158,123,172,81,192,72,99,114,162,33,180,242,21,247,217,33,87,253,49,93,91,29,184,182,34,235,239,197,230,59,96,56,12,62,146,111,238,138,163,188,141,21,203,238,137,88,149,150,247,171,127,112,50,151,242,62,28,106,127,15,32,199,124,166,169,27,166,241,132,147,63,96,248,22,139,151,232,0,182,118,1,211,112,28,249,34,184,8,149,120,112,94,117,221,231,185,146,130,114,177,62,159,6,64,247,250,48,44,51,55,121,39,114,232,70,72,87,215,206,30,48,146,184,126,71,127,190,170,34,244,102,242,24,139,54,138,85,177,136,132,93,183,142,236,164,228,230,12,106,53,64,159,46,215,198,197,49,197,42,71,127,115,205,233,73,65,11,117,197,170,245,168,103,180,126,248,101,80,9,86,52,107,233,40,57,154,103,83,176,6,120,170,110,233,51,226,9,106,233,179,198,84,3,61,57,174,152,186,232,103,147,204,17,55,120,147,111,27,12,83,134,124,97,131,22,161,156,244,95,248,195,162,62,95,147,28,198,61,85,17,234,143,154,163,166,220,199,237,99,159,144,179,135,187,3,123,250,60,168,122,88,239,15,3,189,215,244,207,169,8,255,255,241,101,175,151,15,75,250,57,240,153,222,23,233,158,110,155,7,203,192,251,73,158,243,84,137,116,105,209,221,148,105,187,235,156,119,160,245,73,206,214,88,254,9,242,2,190,87,168,51,197,23,142,197,211,180,171,231,168,66,109,11,243,27,111,174,198,64,182,72,236,62,169,106,79,243,202,186,107,36,219,255,226,73,207,111,205,15,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateImportMatomoSessionFailedTextLocalizableString());
			LocalizableStrings.Add(CreateImportMatomoSessionSuccessTextLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateImportMatomoSessionFailedTextLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("f96b58c3-bf12-9267-cdbe-281878d147a3"),
				Name = "ImportMatomoSessionFailedText",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967"),
				ModifiedInSchemaUId = new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateImportMatomoSessionSuccessTextLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("0f7bb024-1fd4-ca96-3d90-af75b4263a67"),
				Name = "ImportMatomoSessionSuccessText",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967"),
				ModifiedInSchemaUId = new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("2c6febba-600c-4d2f-85b5-2e3db920f967"));
		}

		#endregion

	}

	#endregion

}

