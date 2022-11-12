using HotelReservations.Core.Handlers;
using HotelReservations.Core.Handlers.Interfaces;
using HotelReservations.Query.Handlers;
using HotelReservations.Query.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservations.Api
{
    public static class ApplicationContainer
    {
        public static void ConfigureDIs(this IServiceCollection service)
        {
            AddHandlers(service);
            AddQueryHandlers(service);
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IReservationQueryHandler, ReservationQueryHandler>();
        }

        private static void AddHandlers(IServiceCollection services)
        {
            services.AddScoped<IUserHandler, UserHandler>();
        }
    }
}
