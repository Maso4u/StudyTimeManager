using StudyTimeManager.Domain.Models;

namespace StudyTimeManager.Domain.Services.Contracts;
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
    bool CreateModule(Module module);

    /// <summary>
    /// Deletes a module from the collection of modules found in a semester
    /// </summary>
    /// <param name="moduleCode">The module code of the module to be deleted</param>
    /// <returns>
    /// <see langword="true"/> if the module with the code was found and deleted successfully,
    /// <see langword="false"/> if otherwise.
    /// </returns>
    bool DeleteModule(string moduleCode);

    /// <summary>
    /// Updates module with the same module code as one of the module code given as a parameter
    /// by assigning the values of the properties of the parameter module onto the found module
    /// </summary>
    /// <param name="module"></param>
    void UpdateModule(Module module);

    /// <summary>
    /// Retrieves a module from the collection of modules found in a semester 
    /// where-by the module code equal <paramref name="moduleCode"/>
    /// </summary>
    /// <param name="moduleCode">The module code for the <see cref="Module"/> to retrieve</param>
    /// <returns>A module with a code equal to <paramref name="moduleCode"/></returns>
    Module GetModule(string moduleCode);

    /// <summary>
    /// Retrieves the collection of a semester
    /// </summary>
    /// <returns>A collection of the modules in a semester</returns>
    ICollection<Module> GetModules();
}
