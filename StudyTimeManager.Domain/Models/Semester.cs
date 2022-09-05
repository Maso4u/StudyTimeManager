namespace StudyTimeManager.Domain.Models;

/// <summary>
/// Represents a semester a user is registered to
/// </summary>
public class Semester
{
    /// <summary>
    /// Gets or sets number of weeks in this semester.
    /// </summary>
    public int NumberOfWeeks { get; set; }

    /// <summary>
    /// Gets or sets the start date of this semester
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of this semester
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="Module"/> 
    /// that are in this semester
    /// </summary>
    public ICollection<Module> Modules { get; set; } = new List<Module>();

    /// <summary>
    /// Gets a <see cref="Module"/> in <see cref="Modules"/> with a code equal to <paramref name="moduleCode"/> 
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <returns></returns>
    public Module this[string? moduleCode]
    {
        get
        {
            return Modules.First(m => m.Code.Equals(moduleCode));
        }
    }
}