using HotelReservations.Shared.Validator;
using System;

namespace HotelReservations.Core.Reservations.Domain
{
    public class ReservationDomain : Notifiable
    {
        private int MAX_STRING_LENGTH = 255;

        private ReservationDomain(Guid userId, DateTime startDate, DateTime endDate, string observation)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            Observation = observation;

            ValidateMainFields();
        }

        private ReservationDomain(Guid id, Guid userId, DateTime startDate, DateTime endDate, string observation)
        {
            Id = id;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            Observation = observation;

            ValidateId();
            ValidateMainFields();
        }

        private ReservationDomain(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;

            ValidateId();
            ValidateUserId();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Observation { get; set; }

        private void ValidateId()
        {
            if (Id == Guid.Empty)
                AddNotification("Invalid ReservationId");
        }

        private void ValidateUserId()
        {
            if (UserId == Guid.Empty)
                AddNotification("Invalid UserId");
        }

        private void ValidateMainFields()
        {
            ValidateUserId();

            if (StartDate.Date < DateTime.Now.Date)
                AddNotification("Invalid StartDate");
            
            if (StartDate.Date == DateTime.Now.Date)
                AddNotification("Stay can't start today");

            if(EndDate.Date < StartDate.Date)
                AddNotification("Invalid dates");

            if(IsValidRangeDate())
                AddNotification("Stay can’t be longer than 3 days");

            if(IsValidStartDateFromToday())
                AddNotification("Stay can’t be reserved more than 30 days in advance");

            if (Observation?.Length > MAX_STRING_LENGTH)
                AddNotification($"Observation max length is {MAX_STRING_LENGTH}");
        }

        private bool IsValidRangeDate()
        {
            var totalRange = EndDate.Date - StartDate.Date;

            return totalRange.Days > 3;
        }

        private bool IsValidStartDateFromToday()
        {
            var totalRange = StartDate.Date - DateTime.Now.Date;

            return totalRange.Days > 30;
        }

        public static class Factory
        {
            public static ReservationDomain CreateReservationToInsert(Guid userId, DateTime startDate, DateTime endDate, string observation)
                => new ReservationDomain(userId, startDate, endDate, observation);
            
            public static ReservationDomain CreateReservationToUpdate(Guid id, Guid userId, DateTime startDate, DateTime endDate, string observation)
                => new ReservationDomain(id, userId, startDate, endDate, observation);
            
            public static ReservationDomain CreateReservationToDelete(Guid id, Guid userId)
                => new ReservationDomain(id, userId);
        }
    }
}
