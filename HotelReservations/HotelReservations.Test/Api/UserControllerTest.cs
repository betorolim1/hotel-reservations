using AutoFixture;
using HotelReservations.Api.Controllers;
using HotelReservations.Core.Commands;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Result;
using HotelReservations.Query.Users.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservations.Test.Api
{
    public class UserControllerTest
    {
        private Mock<IUserQueryHandler> _userQueryHandler = new Mock<IUserQueryHandler>();
        private Mock<IUserHandler> _userHandler = new Mock<IUserHandler>();

        private UserController _controller;

        Fixture _fixture = new Fixture();

        public UserControllerTest()
        {
            _controller = new UserController(_userQueryHandler.Object, _userHandler.Object);
        }

        // GetUsersAsync

        [Fact]
        public async Task GetUsersAsync_Should_Return_OkResult()
        {
            var userResult = _fixture.CreateMany<UserResult>().ToList();

            _userQueryHandler.Setup(x => x.GetUsersAsync()).ReturnsAsync(userResult);

            var result = await _controller.GetUsersAsync() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(userResult, result.Value);

            VerifyMocks();
        }

        // GetUserReservationsAsync

        [Fact]
        public async Task GetUserReservationsAsync_Should_Return_BadRequest_When_Handler_Contains_Notifications()
        {
            var userId = _fixture.Create<Guid>();

            var notifications = _fixture.CreateMany<string>().ToList();

            _userQueryHandler.Setup(x => x.GetUserReservationsAsync(userId));

            _userQueryHandler.Setup(x => x.IsValid).Returns(false);
            _userQueryHandler.Setup(x => x.Notifications).Returns(notifications);

            var result = await _controller.GetUserReservationsAsync(userId) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notifications, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task GetUserReservationsAsync_Should_Return_OkResult()
        {
            var userId = _fixture.Create<Guid>();

            var reservationResult = _fixture.CreateMany<UserReservationsResult>().ToList();

            _userQueryHandler.Setup(x => x.GetUserReservationsAsync(userId)).ReturnsAsync(reservationResult);

            _userQueryHandler.Setup(x => x.IsValid).Returns(true);

            var result = await _controller.GetUserReservationsAsync(userId) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(reservationResult, result.Value);

            VerifyMocks();
        }

        // UpdateReservationAsync

        [Fact]
        public async Task UpdateReservationAsync_Should_Return_BadRequest_When_Command_Is_Null()
        {
            var userId = _fixture.Create<Guid>();
            var reservationId = _fixture.Create<Guid>();

            var result = await _controller.UpdateReservationAsync(userId, reservationId, null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Command can not be null", result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Return_BadRequest_When_Handler_Contains_Notifications()
        {
            var userId = _fixture.Create<Guid>();
            var reservationId = _fixture.Create<Guid>();
            var updateReservationCommand = _fixture.Create<UpdateReservationCommand>();

            var notifications = _fixture.CreateMany<string>().ToList();

            _userHandler.Setup(x => x.UpdateReservationAsync(updateReservationCommand));

            _userHandler.Setup(x => x.IsValid).Returns(false);
            _userHandler.Setup(x => x.Notifications).Returns(notifications);

            var result = await _controller.UpdateReservationAsync(userId, reservationId, updateReservationCommand) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notifications, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task UpdateReservationAsync_Should_Return_OkResult()
        {
            var userId = _fixture.Create<Guid>();
            var reservationId = _fixture.Create<Guid>();
            var updateReservationCommand = _fixture.Create<UpdateReservationCommand>();

            _userHandler.Setup(x => x.UpdateReservationAsync(updateReservationCommand));

            _userHandler.Setup(x => x.IsValid).Returns(true);

            var result = await _controller.UpdateReservationAsync(userId, reservationId, updateReservationCommand) as NoContentResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        // CreateReservationAsync

        [Fact]
        public async Task CreateReservationAsync_Should_Return_BadRequest_When_Command_Is_Null()
        {
            var userId = _fixture.Create<Guid>();

            var result = await _controller.CreateReservationAsync(userId, null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Command can not be null", result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task CreateReservationAsync_Should_Return_BadRequest_When_Handler_Contains_Notifications()
        {
            var userId = _fixture.Create<Guid>();
            var createReservationCommand = _fixture.Create<CreateReservationCommand>();

            var notifications = _fixture.CreateMany<string>().ToList();

            _userHandler.Setup(x => x.CreateReservationAsync(createReservationCommand));

            _userHandler.Setup(x => x.IsValid).Returns(false);
            _userHandler.Setup(x => x.Notifications).Returns(notifications);

            var result = await _controller.CreateReservationAsync(userId, createReservationCommand) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notifications, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task CreateReservationAsync_Should_Return_OkResult()
        {
            var userId = _fixture.Create<Guid>();
            var createReservationCommand = _fixture.Create<CreateReservationCommand>();

            _userHandler.Setup(x => x.CreateReservationAsync(createReservationCommand));

            _userHandler.Setup(x => x.IsValid).Returns(true);

            var result = await _controller.CreateReservationAsync(userId, createReservationCommand) as OkObjectResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        // CancelReservationAsync

        [Fact]
        public async Task CancelReservationAsync_Should_Return_BadRequest_When_Handler_Contains_Notifications()
        {
            var userId = _fixture.Create<Guid>();
            var reservationId = _fixture.Create<Guid>();

            var notifications = _fixture.CreateMany<string>().ToList();

            _userHandler.Setup(x => x.CancelReservationAsync(It.Is< CancelReservationCommand>(x => 
                x.ReservationId == reservationId &&
                x.UserId == userId
            )));

            _userHandler.Setup(x => x.IsValid).Returns(false);
            _userHandler.Setup(x => x.Notifications).Returns(notifications);

            var result = await _controller.CancelReservationAsync(userId, reservationId) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notifications, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task CancelReservationAsync_Should_Return_OkResult()
        {
            var userId = _fixture.Create<Guid>();
            var reservationId = _fixture.Create<Guid>();

            _userHandler.Setup(x => x.CancelReservationAsync(It.Is<CancelReservationCommand>(x =>
               x.ReservationId == reservationId &&
               x.UserId == userId
            )));

            _userHandler.Setup(x => x.IsValid).Returns(true);

            var result = await _controller.CancelReservationAsync(userId, reservationId) as NoContentResult;

            Assert.NotNull(result);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _userQueryHandler.VerifyAll();
            _userQueryHandler.VerifyNoOtherCalls();

            _userHandler.VerifyAll();
            _userHandler.VerifyNoOtherCalls();
        }
    }
}
