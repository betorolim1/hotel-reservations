using HotelReservations.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Reservations.Dao
{
    public interface IReservationDao
    {
        Task<List<Reservation>> GetUserReservationsByUserIdAsync(Guid userId);
        Task<List<Reservation>> GetReservationsInPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
