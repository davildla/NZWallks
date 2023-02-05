using NZWallker.API.Models.Domain;

namespace NZWallker.API.Reposetories
{
    public interface IUserReposetory
    {
        Task<User> AuthenticateAsync(string username, string password); 
    }
}
