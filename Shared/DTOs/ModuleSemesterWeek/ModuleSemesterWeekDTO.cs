namespace Shared.DTOs.ModuleSemesterWeek;
public record ModuleSemesterWeekDTO
{
    public Guid Id { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public int WeekNumber { get; init; }
    public int RemainingSelfStudyHours { get; init; }
}