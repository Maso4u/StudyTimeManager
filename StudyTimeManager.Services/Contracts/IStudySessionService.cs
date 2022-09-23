using Shared.DTOs.StudySession;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Services.Contracts;

/// <summary>
/// Handles CRUD operations for a module study session
/// </summary>
public interface IStudySessionService
{
    /// <summary>
    /// Creates a study session for a module with a code equal to
    /// <paramref name="moduleCode"/> for for a semester week equal to 
    /// <paramref name="week"/>
    /// </summary>
    /// <param name="moduleCode">
    /// The code for the module the study session is being created for
    /// </param>
    /// <param name="week">
    /// The week number in the semester the study session is created on.
    /// </param>
    /// <param name="studySession">The study session to be created.</param>
    /// <returns></returns>
    StudySessionDTO CreateStudySession(Guid moduleId, StudySessionForCreationDTO studySession);

    /// <summary>
    /// Removes study session in module with a code equal to <paramref name="moduleCode"/>
    /// on the a semester week equal to <paramref name="week"/> and with a date equal to
    /// <paramref name="date"/>
    /// </summary>
    /// <param name="moduleCode">
    /// The code for the module the study session is to be removed from
    /// </param>
    /// <param name="week">
    /// The week on which the study session was created
    /// </param>
    /// <param name="date">Date for which the study session was created</param>
    /// <returns>
    /// <see langword="true"/> if study session was successfully removed, 
    /// <see langword="false"/> if otherwise</returns>
    bool RemoveStudySession(string moduleCode, int week, DateOnly date);
}