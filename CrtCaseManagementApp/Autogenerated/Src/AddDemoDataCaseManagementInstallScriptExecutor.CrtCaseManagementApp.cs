using System;
using System.Linq;
using Newtonsoft.Json;
using Terrasoft.Core;
using Terrasoft.Core.Entities;

namespace Terrasoft.Configuration
{

	#region Class: InstallDataDTO
	internal class InstallDataDTO
	{
		public string EntitySchemaName {
			get; set;
		}
		public InstallColumnValueDTO[] Values {
			get; set;
		}

		public Guid GetPrimaryValue() {
			return Guid.Parse(this.Values.Where(value => value.Name == "Id").FirstOrDefault().Value.ToString());
		}
	}
	#endregion

	#region Class: InstallColumnValueDTO
	public class InstallColumnValueDTO
	{
		public string Name {
			get; set;
		}
		public object Value {
			get; set;
		}
	}
	#endregion

	#region Class: AddDemoDataCaseManagementInstallScriptExecutor
	internal class AddDemoDataCaseManagementInstallScriptExecutor : IInstallScriptExecutor
	{

		#region Methods: Private
			private void SetColumnValue(Entity entity, string columnName, string stringValue) {
				object value;
				Type columnType = entity.Schema.Columns.GetByColumnValueName(columnName).ValueType;
				if (columnType == typeof(DateTime)) {
					value = DateTime.Parse(stringValue);
				} else if (columnType == typeof(Guid)) {
					value = Guid.Parse(stringValue);
				} else if (columnType == typeof(bool)) {
					value = bool.Parse(stringValue);
				} else if (columnType == typeof(int)) {
					value = int.Parse(stringValue);
				} else {
					value = stringValue;
				}
				entity.SetColumnValue(columnName, value);
			}

			private bool IsLookupColumn(EntitySchema schema, string columnName) {
				return schema.Columns.GetByColumnValueName(columnName).DataValueType is LookupDataValueType;
			}

			private static Entity FetchEntity(UserConnection userConnection, string entitySchemaName, Guid entityId) {
				EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
				Entity entity = entitySchemaManager.GetEntityByName(entitySchemaName, userConnection);
				return !entity.FetchFromDB(entityId) ? null : entity;
			}

			private static Entity CreateEntity(UserConnection userConnection, string entitySchemaName, Guid entityId) {
				EntitySchemaManager entitySchemaManager = userConnection.EntitySchemaManager;
				Entity entity = entitySchemaManager.GetEntityByName(entitySchemaName, userConnection);
				entity.PrimaryColumnValue = entityId;
				return entity;
			}

			private static bool IsReferencedLookupValueExists(UserConnection userConnection, EntitySchema schema,
					string columnName, Guid columnValue) {
				var referenceSchemaName = schema.Columns.GetByColumnValueName(columnName).ReferenceSchema.Name;
				Entity entity = schema.EntitySchemaManager.GetEntityByName(referenceSchemaName, userConnection);
				return entity.FetchFromDB(columnValue);
			}

