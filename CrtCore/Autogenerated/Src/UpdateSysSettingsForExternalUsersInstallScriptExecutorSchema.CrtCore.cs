namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UpdateSysSettingsForExternalUsersInstallScriptExecutorSchema

	/// <exclude/>
	public class UpdateSysSettingsForExternalUsersInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UpdateSysSettingsForExternalUsersInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UpdateSysSettingsForExternalUsersInstallScriptExecutorSchema(UpdateSysSettingsForExternalUsersInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("0e1be858-f9fe-4d6b-a4d3-b2c71df20e6b");
			Name = "UpdateSysSettingsForExternalUsersInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("1296b383-c2ef-47b8-ae67-0601cddb70e1");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,205,85,201,110,219,48,16,61,43,64,254,129,240,73,1,12,127,64,179,0,137,151,212,128,99,4,149,157,251,152,26,73,44,40,82,229,34,71,48,252,239,29,73,150,235,184,206,169,10,80,64,27,57,143,111,22,142,30,21,228,104,11,224,200,86,104,12,88,157,184,209,88,171,68,164,222,128,19,90,93,95,237,174,175,2,111,133,74,89,84,89,135,57,217,165,68,94,27,237,232,25,21,26,193,111,143,152,83,26,131,159,205,143,166,202,9,39,208,18,128,32,133,223,72,193,25,151,96,45,91,23,49,56,36,95,17,58,71,75,237,76,155,233,187,67,163,64,174,45,26,59,87,214,129,148,17,55,162,112,211,119,228,222,105,243,141,205,47,206,19,251,174,241,209,57,41,181,136,89,107,197,176,230,163,116,85,155,14,243,31,134,55,172,78,61,8,74,48,204,254,9,103,33,172,187,103,10,183,172,254,186,155,136,6,12,166,186,179,206,16,96,200,244,230,39,49,60,60,28,214,7,53,246,115,88,135,10,118,108,48,214,49,14,134,108,240,34,212,43,213,98,171,77,188,64,149,186,108,176,111,65,251,97,15,148,81,129,92,128,28,103,64,217,122,229,122,37,95,250,188,238,136,175,33,95,232,45,26,14,22,191,134,126,93,20,189,211,83,139,205,160,20,92,171,153,209,249,73,91,247,66,189,196,109,148,161,148,255,78,214,213,224,59,53,181,54,213,15,228,52,232,175,6,207,90,167,18,87,144,190,128,130,20,205,127,93,216,137,176,176,145,248,24,151,160,56,198,111,194,122,144,211,36,33,124,15,236,199,63,69,187,233,47,34,94,233,90,134,22,58,21,234,140,124,127,219,188,18,82,76,224,25,11,63,42,17,41,85,44,26,221,18,234,92,160,58,237,10,26,161,173,78,204,135,137,251,51,177,107,21,185,138,120,134,57,28,246,136,212,221,181,211,79,213,146,14,138,112,112,90,229,225,185,92,182,193,6,34,97,225,185,187,209,12,29,207,234,125,154,60,133,23,50,184,57,198,27,252,181,148,70,116,226,248,92,189,129,244,20,195,220,70,209,235,99,9,66,214,123,68,97,56,227,177,115,126,97,57,148,24,118,230,182,188,205,179,126,208,77,215,111,172,235,147,49,4,7,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("0e1be858-f9fe-4d6b-a4d3-b2c71df20e6b"));
		}

		#endregion

	}

	#endregion

}

