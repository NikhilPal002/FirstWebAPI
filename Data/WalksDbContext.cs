using FirstWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}