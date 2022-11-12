using HotelReservations.Core.Commands;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task UpdateReservationAsync(UpdateReservationCommand command);
        Task<Guid> CreateReservationAsync(CreateReservationCommand command);
        Task CancelReservationAsync(CancelReservationCommand command);
    }
}
