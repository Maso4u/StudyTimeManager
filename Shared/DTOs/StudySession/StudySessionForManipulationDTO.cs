using System;

namespace Shared.DTOs.StudySession
{
    public abstract record StudySessionForManipulationDTO
    {
        public DateTime Date { get; init; }
        public int HoursSpent { get; init; }
    }
}
