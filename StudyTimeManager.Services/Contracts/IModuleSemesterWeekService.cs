using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Services.Contracts
{
    /// <summary>
    /// Handles CRUD operations for a modules semester week
    /// </summary>
    public interface IModuleSemesterWeekService
    {
        /// <summary>
        /// Creates a semester week for a module.
        /// </summary>
        /// <param name="moduleCode">
        /// The code of the module for which a semester week is being created.
        /// </param>
        Task CreateModuleSemesterWeeks(ModuleDTO module, SemesterDTO semester);

        /// <summary>
        /// Retrieves a collection of semester weeks from a module with a code equal to <paramref name="moduleCode"/>
        /// </summary>
        /// <param name="moduleCode">
        /// The module code of the module for which a collection of semester weeks are being retrieved 
        /// </param>
        /// <returns>A collection of semester weeks</returns>
        Task<IEnumerable<ModuleSemesterWeekDTO>?> GetModuleSemesterWeeksForAModule(Guid ModuleId);
        Task UpdateModuleSemesterWeekForAModule(ModuleSemesterWeekDTO moduleSemesterWeek);
    }
}