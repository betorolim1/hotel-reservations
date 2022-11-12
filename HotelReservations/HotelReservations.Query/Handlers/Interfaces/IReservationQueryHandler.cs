using HotelReservations.Query.Reservation.Query;
using HotelReservations.Query.Reservation.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers.Interfaces
{
    public interface IReservationQueryHandler
    {
        Task<List<FreePeriodResult>> CheckFreePeriodAsync(CheckFreePeriodQuery query);
    }
}
