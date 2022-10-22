namespace Shared.DTOs.User
{
    public abstract record UserForManipulationDTO
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
