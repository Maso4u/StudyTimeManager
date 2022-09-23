using Repository;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;

namespace StudyTimeManager.Repository
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateModuleForSemester(Guid semesterId, Module module)
        {
            module.SemesterId = semesterId;
            Create(module);
        }

        public Module? GetModuleByCode(Guid semesterId,string moduleCode,bool trackChanges)
        {
            return FindByCondition(m=>
            m.SemesterId == semesterId && 
            m.Code.Equals(moduleCode), trackChanges)
                .SingleOrDefault();
        }

    }
}