			private string getJson() {
				return @"
				[
					{
						""EntitySchemaName"": ""Account"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""405947d0-2ffb-4ded-8675-0475f19f5a81""},
							{""Name"": ""CreatedOn"", ""Value"": ""2019-06-11 15:46:11""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-15 16:58:01""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""Name"", ""Value"": ""Accom (sample)""},
							{""Name"": ""OwnershipId"", ""Value"": ""f7af2e24-f46b-1410-fb98-00155d043204""},
							{""Name"": ""OwnerId"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""IndustryId"", ""Value"": ""fbbf0e52-f36b-1410-c493-00155d043205""},
							{""Name"": ""Code"", ""Value"": ""1""},
							{""Name"": ""TypeId"", ""Value"": ""03a75490-53e6-df11-971b-001d60e938c6""},
							{""Name"": ""Phone"", ""Value"": ""+1 617 440 2498""},
							{""Name"": ""Web"", ""Value"": ""ac.com""},
							{""Name"": ""AddressTypeId"", ""Value"": ""780bf68c-4b6e-df11-b988-001d60e938c6""},
							{""Name"": ""Address"", ""Value"": ""31 Union Street""},
							{""Name"": ""CityId"", ""Value"": ""74144617-a288-4984-bcac-e78733277a61""},
							{""Name"": ""RegionId"", ""Value"": ""658acfe7-09ae-4747-b1b3-541e2dfa1b9e""},
							{""Name"": ""Zip"", ""Value"": ""02111""},
							{""Name"": ""CountryId"", ""Value"": ""e0be1264-f36b-1410-fa98-00155d043204""},
							{""Name"": ""AccountCategoryId"", ""Value"": ""38ea507c-55e6-df11-971b-001d60e938c6""},
							{""Name"": ""EmployeesNumberId"", ""Value"": ""ef7f0eb0-f36b-1410-849f-0026185bfcd3""},
							{""Name"": ""AnnualRevenueId"", ""Value"": ""bc30bdaa-55e6-df11-971b-001d60e938c6""},
							{""Name"": ""AlternativeName"", ""Value"": ""Accom-Westhouse Company""},
							{""Name"": ""Completeness"", ""Value"": ""95""}
						]
					},
					{
						""EntitySchemaName"": ""Contact"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""c4ed336c-3e9b-40fe-8b82-5632476472b4""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-15 13:27:21""},
							{""Name"": ""CreatedOn"", ""Value"": ""2019-06-13 12:06:25""},
							{""Name"": ""Name"", ""Value"": ""Andrew Baker (sample)""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""AccountId"", ""Value"": ""405947d0-2ffb-4ded-8675-0475f19f5a81""},
							{""Name"": ""SalutationTypeId"", ""Value"": ""7426ffb3-56e6-df11-971b-001d60e938c6""},
							{""Name"": ""GenderId"", ""Value"": ""eeac42ee-65b6-df11-831a-001d60e938c6""},
							{""Name"": ""OwnerId"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""DecisionRoleId"", ""Value"": ""f71ee81d-0cac-4e41-b7dc-9d4bd2e08491""},
							{""Name"": ""JobId"", ""Value"": ""cb49a603-57e6-df11-971b-001d60e938c6""},
							{""Name"": ""JobTitle"", ""Value"": ""Specialist""},
							{""Name"": ""DepartmentId"", ""Value"": ""2076c4b6-7fe6-df11-971b-001d60e938c6""},
							{""Name"": ""BirthDate"", ""Value"": ""1986-01-20 00:00:00""},
							{""Name"": ""Phone"", ""Value"": ""+1 617 440 2031""},
							{""Name"": ""MobilePhone"", ""Value"": ""+1 617 221 5187""},
							{""Name"": ""Email"", ""Value"": ""a.baker@ac.com""},
							{""Name"": ""Address"", ""Value"": ""39 Columbia Street""},
							{""Name"": ""CityId"", ""Value"": ""74144617-a288-4984-bcac-e78733277a61""},
							{""Name"": ""RegionId"", ""Value"": ""658acfe7-09ae-4747-b1b3-541e2dfa1b9e""},
							{""Name"": ""Zip"", ""Value"": ""02112""},
							{""Name"": ""CountryId"", ""Value"": ""e0be1264-f36b-1410-fa98-00155d043204""},
							{""Name"": ""DoNotUseEmail"", ""Value"": ""False""},
							{""Name"": ""DoNotUseCall"", ""Value"": ""False""},
							{""Name"": ""DoNotUseFax"", ""Value"": ""False""},
							{""Name"": ""DoNotUseSms"", ""Value"": ""False""},
							{""Name"": ""DoNotUseMail"", ""Value"": ""False""},
							{""Name"": ""TypeId"", ""Value"": ""00783ef6-f36b-1410-a883-16d83cab0980""},
							{""Name"": ""AddressTypeId"", ""Value"": ""4f8b2d67-71d0-45fb-897e-cd4a308a97c0""},
							{""Name"": ""Surname"", ""Value"": ""Baker""},
							{""Name"": ""GivenName"", ""Value"": ""Andrew""},
							{""Name"": ""Confirmed"", ""Value"": ""True""},
							{""Name"": ""Completeness"", ""Value"": ""100""},
							{""Name"": ""Age"", ""Value"": ""34""}
						]
					},
					{
						""EntitySchemaName"": ""ServicePact"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""ac198a47-588f-44c9-b5f3-dc507795522c""},
							{""Name"": ""CreatedOn"", ""Value"": ""2016-09-19 00:04:22""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-19 00:05:37""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""Name"", ""Value"": ""2 - Accom (sample)""},
							{""Name"": ""StatusId"", ""Value"": ""f65fbd8f-f7c1-43b2-8f1c-75e02a4975aa""},
							{""Name"": ""StartDate"", ""Value"": ""2019-05-30 00:00:00""},
							{""Name"": ""EndDate"", ""Value"": ""2019-10-24 00:00:00""},
							{""Name"": ""OwnerId"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""TypeId"", ""Value"": ""f6822f7f-0c38-4d98-87d2-9cfd25bdaa60""},
							{""Name"": ""Number"", ""Value"": ""1""},
							{""Name"": ""CalendarId"", ""Value"": ""f0ff1f0e-f46b-1410-1787-0026185bfcd3""}
			
						]
					},
					{
						""EntitySchemaName"": ""ServiceObject"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""5365a271-5baa-4faa-a3ed-2831e64a94ba""},
							{""Name"": ""CreatedOn"", ""Value"": ""2016-09-27 15:38:42""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-27 15:38:42""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""AccountId"", ""Value"": ""405947d0-2ffb-4ded-8675-0475f19f5a81""},
							{""Name"": ""ServicePactId"", ""Value"": ""ac198a47-588f-44c9-b5f3-dc507795522c""},
							{""Name"": ""TypeId"", ""Value"": ""350d6fd7-c84c-43c0-bec6-40116cbb9d2b""}
			
						]
					},
					{
						""EntitySchemaName"": ""ServiceObject"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""538cec53-212f-4e88-9181-4db47f364f4b""},
							{""Name"": ""CreatedOn"", ""Value"": ""2016-09-19 00:05:22""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-19 00:05:22""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ContactId"", ""Value"": ""c4ed336c-3e9b-40fe-8b82-5632476472b4""},
							{""Name"": ""ServicePactId"", ""Value"": ""ac198a47-588f-44c9-b5f3-dc507795522c""},
							{""Name"": ""TypeId"", ""Value"": ""b2aad5df-af5c-425b-ac2c-2f13d8810a71""}			
						]
					},
					{
						""EntitySchemaName"": ""ServiceItem"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""9cd38a93-2835-46fa-8c55-d3c7f4684824""},
							{""Name"": ""CreatedOn"", ""Value"": ""2016-09-19 00:01:33""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2016-09-19 00:04:17""},
							{""Name"": ""ModifiedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""Name"", ""Value"": ""Software configuration (sample)""},
							{""Name"": ""StatusId"", ""Value"": ""9a32e65f-7d52-49ac-aef5-836a9a01f14e""},
							{""Name"": ""ReactionTimeValue"", ""Value"": ""8""},
							{""Name"": ""ReactionTimeUnitId"", ""Value"": ""b788b4de-5ae9-42e2-af34-cd3ad9e6c96f""},
							{""Name"": ""SolutionTimeValue"", ""Value"": ""2""},
							{""Name"": ""SolutionTimeUnitId"", ""Value"": ""36df302e-5ab6-43a0-aec7-45c2795d839d""},
							{""Name"": ""CaseCategoryId"", ""Value"": ""1c0bc159-150a-e111-a31b-00155d04c01d""},
							{""Name"": ""ReactionTime"", ""Value"": ""Calendar hours: 8""},
							{""Name"": ""SolutionTime"", ""Value"": ""Calendar days: 2""},
							{""Name"": ""CategoryId"", ""Value"": ""36a66853-7f72-44c3-9db7-b5c18ab09559""}			
						]
					},
					{
						""EntitySchemaName"": ""Case"",
						""Values"": [
							{""Name"": ""Id"", ""Value"": ""57286774-e71a-4530-bb60-b2e514fa535f""},
							{""Name"": ""CreatedOn"", ""Value"": ""2016-09-19 00:17:24""},
							{""Name"": ""CreatedById"", ""Value"": ""410006e1-ca4e-4502-a9ec-e54d922d2c00""},
							{""Name"": ""ModifiedOn"", ""Value"": ""2018-02-24 15:41:17""},
							{""Name"": ""ModifiedById"", ""Value"": ""76929f8c-7e15-4c64-bdb0-adc62d383727""},
							{""Name"": ""Number"", ""Value"": ""SR00000003""},
							{""Name"": ""RegisteredOn"", ""Value"": ""2019-07-04 17:17:26""},
							{""Name"": ""Subject"", ""Value"": ""Project management software configuration (sample)""},
							{""Name"": ""Symptoms"", ""Value"": ""The software is unstable and sometimes crashes, needs additional configuratio""},
							{""Name"": ""ResponseDate"", ""Value"": ""2019-07-05 01:17:26""},
							{""Name"": ""SolutionDate"", ""Value"": ""2019-07-06 17:17:26""},
							{""Name"": ""StatusId"", ""Value"": ""ae5f2f10-f46b-1410-fd9a-0050ba5d6c38""},
							{""Name"": ""PriorityId"", ""Value"": ""d9bd322c-f46b-1410-ee8c-0050ba5d6c38""},
							{""Name"": ""OriginId"", ""Value"": ""5e5e202a-f46b-1410-3692-0050ba5d6c38""},
							{""Name"": ""AccountId"", ""Value"": ""405947d0-2ffb-4ded-8675-0475f19f5a81""},
							{""Name"": ""ContactId"", ""Value"": ""c4ed336c-3e9b-40fe-8b82-5632476472b4""},
							{""Name"": ""GroupId"", ""Value"": ""b5d51a2d-c0dd-477b-b2aa-58034367ba26""},
							{""Name"": ""CategoryId"", ""Value"": ""1c0bc159-150a-e111-a31b-00155d04c01d""},
							{""Name"": ""ResponseOverdue"", ""Value"": ""True""},
							{""Name"": ""SolutionOverdue"", ""Value"": ""True""},
							{""Name"": ""ServicePactId"", ""Value"": ""ac198a47-588f-44c9-b5f3-dc507795522c""},
							{""Name"": ""ServiceItemId"", ""Value"": ""9cd38a93-2835-46fa-8c55-d3c7f4684824""},
							{""Name"": ""SupportLevelId"", ""Value"": ""ff7f2f92-f36b-1410-3d9c-0050ba5d6c38""}			
						]
					},
				]
		";
			}
		#endregion

		#region Methods: Public
			public void Execute(UserConnection userConnection) {
				try {
					InstallDataDTO[] data = JsonConvert.DeserializeObject<InstallDataDTO[]>(getJson());
					for (int i = 0; i < data.Length; i++) {
						var entity = FetchEntity(userConnection, data[i].EntitySchemaName, data[i].GetPrimaryValue()) ??
							CreateEntity(userConnection, data[i].EntitySchemaName, data[i].GetPrimaryValue());
						for (int j = 0; j < data[i].Values.Length; j++) {
							var columnName = data[i].Values[j].Name;
							var columnValue = data[i].Values[j].Value;
							if (columnValue == null) {
								entity.SetColumnValue(columnName, null);
							} else if (!IsLookupColumn(entity.Schema, columnName)) {
								SetColumnValue(entity, columnName, columnValue.ToString());
							} else {
								Guid lookupValue = Guid.Parse(columnValue.ToString());
								if (IsReferencedLookupValueExists(userConnection, entity.Schema, columnName, lookupValue)) {
									entity.SetColumnValue(columnName, lookupValue);
								}
							}
						}
						entity.Save(false);
					}
				} catch (Exception) {}
			}
		#endregion

	}

	#endregion

}
