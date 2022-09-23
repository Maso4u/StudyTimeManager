using Microsoft.EntityFrameworkCore;
using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;

namespace StudyTimeManager.Repository;
internal sealed class SemesterRepository : RepositoryBase<Semester>, ISemesterRepository
{
    public SemesterRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public void CreateSemester(Semester semester)
    {
        Create(semester);
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
