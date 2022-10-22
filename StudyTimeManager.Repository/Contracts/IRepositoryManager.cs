using StudyTimeManager.Repository.Contracts;

namespace StudyTimeManager.Repository.Contracts
{
    public interface IRepositoryManager
    {
        ISemesterRepository Semester { get;}
        IModuleRepository Module { get; }
        IModuleSemesterWeekRepository ModuleSemesterWeek { get;}
        IStudySessionRepository StudySession { get; }
        IUserRepository User { get; }
        //void Save();
    }
}
