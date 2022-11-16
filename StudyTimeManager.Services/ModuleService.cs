using AutoMapper;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyTimeManager.Services
{
    ///<inheritdoc cref="IModuleService"/>
    public class ModuleService : IModuleService
    {

        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly Semester _semester;

        public ModuleService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ModuleDTO?> CreateModule(SemesterDTO semester, ModuleForCreationDTO module, bool trackChanges)
        {
            var moduleEntity = _mapper.Map<Module>(module);

            ///assign calculated requred weekly self-study hours and 
            ///assign to property of the module to be created
            moduleEntity.RequiredWeeklySelfStudyHours =
                CalculateRequiredWeeklySelfStudyHours(
                    module.NumberOfCredits, semester.NumberOfWeeks, module.ClassHoursPerWeek);

            ///Add module to collection of modules in semester and 
            ///check if it was successfully added with the contains method,
            ///return the result of that check
            await _repository.Module.CreateModuleForSemester(semester.Id, moduleEntity);
            
            return _mapper.Map<ModuleDTO>(moduleEntity);
        }

        public async Task<bool> DeleteModule(Guid semesterId, Guid moduleId)
        {
            Semester? semester = await _repository.Semester.GetSemester(semesterId, false);
            if (semester == null) return false;

            Module? semesterModule = await _repository.Module.GetModuleById(semesterId,moduleId,false);
            if (semesterModule == null) return false;

            await _repository.Module.DeleteModule(semesterModule);
            return true;
        }

        public async Task<ICollection<ModuleDTO>?> GetAllSemesterModules(Guid semesterId)
        {
            var result =await _repository.Module.GetAllModules(semesterId);
            ICollection<Module>? modules = result?.ToList();
            if (modules is null)
            {
                return null;
            }
            return _mapper.Map<ICollection<ModuleDTO>>(modules);
        }

        public async Task<ModuleDTO?> GetModule(Guid semesterId, Guid moduleId)
        {
            Module? moduleEntity = await _repository
                .Module.GetModuleById(semesterId, moduleId, false);
            if (moduleEntity is null)
            {
                return null;
            }

            return _mapper.Map<ModuleDTO>(moduleEntity);
        }

        public async Task<ModuleDTO?> GetModule(Guid semesterId, string moduleCode)
        {
            Module? moduleEntity = await _repository
                .Module.GetModuleByCode(semesterId, moduleCode, false);
            if (moduleEntity is null)
            {
                return null;
            }

            return _mapper.Map<ModuleDTO>(moduleEntity);
        }

        /// <summary>
        /// Calculates the required weekly self-study hours needed for 
        /// <paramref name="module"/> in its semester
        /// </summary>
        /// <param name="module">The module for which the self-study hours are being calculated for</param>
        /// <returns>The hours of weekly self-study hours.</returns>
        private int CalculateRequiredWeeklySelfStudyHours(int credits, int weeks, int classHoursPerWeek) =>
            ((credits * 10) / weeks) - classHoursPerWeek;
    }
}