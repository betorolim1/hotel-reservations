using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Core.Reservations.Repository;
using HotelReservations.Core.Users.Repository;
using HotelReservations.Shared.Validator;
using System;
using System.Threading.Tasks;

namespace HotelReservations.Core.Handlers
{
    public class UserHandler : Notifiable, IUserHandler
    {
        private IReservationRepository _reservationRepository;
        private IUserRepository _userRepository;

        public UserHandler(IReservationRepository reservationRepository, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }

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

            var user = await _userRepository.GetUserAsync(reservation.UserId);

            if(user is null)
            {
                AddNotification("User does not exist");
                return Guid.Empty;
            }

            var isFreePeriod = await _reservationRepository.IsFreePeriodAsync(reservation.StartDate, reservation.EndDate);

            if (!isFreePeriod)
            {
                AddNotification("There is another reservation in the informed period");
                return Guid.Empty;
            }

            var reservationId = await _reservationRepository.CreateReservationAsync(reservation, user);

            return reservationId;
        }

        public Task UpdateReservationAsync(UpdateReservationCommand command)
        {
            throw new NotImplementedException();
        }

        private ReservationDomain GetReservationByCommand(CreateReservationCommand command)
            => ReservationDomain.Factory.CreateReservationToInsert(command.UserId, command.StartDate, command.EndDate, command.Observation);
    }
}
