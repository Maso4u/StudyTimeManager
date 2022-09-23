using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Services.Contracts;
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
    void CreateModuleSemesterWeeks(ModuleDTO module, SemesterDTO semester);

    /// <summary>
    /// Retrieves a collection of semester weeks from a module with a code equal to <paramref name="moduleCode"/>
    /// </summary>
    /// <param name="moduleCode">
    /// The module code of the module for which a collection of semester weeks are being retrieved 
    /// </param>
    /// <returns>A collection of semester weeks</returns>
    IEnumerable<ModuleSemesterWeekDTO>? GetModuleSemesterWeeksForAModule(Guid ModuleId);
}
