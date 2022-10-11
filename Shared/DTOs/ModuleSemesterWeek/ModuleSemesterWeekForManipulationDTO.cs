using System;

namespace Shared.DTOs.ModuleSemesterWeek
{
    public abstract record ModuleSemesterWeekForManipulationDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int WeekNumber { get; set; }
        public int RemainingSelfStudyHours { get; set; }
    }
}
