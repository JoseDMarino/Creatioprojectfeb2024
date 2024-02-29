namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoImportByFormSubmitMessageSchema

	/// <exclude/>
	public class MatomoImportByFormSubmitMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoImportByFormSubmitMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoImportByFormSubmitMessageSchema(MatomoImportByFormSubmitMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("8760db92-f330-469c-a162-74020a454f25");
			Name = "MatomoImportByFormSubmitMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,197,85,75,111,219,48,12,62,167,192,254,3,145,93,90,160,112,176,29,151,38,135,165,143,117,64,135,110,125,92,134,29,20,155,118,4,88,146,75,73,237,140,160,255,125,180,228,56,110,150,46,237,165,187,4,49,31,159,62,146,31,37,45,20,218,74,164,8,215,72,36,172,201,93,50,51,58,151,133,39,225,164,209,239,246,150,239,246,6,222,74,93,192,85,109,29,170,113,247,253,13,31,156,209,33,231,171,53,154,29,236,122,79,88,112,30,204,74,97,237,39,184,16,206,40,115,174,42,67,238,115,125,106,72,93,249,185,146,238,2,173,21,5,134,148,209,104,4,71,214,43,37,168,158,182,223,33,29,156,1,194,138,208,162,118,160,2,20,100,194,9,144,1,16,230,53,228,12,9,54,96,130,72,27,202,160,34,118,178,130,30,245,176,43,63,47,101,10,105,128,223,65,14,86,244,175,141,79,23,223,61,122,236,104,15,150,129,250,186,92,110,132,35,159,58,67,92,245,101,56,37,70,108,86,23,203,227,112,114,33,28,114,50,28,128,8,41,97,62,25,238,32,53,28,77,147,14,118,180,137,123,84,9,18,10,52,143,117,50,76,141,118,220,146,243,108,56,157,197,191,224,181,188,243,8,50,227,134,202,92,98,56,93,193,140,176,153,118,114,52,10,249,219,225,238,165,149,204,183,129,139,28,161,181,48,220,191,51,189,197,126,90,243,185,51,7,127,59,36,45,202,27,42,135,211,211,222,144,61,149,79,50,219,137,238,104,219,254,153,151,25,116,29,57,4,158,86,163,225,174,168,206,18,201,30,54,208,131,65,107,235,145,57,96,85,204,133,197,253,3,88,134,152,235,186,66,152,192,95,26,105,236,73,228,51,14,129,179,213,225,28,221,17,137,174,72,254,118,197,133,3,58,94,253,128,155,64,141,189,145,99,116,157,172,185,177,167,199,52,186,127,224,157,151,188,65,199,152,249,138,27,21,182,154,3,89,172,24,34,30,91,33,163,206,162,150,159,10,251,146,76,133,228,36,190,80,214,78,72,109,87,245,241,148,129,245,209,83,119,51,134,228,68,85,174,126,94,200,63,155,219,164,61,183,222,63,198,92,248,210,221,138,210,227,23,161,179,178,25,200,4,182,153,147,243,66,27,194,131,95,61,93,132,185,175,91,191,132,2,221,24,108,243,243,248,146,74,212,166,212,255,119,65,173,36,55,37,243,234,194,86,91,248,214,148,91,17,191,154,239,3,206,227,101,207,210,126,67,210,253,245,218,194,249,185,181,185,64,183,48,217,75,118,134,249,72,141,22,42,146,134,164,171,193,228,144,122,162,240,228,181,47,25,180,119,10,119,65,186,5,44,100,177,64,235,214,41,15,178,44,97,142,108,48,41,71,98,6,185,36,235,158,105,83,91,160,185,231,39,159,95,2,144,124,210,25,186,203,22,141,111,182,201,20,62,124,28,111,173,240,177,121,227,159,152,216,242,7,163,109,175,8,71,8,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("1c165e98-e3f7-76ad-2cce-2cc180f66e8c"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("8760db92-f330-469c-a162-74020a454f25"),
				ModifiedInSchemaUId = new Guid("8760db92-f330-469c-a162-74020a454f25")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("83436f7d-b633-7b1b-1635-dd914b72a8de"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("8760db92-f330-469c-a162-74020a454f25"),
				ModifiedInSchemaUId = new Guid("8760db92-f330-469c-a162-74020a454f25")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("8760db92-f330-469c-a162-74020a454f25"));
		}

		#endregion

	}

	#endregion

}

