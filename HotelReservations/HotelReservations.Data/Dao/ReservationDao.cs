using HotelReservations.Data.Context;
using HotelReservations.Model.Models;
using HotelReservations.Query.Reservations.Dao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Data.Dao
{
    public class ReservationDao : IReservationDao
    {
        private readonly DatabaseContext _databaseContext;

        public ReservationDao(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Reservation>> GetUserReservationsByUserIdAsync(Guid userId)
        {
            return await _databaseContext.Reservations.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
