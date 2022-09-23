using StudyTimeManager.Repository.Contracts;

namespace StudyTimeManager.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<SemesterRepository> _semesterRepository;
        private readonly Lazy<ModuleRepository> _moduleRepository;
        private readonly Lazy<ModuleSemesterWeekRepository> _moduleSemesterWeekRepository;
        private readonly Lazy<StudySessionRepository> _studySessionRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _semesterRepository = new Lazy<SemesterRepository>(() =>
            new SemesterRepository(repositoryContext));

            _moduleRepository = new Lazy<ModuleRepository>(() =>
            new ModuleRepository(repositoryContext));

            _moduleSemesterWeekRepository = new Lazy<ModuleSemesterWeekRepository>(() =>
            new ModuleSemesterWeekRepository(repositoryContext));

            _studySessionRepository = new Lazy<StudySessionRepository>(() =>
            new StudySessionRepository(repositoryContext));
        }

        public ISemesterRepository Semester => _semesterRepository.Value;

        public IModuleRepository Module => _moduleRepository.Value;

        public IModuleSemesterWeekRepository ModuleSemesterWeek => _moduleSemesterWeekRepository.Value;

        public IStudySessionRepository StudySession => _studySessionRepository.Value;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
