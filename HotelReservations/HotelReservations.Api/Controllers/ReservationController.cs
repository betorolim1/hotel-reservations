using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Api.Controllers
{
    [Route("reservations")]
    public class ReservationController : Controller
    {
        private IReservationQueryHandler _reservationQueryHandler;

        public ReservationController(IReservationQueryHandler reservationQueryHandler)
        {
            _reservationQueryHandler = reservationQueryHandler;
        }

        /// <summary>
        /// Retrieves all reservations in the informated period.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("in-period")]
        public async Task<IActionResult> GetReservationsInPeriodAsync([FromQuery] DateTime startSearchDate, [FromQuery] DateTime endSearchDate)
        {
            var query = new CheckFreePeriodQuery { EndSearchDate = endSearchDate, StartSearchDate = startSearchDate };
            var result = await _reservationQueryHandler.GetReservationsInPeriodAsync(query);

            if (!_reservationQueryHandler.IsValid)
                return BadRequest(_reservationQueryHandler.Notifications);

            return Ok(result);
        }
    }
}
