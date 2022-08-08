namespace StudyTimeManager.Domain.Models
{
    public class Module
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int NumberOfCredits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public int RequiredWeeklySelfStudyHours { get; set; }
        public ICollection<ModuleSemesterWeek> Weeks { get; set; } = new List<ModuleSemesterWeek>();

        public ModuleSemesterWeek this[int week]
        {
            get
            {
                return Weeks.First(w => w.WeekNumber == week);
            }
        }

    }
}
