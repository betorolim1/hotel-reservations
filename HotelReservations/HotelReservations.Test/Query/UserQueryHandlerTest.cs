using AutoFixture;
using HotelReservations.Model.Models;
using HotelReservations.Query.Handlers;
using HotelReservations.Query.Reservations.Dao;
using HotelReservations.Query.Reservations.Query;
using HotelReservations.Query.Users.Dao;
using HotelReservations.Test.Extensions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservations.Test.Query
{
    public class UserQueryHandlerTest
    {
        private Mock<IUserDao> _userDao = new Mock<IUserDao>();
        private Mock<IReservationDao> _reservationDao = new Mock<IReservationDao>();

        private UserQueryHandler _handler;

        Fixture _fixture = new Fixture().SetBehaviors();

        public UserQueryHandlerTest()
        {
            _handler = new UserQueryHandler(_userDao.Object, _reservationDao.Object); ;
        }

        // GetUserReservationsAsync

        [Fact]
        public async Task GetUserReservationsAsync_Should_Notify_When_User_Does_Not_Exist()
        {
            var userId = _fixture.Create<Guid>();

            _userDao.Setup(x => x.DoesUserExistAsync(userId)).ReturnsAsync(false);

            var result = await _handler.GetUserReservationsAsync(userId);

            Assert.Null(result);
            Assert.False(_handler.IsValid);
            Assert.Contains(_handler.Notifications, x => x == "User does not exist");

            VerifyMocks();
        }

        [Fact]
        public async Task GetUserReservationsAsync_Should_Return_User_Reservation_List()
        {
            var userId = _fixture.Create<Guid>();

            var resultListDb = _fixture.CreateMany<Reservation>(10).ToList();

            _userDao.Setup(x => x.DoesUserExistAsync(userId)).ReturnsAsync(true);

            _reservationDao.Setup(x => x.GetUserReservationsByUserIdAsync(userId)).ReturnsAsync(resultListDb);

            var result = await _handler.GetUserReservationsAsync(userId);

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);

            Assert.Equal(10, result.Count);
            Assert.Equal(resultListDb.First().StartDate, result.First().StartDate);
            Assert.Equal(resultListDb.First().EndDate, result.First().EndDate);
            Assert.Equal(resultListDb.First().Observation, result.First().Observation);
            Assert.Equal(resultListDb.First().Id, result.First().ReservationId);
            Assert.Equal(resultListDb.First().CreatedAt, result.First().CreatedAt);
            Assert.Equal(resultListDb.First().UpdateAt, result.First().UpdateAt);

            VerifyMocks();
        }

        // GetUsersAsync

        [Fact]
        public async Task GetUsersAsync_Should_Return_All_Users()
        {
            var userId = _fixture.Create<Guid>();

            var resultListDb = _fixture.CreateMany<User>(10).ToList();

            _userDao.Setup(x => x.GetUsersAsync()).ReturnsAsync(resultListDb);

            var result = await _handler.GetUsersAsync();

            Assert.NotNull(result);
            Assert.True(_handler.IsValid);

            Assert.Equal(10, result.Count);
            Assert.Equal(resultListDb.First().Id, result.First().Id);
            Assert.Equal(resultListDb.First().Name, result.First().Name);

            VerifyMocks();
        }

        private void VerifyMocks()
        {
            _userDao.VerifyAll();
            _userDao.VerifyNoOtherCalls();
            
            _reservationDao.VerifyAll();
            _reservationDao.VerifyNoOtherCalls();
        }
    }
}
