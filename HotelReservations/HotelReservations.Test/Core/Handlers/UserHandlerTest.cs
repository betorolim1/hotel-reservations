using AutoFixture;
using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers;
using HotelReservations.Core.Reservations.Domain;
using HotelReservations.Core.Reservations.Repository;
using HotelReservations.Core.Users.Repository;
using HotelReservations.Model.Models;
using HotelReservations.Test.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservations.Test.Core.Handlers
{
    public class UserHandlerTest
    {
        private Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private Mock<IReservationRepository> _reservationRepository = new Mock<IReservationRepository>();

        private Guid ReservationId = Guid.NewGuid();
        private Guid UserId = Guid.NewGuid();
        private DateTime StartDate = DateTime.Now.AddDays(1);
        private DateTime EndDate = DateTime.Now.AddDays(3);
        private string Observation;

        private UserHandler _handler;

        Fixture _fixture = new Fixture().SetBehaviors();

        public UserHandlerTest()
        {
            _handler = new UserHandler(_reservationRepository.Object, _userRepository.Object);

            Observation = _fixture.Create<string>();
        }

        // CancelReservationAsync

        [Fact]
        public async Task CancelReservationAsync_Should_Notify_When_Domain_Has_Notifications()
        {
            var command = new CancelReservationCommand { ReservationId = Guid.Empty, UserId = UserId };

            await _handler.CancelReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.NotEmpty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task CancelReservationAsync_Should_Notify_When_User_Does_Not_Exist()
        {
            var command = GetValidCancelReservationCommand();

            _userRepository.Setup(x => x.GetUserAsync(UserId));

            await _handler.CancelReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "User does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task CancelReservationAsync_Should_Notify_When_Reservation_Does_Not_Exist()
        {
            var command = GetValidCancelReservationCommand();

            var user = _fixture.Create<User>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.GetReservationByIdAsync(ReservationId));

            await _handler.CancelReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "Reservation does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task CancelReservationAsync_Should_Cancel_Reservation()
        {
            var command = GetValidCancelReservationCommand();

            var user = _fixture.Create<User>();

            var reservation = _fixture.Create<Reservation>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.GetReservationByIdAsync(ReservationId)).ReturnsAsync(reservation);

            _reservationRepository.Setup(x => x.CancelReservationAsync(reservation));

            await _handler.CancelReservationAsync(command);

            Assert.True(_handler.IsValid);

            VerifyMocks();
        }

        // CreateReservationAsync

        [Fact]
        public async Task CreateReservationAsync_Should_Notify_When_Domain_Has_Notifications()
        {
            var command = new CreateReservationCommand
            {
                UserId = Guid.Empty,
                EndDate = EndDate,
                Observation = Observation,
                StartDate = StartDate
            };

            await _handler.CreateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.NotEmpty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task CreateReservationAsync_Should_Notify_When_User_Does_Not_Exist()
        {
            var command = GetValidCreateReservationCommand();

            _userRepository.Setup(x => x.GetUserAsync(UserId));

            await _handler.CreateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "User does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task CreateReservationAsync_Should_Notify_When_There_is_Another_Reservation_In_Period()
        {
            var command = GetValidCreateReservationCommand();

            var user = _fixture.Create<User>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.IsFreePeriodAsync(command.StartDate, command.EndDate, Guid.Empty)).ReturnsAsync(false);

            await _handler.CreateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "There is another reservation in the informed period");

            VerifyMocks();
        }

        [Fact]
        public async Task CreateReservationAsync_Should_Cancel_Reservation()
        {
            var newReservationGuid = Guid.NewGuid();

            var command = GetValidCreateReservationCommand();

            var user = _fixture.Create<User>();

            var reservation = _fixture.Create<Reservation>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.IsFreePeriodAsync(command.StartDate, command.EndDate, Guid.Empty)).ReturnsAsync(true);

            _reservationRepository.Setup(x => x.CreateReservationAsync(It.Is<ReservationDomain>(x =>
                x.UserId == UserId &&
                x.EndDate == EndDate &&
                x.Observation == Observation &&
                x.StartDate == StartDate),
                user)).ReturnsAsync(newReservationGuid);

            var result = await _handler.CreateReservationAsync(command);

            Assert.True(_handler.IsValid);

            Assert.Equal(newReservationGuid, result);

            VerifyMocks();
        }

        // UpdateReservationAsync

        [Fact]
        public async Task UpdateReservationAsync_Should_Notify_When_Domain_Has_Notifications()
        {
            var command = new UpdateReservationCommand
            {
                ReservationId = Guid.Empty,
                UserId = UserId,
                EndDate = EndDate,
                Observation = Observation,
                StartDate = StartDate
            };

            await _handler.UpdateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.NotEmpty(_handler.Notifications);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Notify_When_User_Does_Not_Exist()
        {
            var command = GetValidUpdateReservationCommand();

            _userRepository.Setup(x => x.GetUserAsync(UserId));

            await _handler.UpdateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "User does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Notify_When_Reservation_Does_Not_Exist()
        {
            var command = GetValidUpdateReservationCommand();

            var user = _fixture.Create<User>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.GetReservationByIdAsync(ReservationId));

            await _handler.UpdateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "Reservation does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Notify_When_There_is_Another_Reservation_In_Period()
        {
            var command = GetValidUpdateReservationCommand();

            var user = _fixture.Create<User>();

            var reservation = _fixture.Create<Reservation>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.GetReservationByIdAsync(ReservationId)).ReturnsAsync(reservation);

            _reservationRepository.Setup(x => x.IsFreePeriodAsync(command.StartDate, command.EndDate, ReservationId)).ReturnsAsync(false);

            await _handler.UpdateReservationAsync(command);

            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "There is another reservation in the informed period");

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Cancel_Reservation()
        {
            var newReservationGuid = Guid.NewGuid();

            var command = GetValidUpdateReservationCommand();

            var user = _fixture.Create<User>();

            var reservation = _fixture.Create<Reservation>();

            _userRepository.Setup(x => x.GetUserAsync(UserId)).ReturnsAsync(user);

            _reservationRepository.Setup(x => x.GetReservationByIdAsync(ReservationId)).ReturnsAsync(reservation);

            _reservationRepository.Setup(x => x.IsFreePeriodAsync(command.StartDate, command.EndDate, ReservationId)).ReturnsAsync(true);

            _reservationRepository.Setup(x => x.UpdateReservationAsync(It.Is<ReservationDomain>(x =>
                x.Id == ReservationId &&
                x.UserId == UserId &&
                x.EndDate == EndDate &&
                x.Observation == Observation &&
                x.StartDate == StartDate),
                reservation));

            await _handler.UpdateReservationAsync(command);

            Assert.True(_handler.IsValid);

            VerifyMocks();
        }

        private CancelReservationCommand GetValidCancelReservationCommand()
            => new CancelReservationCommand { ReservationId = ReservationId, UserId = UserId };

        private CreateReservationCommand GetValidCreateReservationCommand()
            => new CreateReservationCommand
            {
                UserId = UserId,
                EndDate = EndDate,
                Observation = Observation,
                StartDate = StartDate
            };
        
        private UpdateReservationCommand GetValidUpdateReservationCommand()
            => new UpdateReservationCommand
            {
                ReservationId = ReservationId,
                UserId = UserId,
                EndDate = EndDate,
                Observation = Observation,
                StartDate = StartDate
            };

        private void VerifyMocks()
        {
            _userRepository.VerifyAll();
            _userRepository.VerifyNoOtherCalls();

            _reservationRepository.VerifyAll();
            _reservationRepository.VerifyNoOtherCalls();
        }
    }
}
