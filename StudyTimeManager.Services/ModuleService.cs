using AutoMapper;
using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ModuleDTO? CreateModule(SemesterDTO semester, ModuleForCreationDTO module, bool trackChanges)
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
            _repository.Module.CreateModuleForSemester(semester.Id, moduleEntity);
            _repository.Save();

            return _mapper.Map<ModuleDTO>(moduleEntity);
        }

        public bool DeleteModule(string moduleCode)
        {
            ///find module with the code given through parameter in collection in semester and 
            ///pass it into the Remove method of the semester's module collection as an argument.
            return _semester.Modules.Remove(_semester[moduleCode]);
        }

        public ModuleDTO GetModule(Guid semesterId, string moduleCode)
        {
            Module moduleEntity = _repository
                .Module.GetModuleByCode(semesterId, moduleCode, false);
            return _mapper.Map<ModuleDTO>(moduleEntity);
        }

        public ICollection<Module> GetModules() => _semester.Modules;

        public void UpdateModule(Module module)
        {
            ///get the index of the module to update in the collection of semester modules and
            ///assign module given through the parameter to the module in the index
            int moduleIndex = _semester.Modules.ToList().IndexOf(module);
            _semester.Modules.ToList()[moduleIndex] = module;
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