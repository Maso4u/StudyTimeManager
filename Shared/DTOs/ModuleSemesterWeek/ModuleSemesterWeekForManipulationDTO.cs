namespace Shared.DTOs.ModuleSemesterWeek;
public abstract record ModuleSemesterWeekForManipulationDTO
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int WeekNumber { get; set; }
    public int RemainingSelfStudyHours { get; set; }
}
