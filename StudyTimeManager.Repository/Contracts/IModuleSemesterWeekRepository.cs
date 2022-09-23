using Shared.DTOs.ModuleSemesterWeek;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleSemesterWeekRepository
    {
        void CreateModuleSemesterWeeks(IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks);
        ModuleSemesterWeek GetModuleSemesterWeekByDate(Guid moduleId, DateOnly studySessionDate, bool trackChanges);
        IEnumerable<ModuleSemesterWeek> GetModuleSemesterWeeksForAModule(Guid moduleId,bool trackChanges);
    }
}
