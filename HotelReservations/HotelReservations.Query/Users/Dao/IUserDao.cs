using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.Users.Dao
{
    public interface IUserDao
    {
        Task<List<Model.Models.User>> GetUsersAsync();
    }
}
