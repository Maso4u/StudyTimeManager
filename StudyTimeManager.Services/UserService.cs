using AutoMapper;
using Shared.DTOs.User;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;

namespace StudyTimeManager.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserDTO? CreateUser(UserForRegisterationDTO user)
        {
            User userEntity = _mapper.Map<User>(user);
            userEntity.PasswordHash = HashPassword(user.Password);
            _repository.User.CreateUser(userEntity);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public bool DeleteUser(Guid userId)
        {
            User user = _repository.User.GetUser(userId);
            if (user is null)
            {
                return false;
            }

            try
            {
                _repository.User.DeleteUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public UserDTO? GetUser(string username, string password)
        {
            User? user = _repository.User.GetUser(username);
            if (user is null)
            {
                return null;
            }



            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO? UpdateUser(UserForRegisterationDTO user)
        {
            throw new System.NotImplementedException();
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
