using HotelReservations.Query.Reservations.Result;
using HotelReservations.Query.Users.Result;
using HotelReservations.Shared.Validator.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers.Interfaces
{
    public interface IUserQueryHandler : INotifiable
    {
        Task<List<UserReservationsResult>> GetUserReservationsAsync(Guid userId);
        Task<List<UserResult>> GetUsersAsync();
    }
}
