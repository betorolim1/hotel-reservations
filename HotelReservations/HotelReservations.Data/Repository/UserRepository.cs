using HotelReservations.Core.Users.Repository;
using HotelReservations.Data.Context;
using HotelReservations.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _databaseContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}
