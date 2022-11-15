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

        public async Task CancelReservationAsync(Reservation reservation)
        {
            _databaseContext.Remove(reservation);
            await _databaseContext.SaveChangesAsync();
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

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _databaseContext.Reservations.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> IsFreePeriodAsync(DateTime startDate, DateTime endDate, Guid excludeReservationId = new Guid())
        {
            var query = _databaseContext.Reservations.Where(x => x.StartDate.Date <= endDate.Date && startDate.Date <= x.EndDate.Date);

            if (excludeReservationId != Guid.Empty)
                query = query.Where(x => !x.Id.Equals(excludeReservationId));

            var teste =  await query.FirstOrDefaultAsync();

            return teste == null;
        }

        public async Task UpdateReservationAsync(ReservationDomain reservationNew, Reservation reservationOld)
        {
            reservationOld.Observation = reservationNew.Observation;
            reservationOld.StartDate = reservationNew.StartDate;
            reservationOld.EndDate = reservationNew.EndDate;
            reservationOld.UpdateAt = DateTime.Now;

            _databaseContext.Update(reservationOld);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
