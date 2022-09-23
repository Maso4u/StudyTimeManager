using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Repository.Contracts;
public interface ISemesterRepository
{
    void CreateSemester(Semester semester);
    Semester GetSemester(Guid semesterId, bool trackChanges);
    IEnumerable<Semester> GetAllSemesters(bool trackChanges);
}
