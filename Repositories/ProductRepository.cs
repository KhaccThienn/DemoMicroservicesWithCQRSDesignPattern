using MicroservicesWithCQRSDesignPattern.AppDbContext;
using MicroservicesWithCQRSDesignPattern.Interfaces;
using MicroservicesWithCQRSDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesWithCQRSDesignPattern.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product entity)
        {
            await _dbContext.Products.AddAsync(entity);
        }

        public async Task DeleteAsync(Product entity)
        {
            _dbContext.Products.Remove(entity);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Products.Update(entity);
        }
    }
}
