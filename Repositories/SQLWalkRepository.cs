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

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(x=>x.ID==id);

            if(existingWalk == null){
                return null;
            }

            context.Walks.Remove(existingWalk);
            await context.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.ID==id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(x=>x.ID==id);

            if(existingWalk == null){
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await context.SaveChangesAsync();
            return existingWalk;
        }
    }
}