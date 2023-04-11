using System.Linq.Expressions;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HouseReservationWebAPI.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateAsync(House entity)
        {
           await _dbContext.Houses.AddAsync(entity);
        }

        public async Task DeleteAsync(House entity)
        {
            _dbContext.Houses.Remove(entity);
            await SaveAsync(entity);
        }

        public async Task SaveAsync(House entity)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<House>> GetAllAsync(Expression<Func<House, bool>> filter = null)
        {
            IQueryable<House> query = _dbContext.Houses;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<House> GetAsync(Expression<Func<House, bool>> filter = null, bool isTracked = true)
        {
            IQueryable<House> query = _dbContext.Houses;
            if (!isTracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(House entity)
        {
            _dbContext.Update(entity);
            await SaveAsync(entity);
        }
    }
}
