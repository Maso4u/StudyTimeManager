namespace StudyTimeManager.Services.Contracts
{
    /// <summary>
    /// Creates instances of service user classes and registers them in the DI container.
    /// </summary>
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }

        /// <summary>
        /// Gets the module service
        /// </summary>
        IModuleService ModuleService { get; }

        /// <summary>
        /// Gets the module semester week service
        /// </summary>
        IModuleSemesterWeekService ModuleSemesterWeekService { get; }

        /// <summary>
        /// Gets the study session service
        /// </summary>
        IStudySessionService StudySessionService { get; }

        /// <summary>
        /// Gets the semester service
        /// </summary>
        ISemesterService SemesterService { get; }


    }
}