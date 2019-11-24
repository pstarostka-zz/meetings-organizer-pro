using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MOP.Abstract;
using MOP.Abstract.Dto;
using MOP.DAL.Model;
using MOP.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOP.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> GenerateJwtToken(UserDto dto, string appSecret)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(appSecret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
                        new Claim(ClaimTypes.Name, dto.Id.ToString() ?? ""),
                        new Claim("firstName", dto.FirstName),
                        new Claim("lastName", dto.LastName),
                        new Claim("email", dto.Email),
                        new Claim("phoneNo", dto.PhoneNo),
                        new Claim("title", dto.Title),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,  new DateTimeOffset(DateTime.UtcNow).ToUniversalTime()
                        .ToString(), ClaimValueTypes.Integer64)
                        }),
                        Expires = DateTime.UtcNow.AddDays(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
                catch (Exception ex)
                {
                    //TODO: add logging
                    throw;
                }
            }).ConfigureAwait(false);
        }

        public async Task<UserDto> AuthenticateUser(string email, string password)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetUserByEmail(email).ConfigureAwait(false);

                if (user == null || !ValidateCredentials(user, password))
                    throw new NullReferenceException("Wrong email or password");

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        private bool ValidateCredentials(User user, string password)
        {
            //TODO: encryption / decryption
            return user.Password == password;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            try
            {
                var user = await _unitOfWork.UserRepo.GetById(id).ConfigureAwait(false);

                if (user == null)
                    throw new NullReferenceException("User not found");

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                //TODO: add logger
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.UserRepo.GetAll().ConfigureAwait(false);

                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserDto> RegisterUser(UserRegistrationDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var x = await _unitOfWork.UserRepo.Add(user).ConfigureAwait(false);

            return null;
        }
    }
}
