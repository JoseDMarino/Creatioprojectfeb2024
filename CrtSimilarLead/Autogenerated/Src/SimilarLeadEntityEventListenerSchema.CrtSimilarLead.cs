namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: SimilarLeadEntityEventListenerSchema

	/// <exclude/>
	public class SimilarLeadEntityEventListenerSchema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public SimilarLeadEntityEventListenerSchema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public SimilarLeadEntityEventListenerSchema(SimilarLeadEntityEventListenerSchema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("7c236e34-22b6-4815-bdb0-6718c56a3063");
			Name = "SimilarLeadEntityEventListener";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("364d74c1-c4ce-4435-a41a-0f64d57d716d");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,133,83,193,106,219,64,16,61,59,144,127,24,212,139,13,101,117,119,108,67,19,66,147,144,180,5,251,86,122,24,75,35,123,139,180,107,118,215,2,19,250,239,157,29,73,70,54,18,62,238,188,55,51,111,222,204,130,193,138,252,1,51,130,13,57,135,222,22,65,61,89,83,232,221,209,97,208,214,220,223,125,222,223,77,142,94,155,29,172,79,62,80,197,120,89,82,22,65,175,190,147,33,167,179,135,51,167,95,166,170,172,25,71,212,155,31,131,29,169,103,19,116,208,228,111,18,212,115,77,38,68,30,51,191,56,218,177,46,120,42,209,251,57,172,117,165,75,116,239,132,185,208,79,194,125,215,60,6,203,150,140,52,77,97,225,143,85,133,238,180,106,223,29,1,10,235,24,36,130,204,81,177,76,122,229,146,116,5,36,37,129,164,191,234,106,165,87,197,98,62,150,222,182,53,30,209,211,128,150,4,210,152,240,123,0,154,174,179,61,85,248,131,55,5,75,136,11,179,197,180,39,101,54,251,195,153,135,227,182,212,25,100,113,240,27,115,195,28,70,84,112,157,79,113,229,108,228,7,133,189,205,217,202,95,78,215,24,168,69,201,228,13,97,140,45,106,26,240,218,97,9,188,160,201,75,242,183,221,237,17,126,154,87,227,201,5,62,6,33,68,229,234,220,33,189,110,177,56,160,195,74,12,91,38,158,21,179,203,43,25,23,154,151,90,164,66,25,206,160,100,181,217,83,191,255,102,62,122,130,162,245,145,248,94,72,58,124,115,59,31,87,10,218,248,128,134,191,87,102,77,64,109,226,33,135,61,117,29,101,6,200,49,224,133,152,118,153,182,230,126,58,39,168,173,206,161,55,254,212,110,255,242,15,108,231,248,10,131,253,129,102,16,255,238,100,178,229,109,171,126,122,151,71,179,7,33,212,232,58,191,151,112,113,91,13,83,88,255,6,119,223,68,47,131,18,251,15,80,176,185,125,91,4,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("7c236e34-22b6-4815-bdb0-6718c56a3063"));
		}

		#endregion

	}

	#endregion

}

