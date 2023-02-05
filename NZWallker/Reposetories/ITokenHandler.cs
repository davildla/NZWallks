using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
