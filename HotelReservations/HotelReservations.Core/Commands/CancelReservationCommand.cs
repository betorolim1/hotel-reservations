using System;

namespace HotelReservations.Core.Commands
{
    public class CancelReservationCommand
    {
        public Guid UserId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
