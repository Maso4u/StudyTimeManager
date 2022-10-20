using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IStudySessionRepository
    {
        void CreateStudySession(StudySession studySession);
        void DeleteStudySession(StudySession studySession);
        void UpdateStudySession(StudySession studySession);
    }
}
