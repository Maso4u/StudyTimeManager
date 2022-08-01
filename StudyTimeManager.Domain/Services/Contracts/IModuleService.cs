using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface IModuleService
    {
        bool CreateModule(Module module);
        bool DeleteModule(string moduleCode);
        void UpdateModule(Module module);
        Module GetModule(string moduleCode);
        ICollection<Module> GetModules();
    }
}
