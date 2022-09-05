using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Data;

namespace StudyTimeManager.Domain.Services;
public class StudySessionService : IStudySessionService
{
    private readonly Semester _semester;

    public StudySessionService(Semester semester)
    {
        _semester = semester;
    }

    public bool CreateStudySession(string moduleCode, int week, StudySession studySession)
    {
        //get the initial count of study sessions in the module and week
        //for which the new study session is being created for
        int initStudySessionsCount = _semester[moduleCode][week].StudySessions.Count;
        
        //Add a study session to the collection of study sessions for
        //the week number passed through method parameter
        _semester[moduleCode][week].StudySessions.Add(studySession);

        //check if the initial count of study sessions is less than the current count
        //which would mean the study session passed through the method parameter was successfully added
        return initStudySessionsCount < _semester[moduleCode][week].StudySessions.Count;
    }

    public bool RemoveStudySession(string moduleCode, int week, DateOnly date)
    {
        //retrieve study session to be removed from the module's semester week
        StudySession studySession = _semester[moduleCode][week][date];
        AddbackWeekSelfStudyHoursLeft(moduleCode, week, studySession.HoursSpent);
        //remove the retrieved study session from the collection of study session for
        return _semester[moduleCode][week].StudySessions.Remove(studySession);
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
        _semester[moduleCode][week].RemainingSelfStudyHours += hoursSpent;
    }
}