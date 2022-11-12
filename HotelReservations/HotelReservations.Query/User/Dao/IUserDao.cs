using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservations.Query.User.Dao
{
    public interface IUserDao
    {
        Task<List<Model.Models.User>> GetUsersAsync();
    }
}
