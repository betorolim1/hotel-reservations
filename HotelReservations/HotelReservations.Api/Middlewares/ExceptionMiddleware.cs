using HotelReservations.Api.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HotelReservations.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.EnvironmentName == "Development")
                    throw;

                var guidError = Guid.NewGuid().ToString("N");
                Log.LogError(exception: ex, method: context.Request.Method, url: context.Request.GetEncodedUrl(), guidError);
                context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { id = $"Error code: {guidError}" }));
            }
        }
    }
}
