using System.Linq.Expressions;
using HouseReservationWebAPI.Models;

namespace HouseReservationWebAPI.Repository.IRepository
{
    public interface IHouseRepository : IRepository<House>
    {
        Task <House> UpdateAsync(House entity);
    }
}
