using System.Linq.Expressions;
using HouseReservationWebAPI.Models;

namespace HouseReservationWebAPI.Repository.IRepository
{
    public interface IHouseRepository
    {
        Task CreateAsync(House  entity);
        Task DeleteAsync(House entity);
        Task SaveAsync(House entity);
        Task UpdateAsync(House entity);
        Task<List<House>> GetAllAsync(Expression<Func<House, bool>> filter = null);
        Task<House> GetAsync(Expression<Func<House, bool>> filter = null, bool isTracked = true);

    }
}
