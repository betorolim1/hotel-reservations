using System;

namespace HotelReservations.Core.Commands
{
    public class UpdateReservationCommand
    {
        public Guid UserId { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }
    }
}
