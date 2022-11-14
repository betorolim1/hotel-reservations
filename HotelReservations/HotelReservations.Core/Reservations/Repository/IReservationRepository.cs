using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Reservations.Repository
{
    public interface IReservationRepository
    {
        Task<bool> IsValidPeriodAsync(DateTime dateTime, DateTime endDate);
    }
}
