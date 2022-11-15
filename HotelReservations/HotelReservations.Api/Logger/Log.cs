using HotelReservations.Api.Logger.ErrorObject;
using System;
using System.IO;

namespace HotelReservations.Api.Logger
{
    public static class Log
    {
        public static void LogError(Exception exception, string method, string url, string guidError)
        {
            var obj = new Error
            {
                Id = guidError,
                ServerName = Environment.MachineName,
                Exception = exception,
                RequestMethod = method,
                RequestUrl = url
            };
            Directory.CreateDirectory("Logs");

            using (StreamWriter w = File.AppendText($"Logs\\Log_Error_{DateTime.Today.ToString("dd-MM-yyyy")}_logs.txt"))
            {
                w.WriteLine(obj.ToString());
            }
        }
    }
}
