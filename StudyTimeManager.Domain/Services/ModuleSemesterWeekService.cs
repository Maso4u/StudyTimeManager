using StudyTimeManager.Domain.Extensions;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;
using System.Globalization;

namespace StudyTimeManager.Domain.Services
{
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
            int numberOfSemesterWeeks = _semester.NumberOfWeeks;

            for (int counter = 0; counter < numberOfSemesterWeeks; counter++)
            {
                DateTime firstDateOfWeek = calendar.AddWeeks(firstDateOfFirstWeek, counter);
                DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);

                ModuleSemesterWeek moduleSemesterWeek = new ModuleSemesterWeek()
                {
                    StartDate = DateOnly.FromDateTime(firstDateOfWeek),
                    EndDate = DateOnly.FromDateTime(lastDateOfWeek),
                    WeekNumber = counter + 1,
                    RemainingSelfStudyHours = _semester[moduleCode].RequiredWeeklySelfStudyHours
                };
                
                _semester[moduleCode].Weeks.Add(moduleSemesterWeek);
            }
        }

        public ModuleSemesterWeek GetModuleSemesterWeek(string moduleCode, int week)
        {
            return _semester[moduleCode][week];
        }

        public ICollection<ModuleSemesterWeek> GetModuleSemesterWeeksForAllModules(int week)
        {
            ICollection<ModuleSemesterWeek> moduleSemesterWeeksFound = new List<ModuleSemesterWeek>();
            foreach (var module in _semester.Modules)
            {
                moduleSemesterWeeksFound.Add(module[week]);
            }
            return moduleSemesterWeeksFound;
        }

        public bool UpdateSelfStudyHoursOfModuleSemesterWeek(string moduleCode, int week, int hoursToDeduct)
        {
            ModuleSemesterWeek moduleSemesterWeek = _semester[moduleCode][week];
            int oldRemainingHours = moduleSemesterWeek.RemainingSelfStudyHours;

            if (hoursToDeduct < oldRemainingHours)
            {
                _semester[moduleCode][week].RemainingSelfStudyHours -= hoursToDeduct;
                return true;
            }
            return false;
        }
    }
}
