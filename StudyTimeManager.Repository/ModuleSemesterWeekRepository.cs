using Repository;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyTimeManager.Repository
{
    public class ModuleSemesterWeekRepository : RepositoryBase<ModuleSemesterWeek>,
        IModuleSemesterWeekRepository
    {
        public ModuleSemesterWeekRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateModuleSemesterWeeks(IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks)
        {
            foreach (var moduleSemesterWeek in moduleSemesterWeeks)
            {
                Create(moduleSemesterWeek);
            }
        }

        public ModuleSemesterWeek GetModuleSemesterWeekByDate(Guid moduleId, DateTime date, bool trackChanges)
        {
            return FindByCondition(m =>
            m.ModuleId.Equals(moduleId) && date >= m.StartDate && date <= m.EndDate, trackChanges)
                .SingleOrDefault();
        }

        public IEnumerable<ModuleSemesterWeek> GetModuleSemesterWeeksForAModule(Guid moduleId, bool trackChanges)
        {
            return FindByCondition(m =>
            m.ModuleId.Equals(moduleId), trackChanges)
                .OrderBy(m => m.WeekNumber)
                .ToList();
        }
    }
}
