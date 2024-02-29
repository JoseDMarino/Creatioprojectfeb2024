namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: UseNewShellForExternalUserExecutorSchema

	/// <exclude/>
	public class UseNewShellForExternalUserExecutorSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public UseNewShellForExternalUserExecutorSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public UseNewShellForExternalUserExecutorSchema(UseNewShellForExternalUserExecutorSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("cea82800-75eb-41a8-84e3-85d056e2f5e9");
			Name = "UseNewShellForExternalUserExecutor";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("e4ab0bc5-d9f5-40fb-aa3b-d9475e1e35c3");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,125,81,203,106,2,49,20,93,143,224,63,92,210,77,2,50,31,160,116,209,90,11,179,104,177,76,165,235,56,115,213,64,76,166,247,38,62,40,254,123,51,15,161,46,234,34,9,247,156,251,58,39,78,239,145,27,93,33,124,34,145,102,191,9,249,220,187,141,217,70,210,193,120,55,30,253,140,71,89,100,227,182,55,41,132,179,127,240,252,229,185,165,32,157,38,174,173,169,160,178,154,25,86,140,239,120,44,119,104,237,171,167,197,41,32,57,109,19,74,188,56,97,21,131,39,152,66,81,56,14,218,218,178,34,211,132,43,145,122,165,53,210,157,61,16,110,211,90,240,134,97,231,107,158,194,178,155,209,147,195,188,131,55,53,244,165,40,219,1,73,145,195,170,149,3,241,38,84,208,170,203,178,85,83,235,128,16,187,231,35,34,157,225,17,28,30,161,39,228,109,213,4,68,121,230,18,67,72,234,89,168,174,69,150,39,64,138,130,203,114,249,116,208,198,234,181,69,49,129,185,183,113,239,90,83,57,200,64,17,213,53,255,107,135,132,82,204,125,141,66,229,5,47,190,163,182,114,200,95,106,74,95,147,60,146,226,142,113,66,41,208,60,108,57,235,250,254,209,144,95,61,80,29,117,25,28,68,87,247,38,118,113,66,179,241,232,242,11,76,52,168,73,9,2,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("cea82800-75eb-41a8-84e3-85d056e2f5e9"));
		}

		#endregion

	}

	#endregion

}

