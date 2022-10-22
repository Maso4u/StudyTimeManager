using Shared.DTOs.User;

namespace StudyTimeManager.Services.Contracts
{
    public enum RegisterationResult
    {
        Success,
        PasswordsDoNotMatch,
        UsernameAlreadyExists
    }
    public interface IAuthenticationService
    {
        RegisterationResult Register(string username, string password, string confirmPassword);
        UserDTO? Login(string username, string password);
    }
}
