using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Shared.Validator;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Handlers
{
    public class UserHandler : Notifiable, IUserHandler
    {
        public Task CancelReservationAsync(CancelReservationCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateReservationAsync(CreateReservationCommand command)
        {
            var reservation = GetReservationByCommand(command);

            AddNotifications(reservation.Notifications);

            if (!IsValid)
                return Guid.Empty;


        }

        public Task UpdateReservationAsync(UpdateReservationCommand command)
        {
            throw new NotImplementedException();
        }

        private Reservation GetReservationByCommand(CreateReservationCommand command)
            => Reservation.Factory.CreateReservationToInsert(command.UserId, command.StartDate, command.EndDate, command.Observation);
    }
}
