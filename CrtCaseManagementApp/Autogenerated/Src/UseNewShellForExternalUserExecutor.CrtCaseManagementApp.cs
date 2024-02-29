namespace Terrasoft.Configuration
{
	using Terrasoft.Core;
	using Terrasoft.Core.DB;
	 
	public class UseNewShellForExternalUsersExecutor : IInstallScriptExecutor
	{

		#region Methods: Public

		public void Execute(UserConnection userConnection) {
			Update updateQuery = new Update(userConnection, "SysSettings")
				.Set("IsSSPAvailable", Column.Const(true))
				.Where("Code").IsEqual(Column.Parameter("UseNewShellForExternalUsers")) as Update;
			updateQuery.Execute();
		}

		#endregion

	}
	
}
