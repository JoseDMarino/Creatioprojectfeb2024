namespace Terrasoft.Configuration.NextSteps
{
	using System;
	using System.Collections.Generic;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using Terrasoft.Nui.ServiceModel.DataContract;
	using Terrasoft.Nui.ServiceModel.WebService;

	#region Class: ProcessElementNextStepQueryExecutor

	/// <summary>
	/// Implementation <see cref="IProcessNextStepQueryExecutor"/> for 'Process' entity.
	/// </summary>
	[DefaultBinding(typeof(IProcessNextStepQueryExecutor), Name = "ProcessElementNextStepQueryExecutor")]
	internal class ProcessElementNextStepQueryExecutor: IProcessNextStepQueryExecutor
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;

		#endregion

		#region Constructors: Public

		public ProcessElementNextStepQueryExecutor(UserConnection uc) {
			_userConnection = uc;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IProcessNextStepQueryExecutor.GetNextSteps(string, Guid, List{string})"/>
		public List<NextStepModel> GetNextSteps(string entityName, Guid entityId, List<string> elementsIds) {
			var result = new List<NextStepModel>();
			var item = _userConnection.EntitySchemaManager.GetItemByName(entityName);
			var request = new ProcessActionDashboardRequest {
				EntityId = entityId,
				EntitySchemaUId = item.UId,
				ElementIds = elementsIds,
				RowCount = 100
			};
			var processDashboardService = new ProcessActionDashboardRequestHandler(_userConnection);
			var response = processDashboardService.Handle(request);
			foreach (var entity in response.Rows) {
				var ownerName = (string)entity["Owner"];
				var ownerRole = (string)entity["RoleName"];
				var hasOwnerRole = ownerName.IsNullOrEmpty();
				var nextStep = new NextStepModel {
					Id = Guid.Parse(entity["Id"].ToString()),
					ProcessElementId = Guid.Parse(entity["ProcessElementId"].ToString()),
					Caption = entity["ElementCaption"].ToString(),
					OwnerName = hasOwnerRole ? ownerRole : ownerName,
					OwnerId = Guid.NewGuid(),
					IsOwnerRole = hasOwnerRole,
					MasterEntityId = entityId,
					MasterEntityName = entityName,
					EntityName = "ProcessElement",
					IsRequired = (bool)entity["IsRequired"],
					Date = DateTime.Parse(entity["Date"].ToString()),
					AdditionalInfo = Json.Serialize(new Dictionary<string, object> { { "UserTaskEntitySchemaName", entity["UserTaskEntitySchemaName"].ToString() }  }, true)
				};
				result.Add(nextStep);
			}
			return result;
		}

		#endregion

	}

	#endregion

}



