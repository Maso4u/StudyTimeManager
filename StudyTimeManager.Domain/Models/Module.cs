using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StudyTimeManager.Domain.Models
{
    /// <summary>
    /// Represents a module in a <see cref="Semester"/>
    /// </summary>
    public class Module
    {
        [Column("ModuleId")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Code of this module.
        /// </summary>
        [Required]
        [MaxLength(8, ErrorMessage ="Maximum length for the Code is 8 characters")]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the name of this module.
        /// </summary>
        [Required]
        [MaxLength(50,ErrorMessage ="Maximum length for the Name is 50")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the number of credits for this module.
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int NumberOfCredits { get; set; }

        /// <summary>
        /// Gets or sets the number of weekly class hours for this module
        /// </summary>
        [Required]
        [Range(1,int.MaxValue)]
        public int ClassHoursPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the required weekly self study hours for this module
        /// </summary>
        [Required]
        [Range(0,int.MaxValue)]
        public int RequiredWeeklySelfStudyHours { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="ModuleSemesterWeek"/> for this module.
        /// </summary>
        public ICollection<ModuleSemesterWeek>? Weeks { get; set; }

        [ForeignKey(nameof(Semester))]
        public Guid SemesterId { get; set; }
        public Semester? Semester { get; set; }

        /// <summary>
        /// Gets a <see cref="ModuleSemesterWeek"/> from <see cref="Weeks"/>
        /// with <see cref="ModuleSemesterWeek.WeekNumber"/> 
        /// the week number equal to <paramref name="week"/>
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public ModuleSemesterWeek? this[int week]
        {
            get
            {
                return Weeks?.FirstOrDefault(w => w.WeekNumber == week);
            }
        }

    }
}
