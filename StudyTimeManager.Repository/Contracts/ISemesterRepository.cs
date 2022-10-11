using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;

namespace StudyTimeManager.Repository.Contracts
{
    public interface ISemesterRepository
    {
        void CreateSemester(Semester semester);
        Semester GetSemester(Guid semesterId, bool trackChanges);
        IEnumerable<Semester> GetAllSemesters(bool trackChanges);
    }
}