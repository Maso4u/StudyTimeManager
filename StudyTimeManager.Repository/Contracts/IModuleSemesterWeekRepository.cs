using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IModuleSemesterWeekRepository
    {
        void CreateModuleSemesterWeeks(IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks);
        ModuleSemesterWeek GetModuleSemesterWeekByDate(Guid moduleId, DateTime studySessionDate, bool trackChanges);
        IEnumerable<ModuleSemesterWeek> GetModuleSemesterWeeksForAModule(Guid moduleId, bool trackChanges);
        void UpdateModuleSemesterWeeksForAModule(ModuleSemesterWeek moduleSemesterWeek);
    }
}
