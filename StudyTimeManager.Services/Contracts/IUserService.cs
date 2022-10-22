using Shared.DTOs.User;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StudyTimeManager.Services.Contracts
{
    public interface IUserService
    {
        UserDTO? CreateUser(UserForRegisterationDTO user);
        UserDTO? UpdateUser(UserForRegisterationDTO user);   
        bool DeleteUser(Guid user);
        UserDTO? GetUser(string username, string password);
    }
}
