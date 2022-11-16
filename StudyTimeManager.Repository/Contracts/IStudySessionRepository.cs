using StudyTimeManager.Domain.Models;
using System.Threading.Tasks;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IStudySessionRepository
    {
        Task CreateStudySession(StudySession studySession);
        Task DeleteStudySession(StudySession studySession);
        Task UpdateStudySession(StudySession studySession);
    }
}
