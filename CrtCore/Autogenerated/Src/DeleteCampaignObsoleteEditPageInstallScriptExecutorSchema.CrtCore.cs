namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DeleteCampaignObsoleteEditPageInstallScriptExecutorSchema

	/// <exclude/>
	public class DeleteCampaignObsoleteEditPageInstallScriptExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DeleteCampaignObsoleteEditPageInstallScriptExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DeleteCampaignObsoleteEditPageInstallScriptExecutorSchema(DeleteCampaignObsoleteEditPageInstallScriptExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("d4918a44-2bf5-4de4-958e-cfa8c88b5c33");
			Name = "DeleteCampaignObsoleteEditPageInstallScriptExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("d2c3f70d-d3a5-4d15-9bc6-62f67312edb1");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,141,82,93,111,218,48,20,125,78,127,197,109,158,130,84,34,24,76,133,177,238,33,225,67,60,116,157,68,247,60,25,231,2,158,28,59,178,29,182,172,226,191,207,177,105,27,74,250,225,135,40,246,61,231,220,175,3,130,228,168,11,66,17,238,81,41,162,229,198,196,169,20,27,182,45,21,49,76,10,120,184,0,123,74,205,196,22,86,149,54,152,79,206,94,44,133,115,164,53,94,199,11,20,168,24,109,65,45,184,92,19,206,254,57,225,102,188,153,59,207,95,143,41,124,61,18,207,132,97,134,161,158,92,56,76,81,174,57,163,160,141,205,70,129,114,162,53,164,36,47,8,219,138,31,100,139,250,216,217,25,84,33,201,164,224,21,44,74,150,193,29,207,30,89,112,3,2,255,184,231,40,252,60,238,39,189,126,127,212,77,6,105,218,29,142,62,13,187,201,48,25,116,103,233,188,215,187,78,199,253,193,104,28,118,124,185,135,147,138,124,41,83,228,104,240,81,250,110,173,101,125,159,101,204,212,197,45,133,45,134,243,21,85,172,48,179,191,72,75,35,21,124,129,101,123,224,169,19,120,250,41,20,219,19,131,176,151,182,137,95,217,155,217,162,159,26,149,221,186,240,43,180,227,109,94,59,86,62,8,2,55,222,106,69,119,152,147,91,34,44,77,1,182,188,221,188,160,199,45,196,201,179,32,232,74,223,202,172,228,174,26,203,110,209,180,150,50,30,157,84,223,173,97,163,112,213,36,133,87,47,43,118,250,123,162,128,74,97,1,181,45,143,203,155,50,135,32,170,250,170,141,178,46,186,2,185,254,109,105,223,124,151,193,3,132,203,204,42,158,88,37,110,186,224,80,227,14,46,5,176,13,68,151,39,29,196,115,52,116,55,87,50,159,38,209,115,250,206,113,138,129,181,151,41,149,240,116,175,114,202,247,198,136,142,214,241,246,105,172,213,123,200,109,213,175,255,3,219,131,198,49,59,166,227,247,252,112,54,206,70,41,238,123,248,15,191,206,60,178,57,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("d4918a44-2bf5-4de4-958e-cfa8c88b5c33"));
		}

		#endregion

	}

	#endregion

}

