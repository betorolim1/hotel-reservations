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

        [HttpGet("free-period")]
        public async Task<IActionResult> CheckFreePeriodAsync([FromQuery] CheckFreePeriodQuery query)
        {
            var result = await _reservationQueryHandler.CheckFreePeriodAsync(query);

            return Ok(result);
        }
    }
}
