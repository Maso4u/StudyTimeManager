using System;

namespace Shared.DTOs.StudySession
{
    public record StudySessionDTO
    {
        public Guid Id { get; init; }
        public DateTime Date { get; init; }
        public int HoursSpent { get; init; }
    }
}
