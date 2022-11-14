using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Dao;
using HotelReservations.Query.Reservations.Result;
using HotelReservations.Query.Users.Dao;
using HotelReservations.Query.Users.Result;
using HotelReservations.Shared.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers
{
    public class UserQueryHandler : Notifiable, IUserQueryHandler
    {
        private IUserDao _userDao;
        private IReservationDao _reservationDao;

        public UserQueryHandler(IUserDao userDao, IReservationDao reservationDao)
        {
            _userDao = userDao;
            _reservationDao = reservationDao;
        }

        public async Task<List<UserReservationsResult>> GetUserReservationsAsync(Guid userId)
        {
            var user = await _userDao.GetUsersByIdAsync(userId);

            if (user is null)
            {
                AddNotification("User does not exist");
                return null;
            }

            var reservationList = await _reservationDao.GetUserReservationsByUserIdAsync(userId);

            var result = reservationList.Select(x => new UserReservationsResult
            {
                EndDate = x.EndDate,
                Observation = x.Observation,
                ReservationId = x.Id,
                StartDate = x.StartDate,
                CreatedAt = x.CreatedAt,
                UpdateAt = x.UpdateAt ?? null
            }).ToList();

            return result;
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
