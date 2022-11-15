using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Core.Reservations.Repository;
using HotelReservations.Core.Users.Repository;
using HotelReservations.Model.Models;
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

            var user = await GetAndValidateUserAsync(reservation.UserId);

            if (!IsValid)
                return Guid.Empty;

            await ValidateFreePeriodAsync(reservation);

            if (!IsValid)
                return Guid.Empty;

            var reservationId = await _reservationRepository.CreateReservationAsync(reservation, user);

            return reservationId;
        }

        public async Task UpdateReservationAsync(UpdateReservationCommand command)
        {
            var reservationNew = GetReservationByCommand(command);

            AddNotifications(reservationNew.Notifications);

            if (!IsValid)
                return;

            await GetAndValidateUserAsync(reservationNew.UserId);

            if (!IsValid)
                return;

            var reservationOld = await _reservationRepository.GetReservationByIdAsync(reservationNew.Id);

            if (reservationOld is null)
            {
                AddNotification("Reservation does not exist");
                return;
            }

            await ValidateFreePeriodAsync(reservationNew);

            if (!IsValid)
                return;

            await _reservationRepository.UpdateReservationAsync(reservationNew, reservationOld);
        }

        private async Task<User> GetAndValidateUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            if (user is null)
            {
                AddNotification("User does not exist");
                return null;
            }

            return user;
        }

        private async Task ValidateFreePeriodAsync(ReservationDomain reservation)
        {
            var isFreePeriod = await _reservationRepository.IsFreePeriodAsync(reservation.StartDate, reservation.EndDate, reservation.Id);

            if (!isFreePeriod)
                AddNotification("There is another reservation in the informed period");
        }

        private ReservationDomain GetReservationByCommand(CreateReservationCommand command)
            => ReservationDomain.Factory.CreateReservationToInsert(command.UserId, command.StartDate, command.EndDate, command.Observation);

        private ReservationDomain GetReservationByCommand(UpdateReservationCommand command)
            => ReservationDomain.Factory.CreateReservationToUpdate(command.ReservationId, command.UserId, command.StartDate, command.EndDate, command.Observation);
    }
}
