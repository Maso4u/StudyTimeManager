namespace Shared.DTOs.Semester;

public abstract record SemesterForManipulationDTO
{
    public int NumberOfWeeks { get; init; }
    public DateOnly StartDate { get; init; }
}