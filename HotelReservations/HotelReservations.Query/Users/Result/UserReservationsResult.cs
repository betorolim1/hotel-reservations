using System;

namespace HotelReservations.Query.Users.Result
{
    public class UserReservationsResult
    {
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }
    }
}
