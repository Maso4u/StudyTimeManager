namespace Shared.DTOs.User
{
    public record UserForRegisterationDTO:UserForManipulationDTO
    {
        public string ConfirmPassword { get; init; }
    }
}
