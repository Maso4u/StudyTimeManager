namespace Shared.DTOs.StudySession;
public record StudySessionDTO
{
    public Guid Id { get; init; }
    public DateOnly? Date { get; init; }
    public int HoursSpent { get; init; }
}
