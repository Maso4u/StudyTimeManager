using AutoMapper;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.StudySession;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System.Data;

namespace StudyTimeManager.Services;
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
            .GetModuleSemesterWeekByDate(moduleId,studySession.Date,true);

        if (moduleSemesterWeek is null)
        {
            return null;
        }

        if (moduleSemesterWeek.RemainingSelfStudyHours>=studySession.HoursSpent)
        {
            moduleSemesterWeek.RemainingSelfStudyHours-=studySession.HoursSpent;

            StudySession studySessionEntity = _mapper.Map<StudySession>(studySession);
            studySessionEntity.ModuleSemesterWeekId = moduleSemesterWeek.Id;
            _repository.StudySession.CreateStudySession(studySessionEntity);

            //_mapper.Map(moduleSemesterForUpdate, moduleSemesterWeek);
            _repository.Save();

            StudySessionDTO studySessionCreated = _mapper.Map<StudySessionDTO>(studySessionEntity);
            return studySessionCreated;
        }
        return null;
    }

    public bool RemoveStudySession(string moduleCode, int week, DateOnly date)
    {
        /*
                //retrieve study session to be removed from the module's semester week
                StudySession studySession = _semester[moduleCode][week][date];
                AddbackWeekSelfStudyHoursLeft(moduleCode, week, studySession.HoursSpent);
                //remove the retrieved study session from the collection of study session for
                return _semester[moduleCode][week].StudySessions.Remove(studySession);
        */
        return false;
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