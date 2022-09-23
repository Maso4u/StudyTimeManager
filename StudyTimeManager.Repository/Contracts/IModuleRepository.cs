using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleRepository
    {
        void CreateModuleForSemester(Guid semesterId, Module module);
        public Module? GetModuleByCode(Guid semesterId, string moduleCode, bool trackChanges);
    }
}
