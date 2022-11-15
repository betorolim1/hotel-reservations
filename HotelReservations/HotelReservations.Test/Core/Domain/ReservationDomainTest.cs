using AutoFixture;
using HotelReservations.Core.Reservations.Domain;
using System;
using Xunit;

namespace HotelReservations.Test.Core.Domain
{
    public class ReservationDomainTest
    {
        private Guid ValidReservationId = Guid.NewGuid();
        private Guid ValidUserId = Guid.NewGuid();
        private DateTime ValidStartDate = DateTime.Now.AddDays(30);
        private DateTime ValidEndDate = DateTime.Now.AddDays(33);
        private string ValidObservation = string.Join(string.Empty, new Fixture().CreateMany<char>(255)); // String of 255 caracters

        // CreateReservationToInsert

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_UserId_Is_Empty_Guid()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(Guid.Empty, ValidStartDate, ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid UserId");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_StartDate_Is_Less_Than_Today()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, DateTime.Now.AddDays(-1), ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid StartDate");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_StartDate_Is_Equals_Today()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, DateTime.Now, ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can't start today");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_EndDate_Is_Lass_Then_StartDate()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid dates");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_Period_Is_Bigger_Than_3_Days()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, ValidStartDate, ValidStartDate.AddDays(4), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can’t be longer than 3 days");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_StartDate_Is_More_Than_30_Days_To_Start()
        {
            var startDate = DateTime.Now.AddDays(31);
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, startDate, startDate.AddDays(3), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can’t be reserved more than 30 days in advance");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Notify_When_Observation_Has_More_Than_255_Length()
        {
            var observation = string.Join(string.Empty, new Fixture().CreateMany<char>(256));

            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, ValidStartDate, ValidEndDate, observation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Observation max length is 255");
        }

        [Fact]
        public void CreateReservationToInsert_Should_Return_ReservationDomain()
        {
            var result = ReservationDomain.Factory.CreateReservationToInsert(ValidUserId, ValidStartDate, ValidEndDate, ValidObservation);

            Assert.True(result.IsValid);

            Assert.Equal(Guid.Empty, result.Id);
            Assert.Equal(ValidUserId, result.UserId);
            Assert.Equal(ValidStartDate, result.StartDate);
            Assert.Equal(ValidEndDate, result.EndDate);
            Assert.Equal(ValidObservation, result.Observation);
        }

        // CreateReservationToUpdate

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_ReservationId_Is_Empty_Guid()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(Guid.Empty, ValidUserId, ValidStartDate, ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid ReservationId");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_UserId_Is_Empty_Guid()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, Guid.Empty, ValidStartDate, ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid UserId");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_StartDate_Is_Less_Than_Today()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, DateTime.Now.AddDays(-1), ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid StartDate");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_StartDate_Is_Equals_Today()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, DateTime.Now, ValidEndDate, ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can't start today");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_EndDate_Is_Lass_Then_StartDate()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid dates");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_Period_Is_Bigger_Than_3_Days()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, ValidStartDate, ValidStartDate.AddDays(4), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can’t be longer than 3 days");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_StartDate_Is_More_Than_30_Days_To_Start()
        {
            var startDate = DateTime.Now.AddDays(31);
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, startDate, startDate.AddDays(3), ValidObservation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Stay can’t be reserved more than 30 days in advance");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Notify_When_Observation_Has_More_Than_255_Length()
        {
            var observation = string.Join(string.Empty, new Fixture().CreateMany<char>(256));

            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, ValidStartDate, ValidEndDate, observation);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Observation max length is 255");
        }

        [Fact]
        public void CreateReservationToUpdate_Should_Return_ReservationDomain()
        {
            var result = ReservationDomain.Factory.CreateReservationToUpdate(ValidReservationId, ValidUserId, ValidStartDate, ValidEndDate, ValidObservation);

            Assert.True(result.IsValid);

            Assert.Equal(ValidReservationId, result.Id);
            Assert.Equal(ValidUserId, result.UserId);
            Assert.Equal(ValidStartDate, result.StartDate);
            Assert.Equal(ValidEndDate, result.EndDate);
            Assert.Equal(ValidObservation, result.Observation);
        }

        // CreateReservationToDelete

        [Fact]
        public void CreateReservationToDelete_Should_Notify_When_ReservationId_Is_Empty_Guid()
        {
            var result = ReservationDomain.Factory.CreateReservationToDelete(Guid.Empty, ValidUserId);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid ReservationId");
        }

        [Fact]
        public void CreateReservationToDelete_Should_Notify_When_UserId_Is_Empty_Guid()
        {
            var result = ReservationDomain.Factory.CreateReservationToDelete(ValidReservationId, Guid.Empty);

            Assert.False(result.IsValid);
            Assert.Contains(result.Notifications, x => x == "Invalid UserId");
        }


        [Fact]
        public void CreateReservationToDelete_Should_Return_ReservationDomain()
        {
            var result = ReservationDomain.Factory.CreateReservationToDelete(ValidReservationId, ValidUserId);

            Assert.True(result.IsValid);

            Assert.Equal(ValidReservationId, result.Id);
            Assert.Equal(ValidUserId, result.UserId);
            Assert.Equal(DateTime.MinValue, result.StartDate);
            Assert.Equal(DateTime.MinValue, result.EndDate);
            Assert.Null(result.Observation);
        }
    }
}
