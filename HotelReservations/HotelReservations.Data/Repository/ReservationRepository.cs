using HotelReservations.Core.Reservations.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        public async Task<bool> IsValidPeriodAsync(DateTime dateTime, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
