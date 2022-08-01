using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface IModuleSemesterWeekService
    {
        bool CreateModuleSemesterWeek(string moduleCode, ModuleSemesterWeek moduleSemesterWeek);
        bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int remainingHoursLeft);
        ICollection<ModuleSemesterWeek> GetModuleSemesterWeeksForAllModules(int week);
    }
}
