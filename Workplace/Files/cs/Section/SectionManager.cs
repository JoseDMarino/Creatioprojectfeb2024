namespace Terrasoft.Configuration.Section
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Terrasoft.Configuration.SspEntity;
	using Terrasoft.Configuration.Workplace;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;
	using WorkplaceApi;

	#region Class: SectionManager

	[DefaultBinding(typeof(ISectionManager))]
	public class SectionManager : ISectionManager
	{

		#region Fields: Private

		/// <summary>
		/// <see cref="ISectionRepository"/> implementation instance.
		/// </summary>
		private readonly ISectionRepository _sectionRepository;

		/// <summary>
		/// <see cref="IWorkplaceRepository"/> implementation instance.
		/// </summary>
		private readonly IWorkplaceRepository _workplaceRepository;

		/// <summary>
		/// <see cref="ISspEntityRepository"/> implementation instance.
		/// </summary>
		private readonly ISspEntityRepository _sspEntityRepository;
		private readonly IWorkplaceSectionAccessManager _workplaceSectionAccessManager;

		#endregion

		#region Constructors: Public

		public SectionManager(UserConnection uc, string sectionType) {
			_sectionRepository = ClassFactory.Get<ISectionRepository>(sectionType, new ConstructorArgument("uc", uc));
			_workplaceRepository = ClassFactory.Get<IWorkplaceRepository>(new ConstructorArgument("uc", uc));
			_sspEntityRepository = ClassFactory.Get<ISspEntityRepository>(new ConstructorArgument("uc", uc));
			_workplaceSectionAccessManager = ClassFactory.Get<IWorkplaceSectionAccessManager>(
				new ConstructorArgument("userConnection", uc));
		}

		#endregion

		#region Methods: Private

		private List<Guid> GetAllowedWorkplaceSectionsIds(Guid workplaceId) {
			List<AllowedWorkplaceStructureInfo> allowedWorkplaceStructureInfo =
				_workplaceSectionAccessManager.GetAllAllowedWorkplacesWithSections().ToList();
			return allowedWorkplaceStructureInfo.Find(info => info.WorkplaceId == workplaceId)?.AllowedSectionsIds
				?? new List<Guid>();
		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Checks that <paramref name="section"/> type equals <paramref name="workplace"/> type.
		/// </summary>
		/// <param name="workplace"><see cref="Workplace"/> instance.</param>
		/// <param name="section"><see cref="Section"/> instance.</param>
		/// <returns><c>True</c> if section type equals workplace type. Returns <c>false</c> otherwise.</returns>
		protected virtual bool CheckSectionTypeEqualsWorkplaceType(Workplace workplace, Section section) {
			return (int)workplace.Type == (int)section.Type;
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="ISectionManager.GetSectionsByEntityUId"/>
		public IEnumerable<Section> GetSectionsByEntityUId(Guid entityUId) {
			var allSections = _sectionRepository.GetAll();
			return allSections.Where(s => s.EntityUId.Equals(entityUId));
		}

		/// <inheritdoc cref="ISectionManager.GetSameEntitySections"/>
		public IEnumerable<Section> GetSameEntitySections(Guid sectionId) {
			Section section = _sectionRepository.Get(sectionId);
			var allSections = _sectionRepository.GetAll();
			return allSections.Where(s => s.EntityUId.Equals(section.EntityUId));
		}

		/// <inheritdoc cref="ISectionManager.GetAvailableWorkplaceSections"/>
		public IEnumerable<Section> GetAvailableWorkplaceSections(Guid workplaceId) {
			var workplace = _workplaceRepository.Get(workplaceId);
			var allSections = _sectionRepository.GetAll();
			return allSections.Where(s => CheckSectionTypeEqualsWorkplaceType(workplace, s) && !s.GetIsInWorkplace(workplace.Id));
		}

		/// <inheritdoc cref="ISectionManager.GetByType"/>
		public IEnumerable<Section> GetByType(SectionType type) {
			return _sectionRepository.GetByType(type);
		}

		/// <inheritdoc cref="ISectionManager.Save"/>
		public void Save(Guid sectionId) {
			_sectionRepository.ClearCache();
			Section section = _sectionRepository.Get(sectionId);
			_sectionRepository.Save(section);
		}

		/// <inheritdoc cref="ISectionManager.GetRelatedEntityIds"/>
		public IEnumerable<Guid> GetRelatedEntityIds(Guid sectionId) {
			Section section = _sectionRepository.Get(sectionId);
			return _sectionRepository.GetRelatedEntityIds(section);
		}

		/// <inheritdoc cref="ISectionManager.GetSectionNonAdministratedByRecordsEntityCaptions"/>
		public IEnumerable<string> GetSectionNonAdministratedByRecordsEntityCaptions(Guid sectionId) {
			Section section = _sectionRepository.Get(sectionId);
			return _sectionRepository.GetSectionNonAdministratedByRecordsEntityCaptions(section);
		}

		/// <inheritdoc cref="ISectionManager.SetSectionSchemasAdministratedByRecords"/>
		public void SetSectionSchemasAdministratedByRecords(Guid sectionId) {
			Section section = _sectionRepository.Get(sectionId);
			_sectionRepository.SetSectionSchemasAdministratedByRecords(section);
		}

		/// <inheritdoc cref="ISectionManager.GetSspColumnAccessList"/>
		public IEnumerable<Guid> GetSspColumnAccessList(Guid entityUId) {
			return _sspEntityRepository.GetSspColumnAccessList(entityUId);
		}

		/// <inheritdoc cref="ISectionManager.GetSectionsInWorkplace"/>
		public IEnumerable<Section> GetSectionsInWorkplace(Guid workplaceId, bool useCache = true) {
			var workplace = _workplaceRepository.Get(workplaceId);
			if (!useCache) {
				_sectionRepository.ClearCache();
			}
			List<Guid> allowedSectionsIds = GetAllowedWorkplaceSectionsIds(workplaceId);
			var sortedSectionIds = workplace.GetSectionIds().ToList();
			var allSections = _sectionRepository.GetAll();
			return allSections.Where(s => s.GetIsInWorkplace(workplace.Id) && allowedSectionsIds.Contains(s.Id))
				.OrderBy(s => sortedSectionIds.IndexOf(s.Id));
		}

		/// <inheritdoc cref="ISectionManager.GetEntitiesCaptionsNotAdministratedByRights"/>
		public (IEnumerable<string> byRecords, IEnumerable<string> byOperations) GetEntitiesCaptionsNotAdministratedByRights(Guid entitySchemaUId) {
			return _sectionRepository.GetEntitiesCaptionsNotAdministratedByRights(entitySchemaUId);
		}

		/// <inheritdoc cref="ISectionManager.SetConnectedEntitiesRights"/>
		public void SetConnectedEntitiesRights(Guid entitySchemaUId) {
			_sectionRepository.SetConnectedEntitiesRights(entitySchemaUId);
		}

		/// <inheritdoc cref="ISectionManager.SetConnectedEntitiesRightsBySection"/>
		public void SetConnectedEntitiesRightsBySection(Guid sectionId) {
			Section section = _sectionRepository.Get(sectionId);
			_sectionRepository.SetConnectedEntitiesRights(section.EntityUId);
		}

		#endregion

	}

	#endregion

}