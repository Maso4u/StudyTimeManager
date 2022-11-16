using AutoMapper;
using Shared.DTOs.User;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services.Contracts;
using System;
using System.Threading.Tasks;

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

        public async Task<UserDTO?> CreateUser(UserForRegisterationDTO user)
        {
            User userEntity = _mapper.Map<User>(user);
            userEntity.PasswordHash = HashPassword(user.Password);
            await _repository.User.CreateUser(userEntity);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            User? user = await _repository.User.GetUser(userId);
            if (user is null)
            {
                return false;
            }

            try
            {
                await _repository.User.DeleteUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<UserDTO?> GetUser(string username, string password)
        {
            User? user = await _repository.User.GetUser(username);
            if (user is null)
            {
                return null;
            }



            return _mapper.Map<UserDTO>(user);
        }

        public Task<UserDTO?> UpdateUser(UserForRegisterationDTO user)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
