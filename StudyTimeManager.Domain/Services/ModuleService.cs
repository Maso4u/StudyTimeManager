using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services.Contracts;

namespace StudyTimeManager.Domain.Services;
///<inheritdoc cref="IModuleService"/>
public class ModuleService : IModuleService
{
    private readonly Semester _semester;

    public ModuleService(Semester semester)
    {
        _semester = semester;
    }

    public bool CreateModule(Module module)
    {
        Module foundModule = null;

        ///check if there are any modules in a module.
        ///if there are check if there is one with the same code 
        ///as the module in the parameter and assign it to the foundModule variable 
        if (_semester.Modules.Count > 0)
        {
            foundModule = _semester[module.Code];
        }

        ///if the foundModule is not null anymore then
        ///that means the module already exists and therefore this method should return false
        if (foundModule != null)
        {
            return false;
        }

        ///assign calculated requred weekly self-study hours and 
        ///assign to property of the module to be created
        module.RequiredWeeklySelfStudyHours = CalculateRequiredWeeklySelfStudyHours(module);
        
        ///Add module to collection of modules in semester and 
        ///check if it was successfully added with the contains method,
        ///return the result of that check
        _semester.Modules.Add(module);
        return _semester.Modules.Contains(module);
    }

    public bool DeleteModule(string moduleCode)
    {
        ///find module with the code given through parameter in collection in semester and 
        ///pass it into the Remove method of the semester's module collection as an argument.
        return _semester.Modules.Remove(_semester[moduleCode]);
    }

    public Module GetModule(string moduleCode) => _semester[moduleCode];

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
    private int CalculateRequiredWeeklySelfStudyHours(Module module) =>
        ((module.NumberOfCredits * 10) / _semester.NumberOfWeeks) - module.ClassHoursPerWeek;
}
