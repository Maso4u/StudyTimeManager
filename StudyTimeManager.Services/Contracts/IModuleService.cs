using Shared.DTOs.Module;
using Shared.DTOs.Semester;
using StudyTimeManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyTimeManager.Services.Contracts
{
    /// <summary>
    /// Handles CRUD operations related to Modules for a semester
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Creates a module for a semester by adding it to a collection of modules found in semester
        /// </summary>
        /// <param name="module">The module to be create</param>
        /// <returns>
        /// <see langword="true"/> if module was successfully created,
        /// <see langword="false"/> if otherwise
        /// </returns>
        Task<ModuleDTO?> CreateModule(SemesterDTO semester, ModuleForCreationDTO module, bool trackChanges);

        /// <summary>
        /// Deletes a module from the collection of modules found in a semester
        /// </summary>
        /// <param name="moduleCode">The module code of the module to be deleted</param>
        /// <returns>
        /// <see langword="true"/> if the module with the code was found and deleted successfully,
        /// <see langword="false"/> if otherwise.
        /// </returns>
        Task<bool> DeleteModule(Guid semesterId, Guid moduleId);

        /// <summary>
        /// Updates module with the same module code as one of the module code given as a parameter
        /// by assigning the values of the properties of the parameter module onto the found module
        /// </summary>
        /// <param name="module"></param>
        //Task UpdateModule(ModuleDTO module);

        /// <summary>
        /// Retrieves a module from the collection of modules found in a semester 
        /// where-by the module code equal <paramref name="moduleCode"/>
        /// </summary>
        /// <param name="moduleCode">The module code for the <see cref="Module"/> to retrieve</param>
        /// <returns>A module with a code equal to <paramref name="moduleCode"/></returns>
        Task<ModuleDTO?> GetModule(Guid semesterId, Guid moduleId);
        Task<ModuleDTO?> GetModule(Guid semesterId, string moduleCode);

        /// <summary>
        /// Retrieves the collection of a semester
        /// </summary>
        /// <returns>A collection of the modules in a semester</returns>
        Task<ICollection<ModuleDTO>?> GetAllSemesterModules(Guid semesterId);
    }
}