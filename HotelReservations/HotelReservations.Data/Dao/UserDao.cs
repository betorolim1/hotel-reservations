using HotelReservations.Data.Context;
using HotelReservations.Model.Models;
using HotelReservations.Query.User.Dao;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Data.Dao
{
    public class UserDao : IUserDao
    {
        private readonly DatabaseContext _databaseContext;

        public UserDao(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _databaseContext.Users.ToListAsync();
        }
    }
}
