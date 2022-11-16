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

        /// <summary>
        /// Retrieves all users on database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _userQueryHandler.GetUsersAsync();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all the user reservations.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/reservations")]
        public async Task<IActionResult> GetUserReservationsAsync(Guid userId)
        {
            var result = await _userQueryHandler.GetUserReservationsAsync(userId);

            if (!_userQueryHandler.IsValid)
                return BadRequest(_userQueryHandler.Notifications);

            return Ok(result);
        }

        /// <summary>
        /// Updates the user reservation.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reservationId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{userId}/reservations/{reservationId}")]
        public async Task<IActionResult> UpdateReservationAsync(Guid userId, Guid reservationId, [FromBody] UpdateReservationCommand command)
        {
            if (command is null)
                return BadRequest("Command can not be null");

            command.ReservationId = reservationId;
            command.UserId = userId;

            await _userHandler.UpdateReservationAsync(command);

            if (!_userHandler.IsValid)
                return BadRequest(_userHandler.Notifications);

            return NoContent();
        }

        /// <summary>
        /// Creates an user reservation.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{userId}/reservations")]
        public async Task<IActionResult> CreateReservationAsync(Guid userId, [FromBody] CreateReservationCommand command)
        {
            if (command is null)
                return BadRequest("Command can not be null");

            command.UserId = userId;

            var result = await _userHandler.CreateReservationAsync(command);

            if (!_userHandler.IsValid)
                return BadRequest(_userHandler.Notifications);

            return Ok(result);
        }

        /// <summary>
        /// Cancels the user reservation.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/reservations/{reservationId}")]
        public async Task<IActionResult> CancelReservationAsync(Guid userId, Guid reservationId)
        {
            var command = new CancelReservationCommand
            {
                ReservationId = reservationId,
                UserId = userId
            };

            await _userHandler.CancelReservationAsync(command);

            if (!_userHandler.IsValid)
                return BadRequest(_userHandler.Notifications);

            return NoContent();
        }
    }
}
