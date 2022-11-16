using Repository;
using Shared.DTOs.Module;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateModuleForSemester(Guid semesterId, Module module)
        {
            module.SemesterId = semesterId;
            await CreateAsync(module);
        }

        public async Task DeleteModule(Module module)
        {
            await DeleteAsync(module);
        }

        public async Task<Module?> GetModuleById(Guid semesterId,Guid moduleId,bool trackChanges)
        {
            var result = await FindByConditionAsync(m=> m.SemesterId == semesterId && 
            m.Id.Equals(moduleId), trackChanges);

            return result.SingleOrDefault();
        }

        public async Task<Module?> GetModuleByCode(Guid semesterId, string moduleCode, bool trackChanges)
        {
            var result = await FindByConditionAsync(m =>
            m.SemesterId == semesterId &&
            m.Id.Equals(moduleCode), trackChanges);

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Module>?> GetAllModules(Guid semesterId)
        {
            return await FindByConditionAsync(m =>
            m.SemesterId.Equals(semesterId),false);
        }
    }
}
