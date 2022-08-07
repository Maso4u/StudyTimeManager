using StudyTimeManager.Domain.Models;
using System.Windows.Input;

namespace StudyTimeManager.WPF.UI.ViewModels;
public class ModuleListingItemViewModel
{
    private readonly Module _module;
    public string? ModuleCode => _module?.Code;
    public string? ModuleName => _module.Name;
    public int NumberOfCredits => _module.NumberOfCredits;
    public int ClassHoursPerWeek => _module.ClassHoursPerWeek;
    public ICommand DeleteModuleCommand { get; }
    public ModuleListingItemViewModel(Module module)
    {
        _module = module;
    }
}