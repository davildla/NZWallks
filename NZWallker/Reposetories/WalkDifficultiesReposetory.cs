using NZWallker.API.Data;
using NZWallker.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWallker.API.Reposetories
{
    public class WalkDifficultiesReposetory : IWalkDifficultyReposetory
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultiesReposetory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> addAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walkDifficulty); // add to db context first
            await nZWalksDbContext.SaveChangesAsync(); // save db context changes into the database
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> deleteAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(item => item.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            // Delete the region
            nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> getAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> getAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<WalkDifficulty> updateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(item => item.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }
            existingWalkDifficulty.Code = walkDifficulty.Code;


            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }

    }
}
