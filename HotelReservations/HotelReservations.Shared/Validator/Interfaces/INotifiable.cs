using System.Collections.Generic;

namespace HotelReservations.Shared.Validator.Interfaces
{
    public interface INotifiable
    {
        bool IsValid { get; }
        List<string> Notifications { get; }
    }
}
