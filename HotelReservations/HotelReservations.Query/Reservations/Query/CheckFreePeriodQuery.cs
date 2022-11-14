using System;

namespace HotelReservations.Query.Reservations.Query
{
    public class CheckFreePeriodQuery
    {
        public DateTime StartSearchDate { get; set; }
        public DateTime EndSearchDate { get; set; }
    }
}
