using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Query;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("in-period")]
        public async Task<IActionResult> GetReservationsInPeriodAsync([FromBody] CheckFreePeriodQuery query)
        {
            var result = await _reservationQueryHandler.GetReservationsInPeriodAsync(query);

            if (!_reservationQueryHandler.IsValid)
                return BadRequest(_reservationQueryHandler.Notifications);

            return Ok(result);
        }
    }
}
