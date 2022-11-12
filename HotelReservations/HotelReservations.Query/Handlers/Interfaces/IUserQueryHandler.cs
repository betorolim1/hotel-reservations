using HotelReservations.Query.Reservation.Result;
using HotelReservations.Query.User.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers.Interfaces
{
    public interface IUserQueryHandler
    {
        Task<List<FreePeriodResult>> GetUserReservationsAsync(Guid userId);
        Task<List<UserResult>> GetUsersAsync();
    }
}
