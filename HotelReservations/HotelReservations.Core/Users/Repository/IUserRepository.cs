using HotelReservations.Model.Models;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Users.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
    }
}
