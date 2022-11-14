using System;
using System.Collections.Generic;

namespace HotelReservations.Model.Models
{
    public class User
    {
        public User()
        {
            Reservations = new List<Reservation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<Reservation> Reservations { get; set; } 
    }
}
