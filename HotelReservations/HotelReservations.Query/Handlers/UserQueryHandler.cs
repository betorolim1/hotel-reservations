using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Result;
using HotelReservations.Query.Users.Dao;
using HotelReservations.Query.Users.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private IUserDao _userDao;

        public UserQueryHandler(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public Task<List<FreePeriodResult>> GetUserReservationsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserResult>> GetUsersAsync()
        {
            var usersDb = await _userDao.GetUsersAsync();

            var userResult = usersDb.Select(x => new UserResult
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return userResult;
        }
    }
}
