using AutoMapper;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.StudySession;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Data;

namespace StudyTimeManager.Services
{
    public class StudySessionService : IStudySessionService
    {
        private IRepositoryManager _repository;
        private IMapper _mapper;

        public StudySessionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public StudySessionDTO? CreateStudySession(Guid moduleId, StudySessionForCreationDTO studySession)
        {
            ModuleSemesterWeek moduleSemesterWeek = _repository.ModuleSemesterWeek
                .GetModuleSemesterWeekByDate(moduleId, studySession.Date, true);

            if (moduleSemesterWeek is null)
            {
                return null;
            }

            if (moduleSemesterWeek.RemainingSelfStudyHours >= studySession.HoursSpent)
            {
                moduleSemesterWeek.RemainingSelfStudyHours -= studySession.HoursSpent;

                StudySession studySessionEntity = _mapper.Map<StudySession>(studySession);
                studySessionEntity.ModuleSemesterWeekId = moduleSemesterWeek.Id;
                _repository.StudySession.CreateStudySession(studySessionEntity);

                _repository.Save();

                StudySessionDTO studySessionCreated = _mapper.Map<StudySessionDTO>(studySessionEntity);
                return studySessionCreated;
            }
            return null;
        }

        public void RemoveStudySession(Guid moduleId, StudySessionDTO studySessionDTO)
        {
            ModuleSemesterWeek moduleSemesterWeek = _repository.ModuleSemesterWeek
                .GetModuleSemesterWeekByDate(moduleId, studySessionDTO.Date, false);

            if (moduleSemesterWeek is null)
            {
                return;
            }
            StudySession studySession = _mapper.Map<StudySession>(studySessionDTO);
            moduleSemesterWeek.RemainingSelfStudyHours += studySessionDTO.HoursSpent;
            _repository.StudySession.DeleteStudySession(studySession);
            _repository.Save();
        }

        /// <summary>
        /// Adds <paramref name="hoursSpent"/> in a study session back to the remaining self study hours
        /// for the module with a code equal to 
        /// <paramref name="moduleCode"/> in the semester week number equal to 
        /// <paramref name="week"/>
        /// </summary>
        /// <param name="moduleCode">
        /// The code for the module the study session was made for
        /// </param>
        /// <param name="week">
        /// The semester week number the study session was created on
        /// </param>
        /// <param name="hoursSpent">
        /// The hours spent during the study session that are to be added back to the week for the module
        /// </param>
        private void AddbackWeekSelfStudyHoursLeft(string moduleCode, int week, int hoursSpent)
        {
            //_semester[moduleCode][week].RemainingSelfStudyHours += hoursSpent;
        }
    }
}