namespace StudyTimeManager.Domain.Models
{
    /// <summary>
    /// Represents a module in a <see cref="Semester"/>
    /// </summary>
    public class Module
    { 
        /// <summary>
        /// Gets or sets the Code of this module.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the name of this module.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the number of credits for this module.
        /// </summary>
        public int NumberOfCredits { get; set; }

        /// <summary>
        /// Gets or sets the number of weekly class hours for this module
        /// </summary>
        public int ClassHoursPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the required weekly self study hours for this module
        /// </summary>
        public int RequiredWeeklySelfStudyHours { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="ModuleSemesterWeek"/> for this module.
        /// </summary>
        public ICollection<ModuleSemesterWeek> Weeks { get; set; } = new List<ModuleSemesterWeek>();

        /// <summary>
        /// Gets a <see cref="ModuleSemesterWeek"/> from <see cref="Weeks"/>
        /// with <see cref="ModuleSemesterWeek.WeekNumber"/> 
        /// the week number equal to <paramref name="week"/>
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public ModuleSemesterWeek this[int week]
        {
            get
            {
                return Weeks.First(w => w.WeekNumber == week);
            }
        }

    }
}
