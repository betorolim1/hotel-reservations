using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Handlers
{
    public class UserHandler : IUserHandler
    {
        public Task CancelReservationAsync(CancelReservationCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateReservationAsync(CreateReservationCommand command)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReservationAsync(UpdateReservationCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
