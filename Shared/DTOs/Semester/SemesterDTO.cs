using System;

namespace Shared.DTOs.Semester
{
    public record SemesterDTO
    {
        public Guid Id { get; init; }
        public int NumberOfWeeks { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
