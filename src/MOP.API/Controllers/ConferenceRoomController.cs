using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOP.Abstract;
using MOP.Abstract.Dto;
using NSwag.Annotations;

namespace MOP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [OpenApiTag("Conference Room", Description = "API for Conference Rooms")]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IConferenceRoomService _conferenceRoomService;

        public ConferenceRoomController(IConferenceRoomService conferenceRoomService)
        {
            _conferenceRoomService = conferenceRoomService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ConferenceRoomDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _conferenceRoomService.GetAllConferenceRoomsAsync().ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}