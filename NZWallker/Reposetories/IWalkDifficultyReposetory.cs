using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public interface IWalkDifficultyReposetory
    {
        Task<IEnumerable<WalkDifficulty>> getAllAsync();

        Task<WalkDifficulty> getAsync(Guid id);
        Task<WalkDifficulty> addAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> deleteAsync(Guid id);
        Task<WalkDifficulty> updateAsync(Guid id, WalkDifficulty walkDifficulty); 
    }
}
