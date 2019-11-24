using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOP.Abstract;
using MOP.Abstract.Dto;
using NSwag.Annotations;

namespace MOP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [OpenApiTag("User", Description = "API for Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authService;
        private readonly IOptions<AppSettings> _settings;

        public UserController(IUserService authService, IOptions<AppSettings> options)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService), "Parameter authService cannot be null");
            _settings = options ?? throw new ArgumentNullException(nameof(options), "Parameter options cannot be null");
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AuthorizeUser([FromBody] UserCredentials credentials)
        {
            try
            {
                var userDto = await _authService.AuthenticateUser(credentials?.Email, credentials?.Password).ConfigureAwait(false);
                var token = await _authService.GenerateJwtToken(userDto, _settings.Value.JwtSecret).ConfigureAwait(false);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _authService.GetAllUsers().ConfigureAwait(false);
            return Ok(users);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto dto)
        {
            var result = await _authService.RegisterUser(dto).ConfigureAwait(false);
            return Ok(result);
        }
    }
}