using System;

namespace Shared.DTOs.ModuleSemesterWeek
{
    public record ModuleSemesterWeekDTO
    {
        public Guid Id { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public int WeekNumber { get; init; }
        public int RemainingSelfStudyHours { get; init; }
    }
}