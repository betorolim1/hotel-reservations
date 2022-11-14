using HotelReservations.Query.Reservations.Query;
using HotelReservations.Query.Reservations.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers.Interfaces
{
    public interface IReservationQueryHandler
    {
        Task<List<FreePeriodResult>> CheckFreePeriodAsync(CheckFreePeriodQuery query);
    }
}
