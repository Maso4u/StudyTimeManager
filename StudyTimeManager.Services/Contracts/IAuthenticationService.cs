using Shared.DTOs.User;
using System.Threading.Tasks;

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
        Task<RegisterationResult> Register(string username, string password, string confirmPassword);
        Task<UserDTO?> Login(string username, string password);
    }
}
