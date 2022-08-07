namespace StudyTimeManager.Domain.Models
{
    public class ModuleSemesterWeek
    {
        public int WeekNumber { get; set; }
        public int RemainingSelfStudyHours { get; set; }
        public ICollection<StudySession> StudySessions = new List<StudySession>();
    }
}
