using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public interface IWalkReposetory
    {
        Task<IEnumerable<Walk>> getAllAsync();

        Task<Walk> getAsync(Guid id);
        Task<Walk> addAsync(Walk walk);
        Task<Walk> deleteAsync(Guid id);
        Task<Walk> updateAsync(Guid id, Walk walk); // region = updateds region
    }
}
