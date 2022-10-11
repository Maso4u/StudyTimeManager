using StudyTimeManager.Domain.Models;
using System;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleRepository
    {
        void CreateModuleForSemester(Guid semesterId, Module module);
        public Module? GetModuleByCode(Guid semesterId, string moduleCode, bool trackChanges);
    }
}
