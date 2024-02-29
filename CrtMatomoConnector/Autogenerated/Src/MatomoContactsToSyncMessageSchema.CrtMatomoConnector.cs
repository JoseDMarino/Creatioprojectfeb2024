namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: MatomoContactsToSyncMessageSchema

	/// <exclude/>
	public class MatomoContactsToSyncMessageSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public MatomoContactsToSyncMessageSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public MatomoContactsToSyncMessageSchema(MatomoContactsToSyncMessageSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd");
			Name = "MatomoContactsToSyncMessage";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,189,85,77,111,219,48,12,61,167,64,255,3,145,93,90,160,176,111,59,44,31,192,208,14,93,7,100,232,208,96,151,97,7,85,166,29,1,182,228,81,84,139,44,232,127,159,36,203,113,154,166,89,79,189,8,177,248,248,241,200,71,69,139,6,109,43,36,194,18,137,132,53,37,103,151,70,151,170,114,36,88,25,125,122,178,57,61,25,57,171,116,5,119,107,203,216,76,246,190,61,190,174,81,6,176,205,174,81,35,41,57,96,190,227,35,123,67,136,251,205,26,237,13,222,244,129,176,242,112,184,172,133,181,159,96,33,216,52,198,167,101,33,217,46,205,221,90,203,5,90,43,42,140,240,60,207,97,106,93,211,8,90,207,211,119,116,5,54,64,216,18,90,212,12,77,12,3,214,123,67,33,88,128,136,69,65,211,133,130,210,16,216,22,165,42,21,22,32,83,186,172,79,144,239,100,104,221,125,173,36,200,152,228,72,121,208,23,191,52,78,174,126,56,116,184,45,124,180,137,197,15,100,125,27,152,156,100,67,158,243,109,204,208,33,246,249,117,4,7,120,172,124,106,17,65,18,150,179,241,145,130,198,249,60,219,134,204,247,99,78,91,65,162,1,237,135,62,27,167,6,220,20,118,60,79,177,64,21,177,167,161,133,217,52,143,232,195,206,150,5,241,149,96,28,207,195,217,59,65,73,166,249,143,167,98,140,57,253,1,166,220,78,205,95,31,206,157,102,113,132,244,217,205,23,237,26,36,113,95,227,244,218,169,98,14,3,185,11,8,5,46,85,131,176,45,250,34,196,29,141,118,221,148,230,57,164,218,206,97,19,1,203,117,139,48,131,23,179,13,247,89,200,63,137,176,203,109,46,15,30,18,119,198,187,46,164,183,164,224,233,186,47,37,24,250,223,209,244,148,84,131,186,232,132,243,92,69,183,100,90,36,86,248,54,13,245,155,25,58,45,159,15,57,174,72,236,246,97,193,252,10,251,154,210,173,207,174,176,20,174,230,159,162,118,248,85,232,162,14,203,61,131,67,215,217,77,165,13,225,249,239,157,233,189,156,208,78,215,54,80,33,79,192,134,227,233,8,157,164,152,197,158,98,188,115,71,38,138,239,157,201,68,221,244,67,126,43,145,56,253,80,115,92,156,82,213,140,212,47,66,96,146,193,231,186,6,124,240,175,154,133,71,197,171,14,91,17,138,128,244,175,1,254,113,162,246,38,15,187,199,56,197,21,25,173,254,98,241,30,13,216,110,212,32,227,3,212,95,147,240,2,121,101,138,183,232,215,215,163,180,31,114,75,202,144,226,117,84,177,35,138,175,125,183,139,25,164,165,76,141,90,169,106,133,150,7,151,190,71,45,25,233,145,254,229,47,21,89,126,165,77,137,160,121,240,255,134,170,64,240,227,133,107,228,219,20,237,236,28,102,115,248,56,57,72,176,163,253,252,210,223,253,3,52,123,168,160,99,7,0,0 };
		}

		protected override void InitializeLocalizableStrings() {
			base.InitializeLocalizableStrings();
			SetLocalizableStringsDefInheritance();
			LocalizableStrings.Add(CreateLogEventDescriptionLocalizableString());
			LocalizableStrings.Add(CreateLogEventNameLocalizableString());
		}

		protected virtual SchemaLocalizableString CreateLogEventDescriptionLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("a89c84b0-070b-428c-4979-14366327c04d"),
				Name = "LogEventDescription",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd"),
				ModifiedInSchemaUId = new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd")
			};
			return localizableString;
		}

		protected virtual SchemaLocalizableString CreateLogEventNameLocalizableString() {
			SchemaLocalizableString localizableString = new SchemaLocalizableString() {
				UId = new Guid("135b87ef-075b-19ea-9095-ecfb6acc3446"),
				Name = "LogEventName",
				CreatedInPackageId = new Guid("992bffc1-0ec1-4459-b82e-3bbf78f3508e"),
				CreatedInSchemaUId = new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd"),
				ModifiedInSchemaUId = new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd")
			};
			return localizableString;
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d7b976bd-072c-41be-8958-9e83f3f3b6dd"));
		}

		#endregion

	}

	#endregion

}

