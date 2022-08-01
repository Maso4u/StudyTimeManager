using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface IStudySessionService
    {
        bool CreateStudySession(string moduleCode, int week,StudySession studySession);
    }
}
