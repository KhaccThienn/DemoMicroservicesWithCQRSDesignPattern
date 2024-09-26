using MicroservicesWithCQRSDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicesWithCQRSDesignPattern.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
