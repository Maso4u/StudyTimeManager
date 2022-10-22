using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using System;

namespace StudyTimeManager.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContextFactory _repositoryContextFactory;
        private readonly Lazy<UserRepository> _userRepository;
        private readonly Lazy<SemesterRepository> _semesterRepository;
        private readonly Lazy<ModuleRepository> _moduleRepository;
        private readonly Lazy<ModuleSemesterWeekRepository> _moduleSemesterWeekRepository;
        private readonly Lazy<StudySessionRepository> _studySessionRepository;

        public RepositoryManager(RepositoryContextFactory repositoryContextFactory)
        {
            _repositoryContextFactory = repositoryContextFactory;
            _userRepository = new Lazy<UserRepository>(()=>
            new UserRepository(repositoryContextFactory));

            _semesterRepository = new Lazy<SemesterRepository>(() =>
            new SemesterRepository(repositoryContextFactory));

            _moduleRepository = new Lazy<ModuleRepository>(() =>
            new ModuleRepository(repositoryContextFactory));

            _moduleSemesterWeekRepository = new Lazy<ModuleSemesterWeekRepository>(() =>
            new ModuleSemesterWeekRepository(repositoryContextFactory));

            _studySessionRepository = new Lazy<StudySessionRepository>(() =>
            new StudySessionRepository(repositoryContextFactory));
        }

        public IUserRepository User => _userRepository.Value;

        public ISemesterRepository Semester => _semesterRepository.Value;

        public IModuleRepository Module => _moduleRepository.Value;

        public IModuleSemesterWeekRepository ModuleSemesterWeek => _moduleSemesterWeekRepository.Value;

        public IStudySessionRepository StudySession => _studySessionRepository.Value;

        /*public void Save()
        {
            _repositoryContextFactory.CreateDbContext().SaveChanges();
        }*/
    }
}
