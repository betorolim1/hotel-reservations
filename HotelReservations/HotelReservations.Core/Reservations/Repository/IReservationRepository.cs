using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Model.Models;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Reservations.Repository
{
    public interface IReservationRepository
    {
        Task<bool> IsFreePeriodAsync(DateTime startDate, DateTime endDate, Guid excludeReservationId = new Guid());
        Task<Guid> CreateReservationAsync(ReservationDomain reservation, User user);
        Task<Reservation> GetReservationByIdAsync(Guid id);
        Task UpdateReservationAsync(ReservationDomain reservationNew, Reservation reservationOld);
        Task CancelReservationAsync(Reservation reservation);
    }
}
