using Shared.DTOs.Semester;
using System;
using System.Threading.Tasks;

namespace StudyTimeManager.Services.Contracts
{

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
        Task<SemesterDTO> CreateSemester(Guid studentId, SemesterForCreationDTO semester);

        /// <summary>
        /// Retrieves the semester a student is in
        /// </summary>
        /// <returns>The semester is enrolled in</returns>
        Task<SemesterDTO?> GetSemester(Guid Id, bool trackChanges);
        Task<bool> DeleteSemester(Guid Id);
    }
}