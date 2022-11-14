using HotelReservations.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Users.Dao
{
    public interface IUserDao
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUsersByIdAsync(Guid id);
    }
}
