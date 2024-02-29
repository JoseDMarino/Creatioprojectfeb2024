namespace Terrasoft.Configuration
{

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;

	#region Class: DummyForCrtC360InUiV2Schema

	/// <exclude/>
	public class DummyForCrtC360InUiV2Schema : Terrasoft.Core.SourceCodeSchema
	{

		#region Constructors: Public

		public DummyForCrtC360InUiV2Schema(SourceCodeSchemaManager sourceCodeSchemaManager)
			: base(sourceCodeSchemaManager) {
		}

		public DummyForCrtC360InUiV2Schema(DummyForCrtC360InUiV2Schema source)
			: base( source) {
		}

		#endregion

		#region Methods: Protected

		protected override void InitializeProperties() {
			base.InitializeProperties();
			UId = new Guid("27f133dd-dbef-4bcf-9ca8-678bb95f4495");
			Name = "DummyForCrtC360InUiV2";
			ParentSchemaUId = new Guid("50e3acc0-26fc-4237-a095-849a1d534bd3");
			CreatedInPackageId = new Guid("7a107d4a-5adb-46ca-8a5d-4e6870cb0738");
			ZipBody = new byte[] { 31,139,8,0,0,0,0,0,4,0,203,75,204,77,45,46,72,76,78,85,8,73,45,42,74,44,206,79,43,209,115,206,207,75,203,76,47,45,74,44,201,204,207,227,229,170,230,229,226,229,226,204,204,43,73,45,202,75,204,81,72,206,73,44,46,86,112,41,205,205,173,116,203,47,114,46,42,113,54,54,51,240,204,11,205,12,51,82,168,86,168,5,41,174,5,0,98,8,191,27,86,0,0,0 };
		}

		#endregion

		#region Methods: Public

		public override void GetParentRealUIds(Collection<Guid> realUIds) {
			base.GetParentRealUIds(realUIds);
			realUIds.Add(new Guid("27f133dd-dbef-4bcf-9ca8-678bb95f4495"));
		}

		#endregion

	}

	#endregion

}

