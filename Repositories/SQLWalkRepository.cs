using FirstWebAPI.Data;
using FirstWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace FirstWebAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WalksDbContext context;

        public SQLWalkRepository(WalksDbContext context)
        {
            this.context = context;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}