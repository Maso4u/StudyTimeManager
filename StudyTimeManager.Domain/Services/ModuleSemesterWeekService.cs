using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

namespace StudyTimeManager.Domain.Services
{
    internal class ModuleSemesterWeekService : IModuleSemesterWeekService
    {
        private readonly Semester _semester;

        public ModuleSemesterWeekService(Semester semester)
        {
            _semester = semester;
        }

        public bool CreateModuleSemesterWeek(string moduleCode, ModuleSemesterWeek moduleSemesterWeek)
        {
            int initialModuleSemesterWeeksCount = _semester.Modules.Count;
            _semester.Modules.First(m => m.Code.Equals(moduleCode)).Weeks.Add(moduleSemesterWeek);
            return _semester.Modules.Count > initialModuleSemesterWeeksCount;
        }

        public ICollection<ModuleSemesterWeek> GetModuleSemesterWeeksForAllModules(int week)
        {
            ICollection<ModuleSemesterWeek> moduleSemesterWeeksFound = new List<ModuleSemesterWeek>();
            foreach (var module in _semester.Modules)
            {
                moduleSemesterWeeksFound.Add(module[week]);
            }
            return moduleSemesterWeeksFound;
        }

        public bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int remainingHoursLeft)
        {
             int oldRemainingHours= _semester.Modules.First(m => m.Code.Equals(moduleCode))[week].RemainingSelfStudyHours;
            if (remainingHoursLeft<oldRemainingHours)
            {
                _semester.Modules.First(m => m.Code.Equals(moduleCode))[week].RemainingSelfStudyHours = remainingHoursLeft;
                return true;
            }
            return false;
        }
    }
}
