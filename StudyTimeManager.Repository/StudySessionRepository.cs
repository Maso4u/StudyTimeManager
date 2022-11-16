using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository
{
    public class StudySessionRepository : RepositoryBase<StudySession>, IStudySessionRepository
    {
        public StudySessionRepository(RepositoryContextFactory repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task CreateStudySession(StudySession studySession)
        {
            await CreateAsync(studySession);
        }

        public async Task DeleteStudySession(StudySession studySession)
        {
            await DeleteAsync(studySession);
        }

        public async Task UpdateStudySession(StudySession studySession)
        {
            await UpdateAsync(studySession);
        }
    }
}
