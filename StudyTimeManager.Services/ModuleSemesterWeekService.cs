using AutoMapper;
using Shared.DTOs.Module;
using Shared.DTOs.ModuleSemesterWeek;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace StudyTimeManager.Services
{
    ///<inheritdoc cref="IModuleSemesterWeekService"/>
    public class ModuleSemesterWeekService : IModuleSemesterWeekService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ModuleSemesterWeekService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public async Task CreateModuleSemesterWeeks(ModuleDTO module, SemesterDTO semester)
        {
            List<ModuleSemesterWeek> moduleSemesterWeeks = new List<ModuleSemesterWeek>();
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            DateTime firstDateOfFirstWeek = semester.StartDate;
            int week = 0;

            while (week < semester.NumberOfWeeks)
            {
                //determine the first and last date of a week
                DateTime firstDateOfWeek = calendar.AddWeeks(firstDateOfFirstWeek, week);
                DateTime lastDateOfWeek = firstDateOfWeek.AddDays(6);

                ModuleSemesterWeek moduleSemesterWeek = new()
                {
                    StartDate = firstDateOfWeek,
                    EndDate = lastDateOfWeek,
                    WeekNumber = week+1,
                    RemainingSelfStudyHours = module.RequiredWeeklySelfStudyHours,
                    ModuleId = module.Id
                };

                moduleSemesterWeeks.Add(moduleSemesterWeek);
                week++;
            }
            await _repository.ModuleSemesterWeek.CreateModuleSemesterWeeks(moduleSemesterWeeks);
        }

        public async Task<IEnumerable<ModuleSemesterWeekDTO>?> GetModuleSemesterWeeksForAModule(Guid moduleId)
        {
            IEnumerable<ModuleSemesterWeek> moduleSemesterWeeks = await _repository.ModuleSemesterWeek
                    .GetModuleSemesterWeeksForAModule(moduleId, false);

            IEnumerable<ModuleSemesterWeekDTO> moduleSemesterWeeksDTO = _mapper
                .Map<IEnumerable<ModuleSemesterWeekDTO>>(moduleSemesterWeeks);

            return moduleSemesterWeeksDTO;
        }

        public async Task UpdateModuleSemesterWeekForAModule(ModuleSemesterWeekDTO moduleSemesterWeek)
        {
            ModuleSemesterWeek moduleSemesterWeekToUpdate = _mapper.Map<ModuleSemesterWeek>(moduleSemesterWeek);
            await _repository.ModuleSemesterWeek.UpdateModuleSemesterWeeksForAModule(moduleSemesterWeekToUpdate);
        }
    }
}
