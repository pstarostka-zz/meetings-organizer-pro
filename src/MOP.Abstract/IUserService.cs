using MOP.Abstract.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOP.Abstract
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> AuthenticateUser(string email, string password);
        Task<string> GenerateJwtToken(UserDto dto, string appSecret);
        Task<UserDto> RegisterUser(UserRegistrationDto dto);
    }
}
