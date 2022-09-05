namespace StudyTimeManager.Domain.Models;

/// <summary>
/// Represents a study session in semester week for a module
/// </summary>
public class StudySession
{
    /// <summary>
    /// Gets or sets a date for this study session.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets hours spent in this study session.
    /// </summary>
    public int HoursSpent { get; set; }
}
