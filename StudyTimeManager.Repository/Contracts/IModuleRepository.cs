using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleRepository
    {
        Task CreateModuleForSemester(Guid semesterId, Module module);
        public Task<Module?> GetModuleById(Guid semesterId, Guid moduleId, bool trackChanges);
        public Task<Module?> GetModuleByCode(Guid semesterId, string moduleCode, bool trackChanges);
        public Task<IEnumerable<Module>?> GetAllModules(Guid semesterId);
        public Task DeleteModule(Module module);
    }
}
