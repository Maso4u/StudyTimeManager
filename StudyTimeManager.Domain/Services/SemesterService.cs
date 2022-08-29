using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Globalization;

namespace StudyTimeManager.Domain.Services;
public class SemesterService : ISemesterService
{
    private readonly Semester _semester;

    public SemesterService(Semester semester)
    {
        _semester = semester;
    }

    public bool CreateSemester(Semester semester)
    {
        if (semester.NumberOfWeeks <= 0)
        {
            return false;
        }
        if (String.IsNullOrEmpty(semester.StartDate.ToString()))
        {
            return false;
        }

        _semester.StartDate = semester.StartDate;
        _semester.NumberOfWeeks = semester.NumberOfWeeks;
        _semester.EndDate = CalculateSemesterLastDay(semester.StartDate,semester.NumberOfWeeks);
        return true;
    }

    public int GetNumberOfWeeks() => _semester.NumberOfWeeks;

    public Semester GetSemester() => _semester;

    private DateOnly CalculateSemesterLastDay(DateOnly startDate, int numberOfWeeks)
    {
        Calendar calendar = CultureInfo.InvariantCulture.Calendar;
        DateTime lastDate = calendar.AddWeeks(startDate.ToDateTime(), numberOfWeeks);
        
        return DateOnly.FromDateTime(lastDate.AddDays(-1));
    }
}
