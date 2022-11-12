using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Query.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Api.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private IUserQueryHandler _userQueryHandler;
        private IUserHandler _userHandler;

        public UserController(IUserQueryHandler userQueryHandler, IUserHandler userHandler)
        {
            _userQueryHandler = userQueryHandler;
            _userHandler = userHandler;
        }

        [HttpGet("{userId}/reservations")]
        public async Task<IActionResult> GetUserReservationsAsync([FromQuery] Guid userId)
        {
            var result = await _userQueryHandler.GetUserReservationsAsync(userId);

            return Ok(result);
        }

        [HttpPut("{userId}/reservations/{reservationId}")]
        public async Task<IActionResult> UpdateReservationAsync([FromQuery] Guid userId, [FromQuery] Guid reservationId, [FromBody] UpdateReservationCommand command)
        {
            if (command is null)
                return BadRequest("Command can not be null");

            command.ReservationId = reservationId;
            command.UserId = userId;

            await _userHandler.UpdateReservationAsync(command);

            return NoContent();
        }

        [HttpPost("{userId}/reservations")]
        public async Task<IActionResult> CreateReservationAsync([FromQuery] Guid userId, [FromBody] CreateReservationCommand command)
        {
            if (command is null)
                return BadRequest("Command can not be null");

            command.UserId = userId;

            var result = await _userHandler.CreateReservationAsync(command);

            return Ok(result);
        }

        [HttpDelete("{userId}/reservations/{reservationId}")]
        public async Task<IActionResult> CancelReservationAsync([FromQuery] Guid userId, [FromQuery] Guid reservationId)
        {
            var command = new CancelReservationCommand
            {
                ReservationId = reservationId,
                UserId = userId
            };

            await _userHandler.CancelReservationAsync(command);

            return Ok();
        }
    }
}
