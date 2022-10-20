using Microsoft.EntityFrameworkCore;
using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyTimeManager.Repository
{
    internal sealed class SemesterRepository : RepositoryBase<Semester>, ISemesterRepository
    {
        public SemesterRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateSemester(Semester semester)
        {
            Create(semester);
        }

        public void DeleteSemester(Semester semester)
        {
            Delete(semester);
        }

        public IEnumerable<Semester> GetAllSemesters(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(s => s.StartDate)
                .ToList();
        }

        public Semester GetSemester(Guid semesterId, bool trackChanges)
        {
            return FindByCondition(e =>
            e.Id.Equals(semesterId), trackChanges).SingleOrDefault();
        }
    }
}