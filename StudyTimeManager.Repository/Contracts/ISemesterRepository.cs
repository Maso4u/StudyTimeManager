using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface ISemesterRepository
    {
        Task CreateSemester(Semester semester);
        Task<Semester?> GetSemester(Guid semesterId, bool trackChanges);
        Task<Semester?> GetSemesterByUser(Guid userId, bool trackChanges);
        Task<IEnumerable<Semester>> GetAllSemesters(bool trackChanges);
        Task DeleteSemester(Semester semester);

    }
}