namespace Terrasoft.Configuration.NextSteps
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Core;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;

	#region Class: ApprovalNextStepQueryExecutor

	/// <summary>
	/// Implementation <see cref="INextStepQueryExecutor"/> for 'Approval' entity.
	/// </summary>
	[DefaultBinding(typeof(INextStepQueryExecutor), Name = "ApprovalNextStepQueryExecutor")]
	internal class ApprovalNextStepQueryExecutor: INextStepQueryExecutor
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public ApprovalNextStepQueryExecutor(UserConnection uc) {
			_userConnection = uc;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="INextStepQueryExecutor.GetNextSteps(string, Guid)"/>
		public List<NextStepModel> GetNextSteps(string entityName, Guid entityId) {
			var executingEntityName = "SysApproval";
			var result = new List<NextStepModel>();
			var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager, executingEntityName);
			esq.RowCount = 100;
			esq.AddColumn("Id");
			esq.AddColumn("Objective").OrderByAsc();
			esq.AddColumn("CreatedOn");
			esq.AddColumn("VisaOwner.Contact");
			esq.AddColumn("VisaOwner");
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "EntityId", entityId));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.IsNotNull, "EntityId"));
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal,
				"[SysAdminUnitInRole:SysAdminUnitRoleId:VisaOwner].SysAdminUnit", _userConnection.CurrentUser.Id));
			var statusFilter = new EntitySchemaQueryFilterCollection(esq, LogicalOperationStrict.Or) {
				esq.CreateFilterWithParameters(FilterComparisonType.IsNull, "Status"),
				esq.CreateFilterWithParameters(FilterComparisonType.Equal, "Status.IsFinal", false)
			};
			esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Equal, "IsCanceled", false));
			esq.Filters.Add(statusFilter);
			EntityCollection entityCollection = esq.GetEntityCollection(_userConnection);
			foreach (var entity in entityCollection) {
				var ownerColumn = entity.Schema.GetSchemaColumnByPath("VisaOwner.Contact");
				var ownerRoleColumn = entity.Schema.GetSchemaColumnByPath("VisaOwner");
				var ownerName = entity.GetTypedColumnValue<string>($"VisaOwner_{ownerColumn.DisplayColumnValueName}");
				var ownerRoleName = entity.GetTypedColumnValue<string>(ownerRoleColumn.DisplayColumnValueName);
				var hasOwnerRole = ownerName.IsNullOrEmpty();
				var nextStep = new NextStepModel {
					Id = entity.GetTypedColumnValue<Guid>("Id1"),
					ProcessElementId = entity.GetTypedColumnValue<Guid>("Id1"),
					Caption = entity.GetTypedColumnValue<string>("Objective"),
					OwnerName = hasOwnerRole ? ownerRoleName : ownerName,
					OwnerId = hasOwnerRole 
						? entity.GetTypedColumnValue<Guid>(ownerRoleColumn.ColumnValueName)
						: entity.GetTypedColumnValue<Guid>($"VisaOwner_{ownerColumn.ColumnValueName}"),
					OwnerPhotoId = entity.GetTypedColumnValue<Guid>($"VisaOwner_{ownerColumn.PrimaryImageColumnValueName}"),
					IsOwnerRole = hasOwnerRole,
					MasterEntityId = entityId,
					MasterEntityName = entityName,
					EntityName = executingEntityName,
					IsRequired = false,
					Date = entity.GetTypedColumnValue<DateTime>("CreatedOn")
				};
				result.Add(nextStep);
			}
			return result;
		}

		#endregion

	}

	#endregion

}



