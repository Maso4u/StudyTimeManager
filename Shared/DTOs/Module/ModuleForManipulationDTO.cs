namespace Shared.DTOs.Module;
public abstract record ModuleForManipulationDTO
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public int NumberOfCredits { get; init; }
    public int ClassHoursPerWeek { get; init; }
}
