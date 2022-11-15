using HotelReservations.Core.Users.Repository;
using HotelReservations.Data.Context;
using HotelReservations.Data.Repository.Base;
using HotelReservations.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await databaseContext.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}
