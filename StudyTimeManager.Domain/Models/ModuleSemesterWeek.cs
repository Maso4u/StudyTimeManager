using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StudyTimeManager.Domain.Models
{

    /// <summary>
    /// Represents a semester week in a <see cref="Module"/>
    /// </summary>
    public class ModuleSemesterWeek
    {
        [Column("ModuleSemesterWeekId")]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the start date for this semester week
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for this semester week
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the week number this semester week
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the remaining self study hours for this semester week
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int RemainingSelfStudyHours { get; set; }

        /// <summary>
        /// Gets or sets a collection of <see cref="StudySession"/>  
        /// for this semester week
        /// </summary>
        public ICollection<StudySession>? StudySessions { get; set; }

        [ForeignKey(nameof(Module))]
        public Guid ModuleId { get; set; }
        public Module? Module { get; set; }


        /// <summary>
        /// Gets a <see cref="StudySession"/> with <see cref="StudySession.Date"/> 
        /// equal to <paramref name="date"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public StudySession? this[DateTime date]
        {
            get
            {
                return StudySessions?.FirstOrDefault(ss => ss.Date.Equals(date));
            }
        }
    }
}