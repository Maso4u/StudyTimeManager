using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

namespace StudyTimeManager.Domain.Services
{
    public class ModuleService : IModuleService
    {
        private readonly Semester _semester;

        public ModuleService(Semester semester)
        {
            _semester = semester;
        }
        public bool CreateModule(Module module)
        {
            module.RequiredWeeklySelfStudyHours = CalculateRequiredWeeklySelfStudyHours(module);
            _semester.Modules.Add(module);
            
            return _semester.Modules.Contains(module);
        }

        public bool DeleteModule(string moduleCode)
        {
            Module? module = _semester.Modules.FirstOrDefault(m=>m.Code.Equals(moduleCode));
            return _semester.Modules.Remove(module);
        }

        public Module GetModule(string moduleCode) =>
            _semester.Modules.FirstOrDefault(m => m.Code.Equals(moduleCode));

        public ICollection<Module> GetModules() => _semester.Modules;

        public void UpdateModule(Module module)
        {
            int moduleIndex = _semester.Modules.ToList().IndexOf(module);
            _semester.Modules.ToList()[moduleIndex] = module;
        }

        private int CalculateRequiredWeeklySelfStudyHours(Module module)=>
            ((module.NumberOfCredits * 10) / _semester.NumberOfWeeks) - module.ClassHoursPerWeek;
    }
}
