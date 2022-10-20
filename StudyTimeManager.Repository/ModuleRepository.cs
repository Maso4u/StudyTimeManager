using Repository;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Linq;

namespace StudyTimeManager.Repository
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateModuleForSemester(Guid semesterId, Module module)
        {
            module.SemesterId = semesterId;
            Create(module);
        }

        public void DeleteModule(Module module)
        {
            Delete(module);
        }

        public Module? GetModuleById(Guid semesterId,Guid moduleId,bool trackChanges)
        {
            return FindByCondition(m=>
            m.SemesterId == semesterId && 
            m.Id.Equals(moduleId), trackChanges)
                .SingleOrDefault();
        }

        public Module? GetModuleByCode(Guid semesterId, string moduleCode, bool trackChanges)
        {
            return FindByCondition(m =>
            m.SemesterId == semesterId &&
            m.Id.Equals(moduleCode), trackChanges)
                .SingleOrDefault();
        }
    }
}
