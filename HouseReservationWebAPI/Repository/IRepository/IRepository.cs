using HouseReservationWebAPI.Models;
using System.Linq.Expressions;

namespace HouseReservationWebAPI.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool isTracked = true);
    }
}
