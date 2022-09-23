using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTimeManager.Domain.Models;

/// <summary>
/// Represents a study session in semester week for a module
/// </summary>
public class StudySession
{
    [Column("StudySessionId")]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets a date for this study session.
    /// </summary>
    [Required]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Gets or sets hours spent in this study session.
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int HoursSpent { get; set; }


    [ForeignKey(nameof(ModuleSemesterWeek))]
    public Guid ModuleSemesterWeekId { get; set; }
    public ModuleSemesterWeek? ModuleSemesterWeek { get; set; }
}
