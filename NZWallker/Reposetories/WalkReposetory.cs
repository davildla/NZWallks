using Microsoft.EntityFrameworkCore;
using NZWallker.API.Data;
using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public class WalkReposetory : IWalkReposetory
    {
        // connect to db
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkReposetory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> addAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk); // add to db context first
            await nZWalksDbContext.SaveChangesAsync(); // save db context changes into the database
            return walk;
        }

        public async Task<Walk> deleteAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(item => item.Id == id);

            if (walk == null)
            {
                return null;
            }

            // Delete the region
            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> getAllAsync()
        {
            // we also return data of connected region and walk difficulty
            return await nZWalksDbContext.Walks.
                Include( x => x.Region).
                Include( x => x.WalkDifficulty).
                ToListAsync();
        }

        public async Task<Walk> getAsync(Guid id)
        {
            return await nZWalksDbContext.Walks.
                Include( x => x.Region).
                Include( x => x.WalkDifficulty).
                FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<Walk> updateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(item => item.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
