using Repository;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;

namespace StudyTimeManager.Repository
{
    public class StudySessionRepository : RepositoryBase<StudySession>, IStudySessionRepository
    {
        public StudySessionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateStudySession(StudySession studySession)
        {
            Create(studySession);
        }

        public void DeleteStudySession(StudySession studySession)
        {
            Delete(studySession);
        }
    }
}
