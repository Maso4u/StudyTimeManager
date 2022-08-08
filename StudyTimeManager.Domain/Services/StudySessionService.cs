using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

namespace StudyTimeManager.Domain.Services
{
    public class StudySessionService : IStudySessionService
    {
        private readonly Semester _semester;

        public StudySessionService(Semester semester)
        {
            _semester = semester;
        }

        public bool CreateStudySession(string moduleCode, int week, StudySession studySession)
        {
            int initStudySessions= _semester.Modules
                .First(m => m.Code.Equals(moduleCode))[week]
                .StudySessions.Count;
            
            _semester.Modules
                .First(m => m.Code.Equals(moduleCode))[week]
                .StudySessions.Add(studySession);

            return initStudySessions < _semester.Modules
                .First(m => m.Code.Equals(moduleCode))[week]
                .StudySessions.Count;
        }
    }
}
