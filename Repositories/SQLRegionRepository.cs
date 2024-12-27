

using FirstWebAPI.Data;
using FirstWebAPI.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext context;

        public SQLRegionRepository(WalksDbContext context)
        {
            this.context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            context.Regions.Remove(existingRegion);
            await context.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await context.SaveChangesAsync();
            return existingRegion;
        }
    }
}