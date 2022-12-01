using Repository;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository
{
    public class ModuleSemesterWeekRepository : RepositoryBase<ModuleSemesterWeek>,
        IModuleSemesterWeekRepository
    {
        public ModuleSemesterWeekRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateModuleSemesterWeeks(IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks)
        {
            foreach (var moduleSemesterWeek in moduleSemesterWeeks)
            {
                await CreateAsync(moduleSemesterWeek);
            }
        }

        public async Task<ModuleSemesterWeek?> GetModuleSemesterWeekByDate(Guid moduleId, DateTime date, bool trackChanges)
        {
            var result = await FindByConditionAsync(m =>
            m.ModuleId.Equals(moduleId) && date >= m.StartDate && date <= m.EndDate, trackChanges);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ModuleSemesterWeek>> GetModuleSemesterWeeksForAModule(Guid moduleId, bool trackChanges)
        {
            var result = await FindByConditionAsync(m =>
        m.ModuleId.Equals(moduleId), trackChanges);
            return result.OrderBy(m => m.WeekNumber)
                .ToList();
        }

        public async Task UpdateModuleSemesterWeeksForAModule(ModuleSemesterWeek moduleSemesterWeek)
        {
            await UpdateAsync(moduleSemesterWeek);
        }
    }
}
