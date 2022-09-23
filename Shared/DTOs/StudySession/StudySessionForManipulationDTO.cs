namespace Shared.DTOs.StudySession
{
    public abstract record StudySessionForManipulationDTO
    {
        public DateOnly Date { get; init; }
        public int HoursSpent { get; init; }
    }
}
