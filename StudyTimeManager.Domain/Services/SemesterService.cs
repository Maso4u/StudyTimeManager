using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Globalization;

namespace StudyTimeManager.Domain.Services;

/// <inheritdoc cref="ISemesterService"/>
public class SemesterService : ISemesterService
{
    private readonly Semester _semester;

    public SemesterService(Semester semester)
    {
        _semester = semester;
    }

    public bool CreateSemester(Semester semester)
    {
        ///validate whether or not the number of weeks of semester parameter value are <= 0.
        if (semester.NumberOfWeeks <= 0)
        {
            return false;
        }

        ///validate whether or not start date of semester parameter value is null or empty.
        if (String.IsNullOrEmpty(semester.StartDate.ToString()))
        {
            return false;
        }

        ///assign properties of this methods argument to _semester field
        _semester.StartDate = semester.StartDate;
        _semester.NumberOfWeeks = semester.NumberOfWeeks;
        _semester.EndDate = CalculateSemesterLastDay(semester.StartDate,semester.NumberOfWeeks);

        return true;
    }

    public int GetNumberOfWeeks() => _semester.NumberOfWeeks;

    public Semester GetSemester() => _semester;

    /// <summary>
    /// Calculates the last day of the semester and returns it as type DateOnly
    /// </summary>
    /// <remarks>
    /// This is done based on <paramref name="startDate"/> and 
    /// <paramref name="numberOfWeeks"/> for the semester
    /// </remarks>
    /// <param name="startDate">The start date of the semester</param>
    /// <param name="numberOfWeeks">The number of weeks in the semester</param>
    /// <returns>A DateOnly</returns>
    private DateOnly CalculateSemesterLastDay(DateOnly startDate, int numberOfWeeks)
    {

        Calendar calendar = CultureInfo.InvariantCulture.Calendar;

        ///determine date that is the number of the semester's weeks away 
        ///and gets date before then to determine last day of semester
        DateTime lastDate = calendar
            .AddWeeks(startDate.ToDateTime(), numberOfWeeks)
            .AddDays(-1);
        
        return DateOnly.FromDateTime(lastDate);
    }
}
