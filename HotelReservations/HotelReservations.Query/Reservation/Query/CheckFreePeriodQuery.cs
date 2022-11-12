using System;

namespace HotelReservations.Query.Reservation.Query
{
    public class CheckFreePeriodQuery
    {
        public DateTime StartSearchDate { get; set; }
        public DateTime EndSearchDate { get; set; }
    }
}
