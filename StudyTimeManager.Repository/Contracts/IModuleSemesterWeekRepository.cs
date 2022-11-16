using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleSemesterWeekRepository
    {
        Task CreateModuleSemesterWeeks(IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks);
        Task<ModuleSemesterWeek?>  GetModuleSemesterWeekByDate(Guid moduleId, DateTime studySessionDate, bool trackChanges);
        Task<IEnumerable<ModuleSemesterWeek>> GetModuleSemesterWeeksForAModule(Guid moduleId, bool trackChanges);
        Task UpdateModuleSemesterWeeksForAModule(ModuleSemesterWeek moduleSemesterWeek);
    }
}
