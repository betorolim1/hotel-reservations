using HotelReservations.Core.Commands;
using HotelReservations.Shared.Validator.Interfaces;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Handlers.Interfaces
{
    public interface IUserHandler : INotifiable
    {
        Task UpdateReservationAsync(UpdateReservationCommand command);
        Task<Guid> CreateReservationAsync(CreateReservationCommand command);
        Task CancelReservationAsync(CancelReservationCommand command);
    }
}
