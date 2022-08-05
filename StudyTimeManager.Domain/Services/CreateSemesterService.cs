using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

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
        return true;
    }
}
