using HotelReservations.Query.Reservations.Result;
using HotelReservations.Query.Users.Result;
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
