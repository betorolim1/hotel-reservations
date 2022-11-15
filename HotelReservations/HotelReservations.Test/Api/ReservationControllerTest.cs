using AutoFixture;
using HotelReservations.Api.Controllers;
using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Query;
using HotelReservations.Query.Reservations.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservations.Test.Api
{
    public class ReservationControllerTest
    {
        private Mock<IReservationQueryHandler> _reservationQueryHandler = new Mock<IReservationQueryHandler>();

        private ReservationController _controller;

        Fixture _fixture = new Fixture();

        public ReservationControllerTest()
        {
            _controller = new ReservationController(_reservationQueryHandler.Object);
        }

        [Fact]
        public async Task GetReservationsInPeriodAsync_Should_Return_BadRequest_When_Handler_Contains_Notifications()
        {
            var query = _fixture.Create<CheckFreePeriodQuery>();

            var notifications = _fixture.CreateMany<string>().ToList();

            _reservationQueryHandler.Setup(x => x.GetReservationsInPeriodAsync(query));

            _reservationQueryHandler.Setup(x => x.IsValid).Returns(false);
            _reservationQueryHandler.Setup(x => x.Notifications).Returns(notifications);

            var result = await _controller.GetReservationsInPeriodAsync(query) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(notifications, result.Value);

            VerifyMocks();
        }

        [Fact]
        public async Task GetReservationsInPeriodAsync_Should_Return_OkResult()
        {
            var query = _fixture.Create<CheckFreePeriodQuery>();

            var reservationResult = _fixture.CreateMany<ReservationInPeriodResult>().ToList();

            _reservationQueryHandler.Setup(x => x.GetReservationsInPeriodAsync(query)).ReturnsAsync(reservationResult);

            _reservationQueryHandler.Setup(x => x.IsValid).Returns(true);

            var result = await _controller.GetReservationsInPeriodAsync(query) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(reservationResult, result.Value);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _reservationQueryHandler.VerifyAll();
            _reservationQueryHandler.VerifyNoOtherCalls();
        }
    }
}
