using System;

namespace Shared.DTOs.Module 
{ 
    public record ModuleDTO
    {
        public Guid Id { get; init; }
        public string? Code { get; init; }
        public string? Name { get; init; }
        public int NumberOfCredits { get; init; }

        public int ClassHoursPerWeek { get; init; }

        public int RequiredWeeklySelfStudyHours { get; init; }
    }
}
