using Shared.DTOs.User;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.WPF.UI.State.Authenticators
{
    public interface IAuthenticator
    {
        UserDTO CurrentUser { get; }
        bool IsLoggedIn { get; }

        bool Login(UserForLoginDTO loginDTO);
        RegisterationResult Register(UserForRegisterationDTO registerationDTO);
    }
}