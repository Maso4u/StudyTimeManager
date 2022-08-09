namespace StudyTimeManager.Domain.Models
{
    public class Semester
    {
        public int NumberOfWeeks { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Module> Modules  { get; set; } = new List<Module>();

        public Module this[string? moduleCode]
        {
            get
            {
                return Modules.First(m => m.Code.Equals(moduleCode));
            }
        }
    }
}
