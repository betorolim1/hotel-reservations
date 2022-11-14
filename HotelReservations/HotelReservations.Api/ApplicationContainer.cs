using HotelReservations.Core.Handlers;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Core.Reservations.Repository;
using HotelReservations.Core.Users.Repository;
using HotelReservations.Data.Dao;
using HotelReservations.Data.Repository;
using HotelReservations.Query.Handlers;
using HotelReservations.Query.Handlers.Interfaces;
using HotelReservations.Query.Reservations.Dao;
using HotelReservations.Query.Users.Dao;
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
            AddRepositories(service);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
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
            services.AddScoped<IReservationDao, ReservationDao>();
        }
    }
}
