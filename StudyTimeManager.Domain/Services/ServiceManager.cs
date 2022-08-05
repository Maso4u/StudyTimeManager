using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

namespace StudyTimeManager.Domain.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IModuleService> _moduleService;
        private readonly Lazy<IModuleSemesterWeekService> _moduleSemesterWeekService;
        private readonly Lazy<IStudySessionService> _studySessionService;
        private readonly Lazy<ISemesterService> _semesterService;

        public ServiceManager( Semester semester)
        {
            _moduleService = new Lazy<IModuleService>(()=> new ModuleService(semester));
            _moduleSemesterWeekService = new Lazy<IModuleSemesterWeekService>(()=> new ModuleSemesterWeekService(semester));
            _studySessionService = new Lazy<IStudySessionService>(()=> new StudySessionService(semester));
            _semesterService = new Lazy<ISemesterService>(() => new SemesterService(semester));
        }

        public IModuleService ModuleService => _moduleService.Value;

        public IModuleSemesterWeekService ModuleSemesterWeekService => _moduleSemesterWeekService.Value;

        public IStudySessionService StudySessionService => _studySessionService.Value;
        public ISemesterService SemesterService => _semesterService.Value;
    }
}
