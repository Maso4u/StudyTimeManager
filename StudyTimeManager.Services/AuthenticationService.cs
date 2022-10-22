using AutoMapper;
using Microsoft.AspNet.Identity;
using Shared.DTOs.User;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;

namespace StudyTimeManager.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher hasher;
        public AuthenticationService(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            hasher = passwordHasher;
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public UserDTO? Login(string username, string password)
        {
            User userFound = _repositoryManager.User.GetUser(username);
            if (userFound is null)
            {
                ///TODO - Throw custom exception
                return null;
            }
            PasswordVerificationResult passwordVerificationResult = hasher
                .VerifyHashedPassword(userFound.PasswordHash,password);

            if (passwordVerificationResult.Equals(PasswordVerificationResult.Failed))
            {
                ///TODO - Throw custom exception
                return null;
            }

            return _mapper.Map<UserDTO>(userFound);
        }

        public RegisterationResult Register(string username, string password, string confirmPassword)
        {

            if (!password.Equals(confirmPassword))
            {
                
                return RegisterationResult.PasswordsDoNotMatch;
            }
            User userFound = _repositoryManager.User.GetUser(username);

            if (userFound is not null)
            {
                return RegisterationResult.UsernameAlreadyExists;
            }

            User user = new User()
            {
                Username = username,
                PasswordHash = hasher.HashPassword(password)
            };
            _repositoryManager.User.CreateUser(user);


            return RegisterationResult.Success;
        }
    }
}