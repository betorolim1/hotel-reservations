using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservation.Query;
using HotelReservations.Query.Reservation.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers
{
    public class ReservationQueryHandler : IReservationQueryHandler
    {
        public Task<List<FreePeriodResult>> CheckFreePeriodAsync(CheckFreePeriodQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
