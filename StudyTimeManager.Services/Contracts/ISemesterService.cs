using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Services.Contracts;

/// <summary>
/// Handles CRUD operations related to a semester
/// </summary>
public interface ISemesterService
{
    /// <summary>
    /// Creates a semester for a student
    /// </summary>
    /// <param name="semester">Semester to be created</param>
    /// <returns><see langword="true"/> if semester is created successfully, <see langword="false"/> if otherwise </returns>
    SemesterDTO CreateSemester(SemesterForCreationDTO semester);

    /// <summary>
    /// Retrieves the semester a student is in
    /// </summary>
    /// <returns>The semester is enrolled in</returns>
    SemesterDTO GetSemester(Guid Id, bool trackChanges);
}