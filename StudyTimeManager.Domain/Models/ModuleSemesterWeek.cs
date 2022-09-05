namespace StudyTimeManager.Domain.Models;

/// <summary>
/// Represents a semester week in a <see cref="Module"/>
/// </summary>
public class ModuleSemesterWeek
{
    /// <summary>
    /// Gets or sets the start date for this semester week
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for this semester week
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Gets or sets the week number this semester week
    /// </summary>
    public int WeekNumber { get; set; }

    /// <summary>
    /// Gets or sets the remaining self study hours for this semester week
    /// </summary>
    public int RemainingSelfStudyHours { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="StudySession"/>  
    /// for this semester week
    /// </summary>
    public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();

    /// <summary>
    /// Gets a <see cref="StudySession"/> with <see cref="StudySession.Date"/> 
    /// equal to <paramref name="date"/>
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public StudySession this[DateOnly date]
    {
        get
        {
            return StudySessions.First(ss => ss.Date.Equals(date));
        }
    }
}