using System;

namespace Shared.DTOs.Semester
{

    public abstract record SemesterForManipulationDTO
    {
        public int NumberOfWeeks { get; init; }
        public DateTime StartDate { get; init; }
    }
}