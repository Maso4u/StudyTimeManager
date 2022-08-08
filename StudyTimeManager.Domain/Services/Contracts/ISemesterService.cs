using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface ISemesterService
    {
        bool CreateSemester(Semester semester);

        Semester GetSemester();
        int GetNumberOfWeeks();
    }
}