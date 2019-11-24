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
    [OpenApiTag("Reservation", Description = "API for Reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync().ConfigureAwait(false);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsForUserAsync(id).ConfigureAwait(false);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var reservations = await _reservationService.GetById(id).ConfigureAwait(false);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByConferenceRoomId(Guid id)
        {
            try
            {
                var reservations = await _reservationService.GetByConferenceRoomId(id).ConfigureAwait(false);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReservationDto dto)
        {
            try
            {
                var result = await _reservationService.AddReservationAsync(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReservationDto dto)
        {
            try
            {
                var result = await _reservationService.UpdateReservationAsync(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ReservationDto dto)
        {
            try
            {
                var result = await _reservationService.DeleteReservationAsync(dto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}