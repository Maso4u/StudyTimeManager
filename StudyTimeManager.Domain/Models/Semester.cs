namespace StudyTimeManager.Domain.Models
{
    public class Semester
    {
        public int NumberOfWeeks { get; set; }
        public DateOnly StartDate { get; set; }
        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
