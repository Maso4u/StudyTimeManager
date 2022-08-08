﻿using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface IModuleSemesterWeekService
    {
        void CreateModuleSemesterWeeks(string moduleCode);
        bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int remainingHoursLeft);
        ICollection<ModuleSemesterWeek> GetModuleSemesterWeeksForAllModules(int week);
    }
}
