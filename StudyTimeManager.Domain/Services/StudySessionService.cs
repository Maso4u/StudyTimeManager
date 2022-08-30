using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Data;

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
            int initStudySessions= _semester[moduleCode][week].StudySessions.Count;
            
            _semester[moduleCode][week].StudySessions.Add(studySession);

            return initStudySessions < _semester[moduleCode][week].StudySessions.Count;
        }

        public bool RemoveStudySession(string moduleCode, int week, DateOnly date)
        {
            StudySession studySession = _semester[moduleCode][week][date];
            UpdateWeekSelfStudyHoursLeft(moduleCode, week, studySession.HoursSpent);
            return _semester[moduleCode][week].StudySessions.Remove(studySession);
        }

        private void UpdateWeekSelfStudyHoursLeft(string moduleCode, int week, int hoursSpent)
        {
            _semester[moduleCode][week].RemainingSelfStudyHours += hoursSpent;
        }
    }
}
