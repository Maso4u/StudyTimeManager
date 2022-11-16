using AutoMapper;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.StudySession;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

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

        public async Task<StudySessionDTO?> 
            CreateStudySession (Guid moduleId, StudySessionForCreationDTO studySession)
        {
            ModuleSemesterWeek? moduleSemesterWeek = await _repository.ModuleSemesterWeek
                .GetModuleSemesterWeekByDate(moduleId, studySession.Date, false);

            if (moduleSemesterWeek is null)
            {
                return null;
            }

            if (moduleSemesterWeek.RemainingSelfStudyHours >= studySession.HoursSpent)
            {
                moduleSemesterWeek.RemainingSelfStudyHours -= studySession.HoursSpent;

                StudySession studySessionEntity = _mapper.Map<StudySession>(studySession);
                studySessionEntity.ModuleSemesterWeekId = moduleSemesterWeek.Id;
                await _repository.StudySession.CreateStudySession(studySessionEntity);
                await _repository.ModuleSemesterWeek.UpdateModuleSemesterWeeksForAModule(moduleSemesterWeek);
                //_service.ModuleSemesterWeekService.UpdateModuleSemesterWeekForAModule(message.Value.Item2);
                //_repository.Save();

                StudySessionDTO studySessionCreated = _mapper.Map<StudySessionDTO>(studySessionEntity);
                return (studySessionCreated);
            }
            return null;
        }

        public async Task RemoveStudySession(Guid moduleId, StudySessionDTO studySessionDTO)
        {
            ModuleSemesterWeek? moduleSemesterWeek = await _repository.ModuleSemesterWeek
                .GetModuleSemesterWeekByDate(moduleId, studySessionDTO.Date, false);

            if (moduleSemesterWeek is null)
            {
                return;
            }

            StudySession studySession = _mapper.Map<StudySession>(studySessionDTO);
            moduleSemesterWeek.RemainingSelfStudyHours += studySessionDTO.HoursSpent;
            await _repository.StudySession.DeleteStudySession(studySession);
            await _repository.ModuleSemesterWeek.UpdateModuleSemesterWeeksForAModule(moduleSemesterWeek);
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