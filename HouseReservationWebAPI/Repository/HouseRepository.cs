using System.Linq.Expressions;
using HouseReservationWebAPI.Data;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HouseReservationWebAPI.Repository
{
    public class HouseRepository : Repository<House>, IHouseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseRepository(ApplicationDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<House> UpdateAsync(House entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _dbContext.Houses.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
