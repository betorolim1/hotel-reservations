using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Model.Models;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Reservations.Repository
{
    public interface IReservationRepository
    {
        Task<bool> IsFreePeriodAsync(DateTime startDate, DateTime endDate);
        Task<Guid> CreateReservationAsync(ReservationDomain reservation, User user);
    }
}
