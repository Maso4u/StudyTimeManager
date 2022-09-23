using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTimeManager.Domain.Models;

/// <summary>
/// Represents a semester a user is registered to
/// </summary>
public class Semester
{
    [Column("SemesterId")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets number of weeks in this semester.
    /// </summary>
    [Required]
    [Range(1,52,ErrorMessage ="Your weeks must be more than 0 and less than or equal 52")]
    public int NumberOfWeeks { get; set; }

    /// <summary>
    /// Gets or sets the start date of this semester
    /// </summary>
    [Required]
    public DateOnly? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of this semester
    /// </summary>
    [Required]
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="Module"/> 
    /// that are in this semester
    /// </summary>
    public ICollection<Module>? Modules { get; set; }

    /// <summary>
    /// Gets a <see cref="Module"/> in <see cref="Modules"/> with a code equal to <paramref name="moduleCode"/> 
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <returns></returns>
    public Module? this[string? moduleCode]
    {
        get
        {
            return Modules.FirstOrDefault(m => m.Code.Equals(moduleCode));
        }
    }
}