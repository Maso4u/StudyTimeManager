namespace StudyTimeManager.Domain.Services.Contracts
{
    public interface IServiceManager
    {
        IModuleService ModuleService { get; }
        IModuleSemesterWeekService ModuleSemesterWeekService { get; }
        IStudySessionService StudySessionService { get; }
        ISemesterService SemesterService { get; }
    }
}
