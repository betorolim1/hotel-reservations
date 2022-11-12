using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservation.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        public Task<List<FreePeriodResult>> GetUserReservationsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
