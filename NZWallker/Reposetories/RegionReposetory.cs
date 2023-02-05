using Microsoft.EntityFrameworkCore;
using NZWallker.API.Data;
using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public class RegionReposetory : IRegionReposetory
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionReposetory(NZWalksDbContext  nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> addAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region); // add to db context first
            await nZWalksDbContext.SaveChangesAsync(); // save db context changes into the database
            return region;
        }

        public async Task<Region> deleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(item => item.Id == id);

            if (region == null) 
            {
                return null;
            }

            // Delete the region
            nZWalksDbContext.Regions.Remove(region); 
            await nZWalksDbContext.SaveChangesAsync();
            return region;  
        }

        public async Task<IEnumerable<Region>> getAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> getAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Region> updateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(item => item.Id == id);

            if (existingRegion == null) 
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;  
        }
    }
}
