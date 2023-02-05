using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public interface IRegionReposetory
    {
        Task<IEnumerable<Region>> getAllAsync();

        Task<Region> getAsync(Guid id);

        Task<Region> addAsync(Region region);   

        Task<Region> deleteAsync(Guid id);
        Task<Region> updateAsync(Guid id, Region region); // region = updateds region
        
    }
}
