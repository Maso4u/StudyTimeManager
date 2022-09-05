using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts;
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
    void CreateModuleSemesterWeeks(string moduleCode);

    /// <summary>
    /// Updates the self-study hours of module on a specific week
    /// </summary>
    /// <param name="moduleCode">The code for the module thats being updated</param>
    /// <param name="week">The week for which the self-study hours are being updated</param>
    /// <param name="remainingHoursLeft">The new value to be set on the weeks' self-study hours</param>
    /// <returns>
    /// <see langword="true"/> when self-study hours for the module in the specified week
    /// is successfully updated, <see langword="false"/> if unsuccessful
    /// </returns>
    bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int remainingHoursLeft);
    
    /// <summary>
    /// Retrieves the semester week for the module with a code equal to <paramref name="moduleCode"/>
    /// and with a week number equal to <paramref name="week"/>
    /// </summary>
    /// <param name="moduleCode">
    /// The module code of the module for which a semester week is being retrieved
    /// </param>
    /// <param name="week">
    /// The week number a semester week represents 
    /// in the semester for the module with a code equal to <paramref name="moduleCode"/>
    /// </param>
    /// <returns>A semester week</returns>
    ModuleSemesterWeek GetModuleSemesterWeek(string moduleCode, int week);

    /// <summary>
    /// Retrieves a collection of semester weeks from a module with a code equal to <paramref name="moduleCode"/>
    /// </summary>
    /// <param name="moduleCode">
    /// The module code of the module for which a collection of semester weeks are being retrieved 
    /// </param>
    /// <returns>A collection of semester weeks</returns>
    ICollection<ModuleSemesterWeek>? GetModuleSemesterWeeksForAModule(string moduleCode);
}
