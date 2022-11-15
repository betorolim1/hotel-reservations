using HotelReservations.Query.Reservations.Query;
using HotelReservations.Query.Reservations.Result;
using HotelReservations.Shared.Validator.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers.Interfaces
{
    public interface IReservationQueryHandler : INotifiable
    {
        Task<List<ReservationInPeriodResult>> GetReservationsInPeriodAsync(CheckFreePeriodQuery query);
    }
}
