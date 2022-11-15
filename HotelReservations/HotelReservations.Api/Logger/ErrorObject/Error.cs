using System;

namespace HotelReservations.Api.Logger.ErrorObject
{
    public class Error
    {
        public string Id { get; set; }
        public string ServerName { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return $@"
[ID]                  : {Id}
[Date]                : {DateTime.Now}
[Request URL]         : {RequestUrl}
[Request Method]      : {RequestMethod}
[Server Name]         : {ServerName}
[Exception]           : {Environment.NewLine}{Exception.Message}
[StackTrace]          : {Environment.NewLine}{Exception.StackTrace}
                ";
        }
    }
}
