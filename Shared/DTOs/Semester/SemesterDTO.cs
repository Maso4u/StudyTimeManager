namespace Shared.DTOs.Semester;
public record SemesterDTO
{
    public Guid Id { get; init; }
    public int NumberOfWeeks { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
