﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;

namespace StudyTimeManager.Services
{
    ///<inheritdoc cref="IServiceManager"/>
    public class ServiceManager : IServiceManager
    {
        /// <summary>
        /// Leverages Lazy class to ensure lazy initialization of services.
        /// Meaning instances are only created when their accessed for the first time
        /// </summary>
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IModuleService> _moduleService;
        private readonly Lazy<IModuleSemesterWeekService> _moduleSemesterWeekService;
        private readonly Lazy<IStudySessionService> _studySessionService;
        private readonly Lazy<ISemesterService> _semesterService;
        

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _authenticationService = new Lazy<IAuthenticationService>(
                () => new AuthenticationService(repositoryManager, mapper,passwordHasher));
            _moduleService = new Lazy<IModuleService>(() => new ModuleService(repositoryManager, mapper));
            _moduleSemesterWeekService = new Lazy<IModuleSemesterWeekService>(() => new ModuleSemesterWeekService(repositoryManager, mapper));
            _studySessionService = new Lazy<IStudySessionService>(() => new StudySessionService(repositoryManager, mapper));
            _semesterService = new Lazy<ISemesterService>(
                () => new SemesterService(repositoryManager, mapper));
        }

        public IModuleService ModuleService => _moduleService.Value;

        public IModuleSemesterWeekService ModuleSemesterWeekService => _moduleSemesterWeekService.Value;

        public IStudySessionService StudySessionService => _studySessionService.Value;
        public ISemesterService SemesterService => _semesterService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}