using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Dao;
using HotelReservations.Query.Reservations.Query;
using HotelReservations.Query.Reservations.Result;
using HotelReservations.Shared.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservations.Query.Handlers
{
    public class ReservationQueryHandler : Notifiable, IReservationQueryHandler
    {
        private IReservationDao _reservationDao;

        public ReservationQueryHandler(IReservationDao reservationDao)
        {
            _reservationDao = reservationDao;
        }

        public async Task<List<ReservationInPeriodResult>> GetReservationsInPeriodAsync(CheckFreePeriodQuery query)
        {
            if (query.StartSearchDate.Date < DateTime.Now.Date)
                AddNotification("Invalid StartDate");

            if (query.EndSearchDate.Date < query.StartSearchDate.Date)
                AddNotification("Invalid dates");

            if (!IsValid)
                return null;

            var reservationList = await _reservationDao.GetReservationsInPeriodAsync(query.StartSearchDate, query.EndSearchDate);

            var result = reservationList.Select(x => new ReservationInPeriodResult
            {
                EndDate = x.EndDate,
                StartDate = x.StartDate
            }).ToList();

            return result;
        }
    }
}
