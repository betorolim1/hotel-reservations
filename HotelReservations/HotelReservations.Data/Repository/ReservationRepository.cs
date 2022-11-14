using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Core.Reservations.Repository;
using HotelReservations.Data.Context;
using HotelReservations.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ReservationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Guid> CreateReservationAsync(ReservationDomain reservation, User user)
        {
            var reservationId = Guid.NewGuid();

            var reservationToInsert = new Reservation
            {
                CreatedAt = DateTime.Now,
                EndDate = reservation.EndDate.Date,
                Observation = reservation.Observation,
                StartDate = reservation.StartDate.Date,
                Id = reservationId,
                UserId = user.Id
            };

            await _databaseContext.AddAsync(reservationToInsert);
            await _databaseContext.SaveChangesAsync();

            return reservationId;
        }

        public async Task<bool> IsFreePeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _databaseContext.Reservations.Where(x => x.StartDate.Date <= endDate.Date && startDate.Date <= x.EndDate.Date ).FirstOrDefaultAsync() == null;
        }
    }
}
