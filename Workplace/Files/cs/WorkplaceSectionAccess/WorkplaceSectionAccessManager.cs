namespace Terrasoft.Configuration.WorkplaceSectionAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Core;
	using Core.Factories;
	using Common;
	using Section;
	using Terrasoft.Core.Configuration;
	using Workplace;
	using WorkplaceApi;

	#region Class: WorkplaceSectionAccessManager

	[DefaultBinding(typeof(IWorkplaceSectionAccessManager))]
	public class WorkplaceSectionAccessManager : IWorkplaceSectionAccessManager
	{

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private ISectionRepository _sectionRepository;
		private IWorkplaceRepository _workplaceRepository;
		private IEnumerable<string> _excludedFromAdditionalUserSections;
		private int _additionalUserSectionsCount;
		private IReadOnlyCollection<WorkplaceType> _allowedWorkplaceTypes;

		#endregion

		#region Constructors: Public

		public WorkplaceSectionAccessManager(UserConnection userConnection) {
			_userConnection = userConnection;
			InitSectionRepository();
			InitWorkplaceRepository();
			InitExcludedFromAdditionalUserSectionsLimits();
			InitAllowedWorkplaceTypesLimits();
			InitAdditionalUserSectionsCountLimits();
		}

		#endregion

		#region Properties: Private

		private ILicManager _licManager;
		private ILicManager LicManager => _licManager ?? (_licManager = _userConnection.AppConnection.LicManager);

		private SysUserInfo _currentUser;
		private SysUserInfo CurrentUser => _currentUser ?? (_currentUser = _userConnection.CurrentUser);

		#endregion

		#region Methods: Private

		private void InitAllowedWorkplaceTypesLimits() {
			ICollection<string> allowedWorkplaceTypesCodes =
				LicManager.GetOperationValues(_userConnection.CurrentUser.Id, "AllowedWorkplaceTypes").ToList();
			_allowedWorkplaceTypes = allowedWorkplaceTypesCodes
				.Select(allowedWorkplaceTypeCode => ConvertToWorkPlaceType(int.Parse(allowedWorkplaceTypeCode)))
				.ToList();
		}

		private void InitAdditionalUserSectionsCountLimits() {
			_additionalUserSectionsCount = LicManager.FindOperationLimit(CurrentUser.Id, "AllowedSectionsCount") ?? 0;
		}

		private void InitWorkplaceRepository() {
			_workplaceRepository = ClassFactory.Get<IWorkplaceRepository>(
				new ConstructorArgument("uc", _userConnection));
			_workplaceRepository.ClearCache();
		}

		private void InitSectionRepository() {
			_sectionRepository = ClassFactory.Get<ISectionRepository>(CurrentUser.ConnectionType.ToString(),
				new ConstructorArgument("uc", _userConnection));
			_sectionRepository.ClearCache();
		}

		private void InitExcludedFromAdditionalUserSectionsLimits() {
			_excludedFromAdditionalUserSections =
				LicManager.GetOperationValues(CurrentUser.Id, "ExcludedFromSectionsCountLimitation");
		}

		private static WorkplaceType ConvertToWorkPlaceType(int allowedWorkplaceType) {
			var workplaceType = (WorkplaceType)allowedWorkplaceType;
			if (!Enum.IsDefined(typeof(WorkplaceType), workplaceType)) {
				throw new ItemNotFoundException($"Workplace type {workplaceType} from LicManager not found");
			}
			return workplaceType;
		}

		private IEnumerable<Section> GetSectionsInWorkplace(Workplace workplace) {
			var sortedSectionIds = workplace.GetSectionIds().ToList();
			var allSections = _sectionRepository.GetAll()
				.Where(s => s.GetIsInWorkplace(workplace.Id))
				.OrderBy(s => sortedSectionIds.IndexOf(s.Id));
			return allSections;
		}

		private IEnumerable<AllowedWorkplaceStructureInfo> GetAllAllowedWorkplaceStructuresInternal() {
			IEnumerable<Workplace> allowedWorkplaces = GetAllAllowedWorkplacesInternal();
			var allowedWorkplacesStructures = new List<AllowedWorkplaceStructureInfo>();
			foreach (var workplace in allowedWorkplaces) {
				var allowedWorkplaceStructureInfo = new AllowedWorkplaceStructureInfo() {
					WorkplaceId = workplace.Id
				};
				FillWorkplaceSections(workplace, allowedWorkplaceStructureInfo);
				allowedWorkplacesStructures.Add(allowedWorkplaceStructureInfo);
			}
			return allowedWorkplacesStructures;
		}

		private IEnumerable<Workplace> SortWorkplaces(IEnumerable<Workplace> workplaces) {
			return workplaces.OrderBy(workplace => workplace.Position);
		}

		private IEnumerable<Workplace> GetAllAllowedWorkplacesInternal() {
			ICollection<Workplace> allWorkplaces = _workplaceRepository.GetAll()
				.Where(workplace => workplace.GetIsAllowedForUser(_userConnection.CurrentUser.Id)).ToList();
			if (!GetUserHasLimits()) {
				return SortWorkplaces(allWorkplaces);
			}
			return SortWorkplaces(allWorkplaces.Where(workplace => _allowedWorkplaceTypes.Contains(workplace.Type)))
				.Take(1);
		}

		private void FillWorkplaceSections(Workplace workplace,
				AllowedWorkplaceStructureInfo allowedWorkplaceStructureInfo) {
			IEnumerable<Section> allSections = GetSectionsInWorkplace(workplace);
			var allowedSectionsIds = new List<Guid>();
			if (!GetUserHasLimits()) {
				allowedWorkplaceStructureInfo.AllowedSectionsIds.AddRange(allSections.Select(section => section.Id));
				return;
			}
			int sectionCounter = 0;
			foreach (Section section in allSections) {
				if (_excludedFromAdditionalUserSections.Contains(section.Code)) {
					allowedSectionsIds.Add(section.Id);
					continue;
				}

				if (sectionCounter < _additionalUserSectionsCount) {
					allowedSectionsIds.Add(section.Id);
					sectionCounter++;
				}
			}
			int allowedSectionCount = _excludedFromAdditionalUserSections.Count() + _additionalUserSectionsCount;
			allowedWorkplaceStructureInfo.AllowedSectionsIds.AddRange(allowedSectionsIds
				.Take(allowedSectionCount));
		}

		private bool GetUserHasLimits() {
			return _excludedFromAdditionalUserSections.IsNotNullOrEmpty() || _additionalUserSectionsCount > 0
				|| _allowedWorkplaceTypes.IsNotNullOrEmpty();
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetAllAllowedWorkplacesWithSections"/>
		public IEnumerable<AllowedWorkplaceStructureInfo> GetAllAllowedWorkplacesWithSections() {
			return GetAllAllowedWorkplaceStructuresInternal();
		}

		/// <inheritdoc cref="IWorkplaceSectionAccessManager.GetAllAllowedWorkplacesIds"/>
		public IEnumerable<Guid> GetAllAllowedWorkplacesIds() {
			return GetAllAllowedWorkplacesInternal().Select(workplace => workplace.Id);
		}

		#endregion

	}

	#endregion

}