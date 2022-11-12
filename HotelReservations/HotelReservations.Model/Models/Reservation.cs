using System;

namespace HotelReservations.Model.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }
    }
}
