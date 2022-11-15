using AutoFixture;
using HotelReservations.Model.Models;
using HotelReservations.Query.Handlers;
using HotelReservations.Query.Reservations.Dao;
using HotelReservations.Query.Reservations.Query;
using HotelReservations.Test.Extensions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservations.Test.Query
{
    public class ReservationQueryHandlerTest
    {
        private Mock<IReservationDao> _reservationDao = new Mock<IReservationDao>();

        private ReservationQueryHandler _handler;

        Fixture _fixture = new Fixture().SetBehaviors();

        public ReservationQueryHandlerTest()
        {
            _handler = new ReservationQueryHandler(_reservationDao.Object);
        }

        [Fact]
        public async Task GetReservationsInPeriodAsync_Should_Notify_When_StartDate_Is_Less_Than_Today()
        {
            var query = _fixture.Build<CheckFreePeriodQuery>()
                .With(x => x.StartSearchDate, DateTime.Now.AddDays(-1))
                .Create();

            var result = await _handler.GetReservationsInPeriodAsync(query);

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "Invalid StartDate");

            VerifyMocks();
        }

        [Fact]
        public async Task GetReservationsInPeriodAsync_Should_Notify_When_EndDate_Is_Less_Than_StartDate()
        {
            var query = _fixture.Build<CheckFreePeriodQuery>()
                .With(x => x.StartSearchDate, DateTime.Now.AddDays(2))
                .With(x => x.EndSearchDate, DateTime.Now.AddDays(1))
                .Create();

            var result = await _handler.GetReservationsInPeriodAsync(query);

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "Invalid dates");

            VerifyMocks();
        }

        [Fact]
        public async Task GetReservationsInPeriodAsync_Should_Return_ReservationList()
        {
            var query = _fixture.Build<CheckFreePeriodQuery>()
                .With(x => x.StartSearchDate, DateTime.Now.AddDays(1))
                .With(x => x.EndSearchDate, DateTime.Now.AddDays(2))
                .Create();
            
            var resultListDb = _fixture.CreateMany<Reservation>(10).ToList();

            _reservationDao.Setup(x => x.GetReservationsInPeriodAsync(query.StartSearchDate, query.EndSearchDate)).ReturnsAsync(resultListDb);

            var result = await _handler.GetReservationsInPeriodAsync(query);

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);

            Assert.Equal(10, result.Count);
            Assert.Equal(resultListDb.First().StartDate, result.First().StartDate);
            Assert.Equal(resultListDb.First().EndDate, result.First().EndDate);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _reservationDao.VerifyAll();
            _reservationDao.VerifyNoOtherCalls();
        }
    }
}
