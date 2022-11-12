using System;
using System.Collections.Generic;

namespace HotelReservations.Model.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
