using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository
{
    internal sealed class SemesterRepository : RepositoryBase<Semester>, ISemesterRepository
    {
        public SemesterRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateSemester(Semester semester)
        {
            await CreateAsync(semester);
        }

        public async Task DeleteSemester(Semester semester)
        {
            await DeleteAsync(semester);
        }

        public async Task<IEnumerable<Semester>> GetAllSemesters(bool trackChanges)
        {
            var result = await FindAllAsync(trackChanges);
            return result.OrderBy(s => s.StartDate).ToList();
        }

        public async Task<Semester?> GetSemester(Guid semesterId, bool trackChanges)
        {
            var result = await FindByConditionAsync(e =>
            e.Id.Equals(semesterId), trackChanges);
            return result.SingleOrDefault();
        }
        
        public async Task<Semester?> GetSemesterByUser(Guid userId, bool trackChanges)
        {
            var result = await FindByConditionAsync(s =>
            s.UserId.Equals(userId), trackChanges);
            return result.SingleOrDefault();
        }
    }
}