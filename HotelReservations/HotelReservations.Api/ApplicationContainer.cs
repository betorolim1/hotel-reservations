using HotelReservations.Core.Handlers;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Data.Dao;
using HotelReservations.Query.Handlers;
using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.User.Dao;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservations.Api
{
    public static class ApplicationContainer
    {
        public static void ConfigureDIs(this IServiceCollection service)
        {
            AddHandlers(service);
            AddQueryHandlers(service);
            AddDaos(service);
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IReservationQueryHandler, ReservationQueryHandler>();
            services.AddScoped<IUserQueryHandler, UserQueryHandler>();
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IUserHandler, UserHandler>();
        }

        private static void AddDaos(IServiceCollection services)
        {
            services.AddScoped<IUserDao, UserDao>();
        }
    }
}
