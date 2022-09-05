using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Globalization;

namespace StudyTimeManager.Domain.Services
{
    ///<inheritdoc cref="IModuleSemesterWeekService"/>
    public class ModuleSemesterWeekService : IModuleSemesterWeekService
    {
        private readonly Semester _semester;

        public ModuleSemesterWeekService(Semester semester)
        {
            _semester = semester;
        }

        public void CreateModuleSemesterWeeks(string moduleCode)
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;

            DateTime firstDateOfFirstWeek = _semester.StartDate.ToDateTime();
            for (int counter = 0; counter < _semester.NumberOfWeeks; counter++)
            {
                //determine the first and last date of a week
                DateTime firstDateOfWeek = calendar.AddWeeks(firstDateOfFirstWeek, counter);
                DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);

                ///create a new semester week with the calculated start and end date
                ///and remaining self-study hours being 
                ///the required self-study hours calculated for a module by default
                ModuleSemesterWeek moduleSemesterWeek = new ModuleSemesterWeek()
                {
                    StartDate = DateOnly.FromDateTime(firstDateOfWeek),
                    EndDate = DateOnly.FromDateTime(lastDateOfWeek),
                    WeekNumber = counter + 1,
                    RemainingSelfStudyHours = _semester[moduleCode].RequiredWeeklySelfStudyHours
                };

                ///Adds the created semester week to 
                ///the collection of semester weeks in the module
                ///with a code equal to the value of the parameter
                _semester[moduleCode].Weeks.Add(moduleSemesterWeek);
            }
        }

        public ModuleSemesterWeek GetModuleSemesterWeek(string moduleCode, int week)
        {
            return _semester[moduleCode][week];
        }

        public ICollection<ModuleSemesterWeek>? GetModuleSemesterWeeksForAModule(string? moduleCode)
        {
            try
            {
                ///retrieve semester weeks from module with a code stored in parameter and return it
                ICollection<ModuleSemesterWeek> moduleSemesterWeeksFound = _semester[moduleCode].Weeks;
                return moduleSemesterWeeksFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int hoursToDeduct)
        {
            //retrieve semester week for the module to be updated
            ModuleSemesterWeek moduleSemesterWeek = _semester[moduleCode][week];
            int oldRemainingHours = moduleSemesterWeek.RemainingSelfStudyHours;
            
            //if the study hours to deduct are less than the studyhours currently left for the week
            //then those hours should be deducted and a result of true must be returned by the method
            if (hoursToDeduct < oldRemainingHours)
            {
                _semester[moduleCode][week].RemainingSelfStudyHours -= hoursToDeduct;
                return true;
            }
            return false;
        }
    }
}
