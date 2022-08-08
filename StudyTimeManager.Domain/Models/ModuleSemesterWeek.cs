namespace StudyTimeManager.Domain.Models
{
    public class ModuleSemesterWeek
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int WeekNumber { get; set; }
        public int RemainingSelfStudyHours { get; set; }
        public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
    }
}
