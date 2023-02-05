using Microsoft.AspNetCore.Mvc;
using NZWallker.API.Reposetories;

namespace NZWallker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserReposetory userReposetory;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserReposetory userReposetory, ITokenHandler tokenHandler)
        {
            this.userReposetory = userReposetory;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            var user = await userReposetory.AuthenticateAsync(
                loginRequest.UserName, loginRequest.Password);

            if(user != null)
            {
                // Generete jwt
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);   
            }

            return BadRequest("UserName Or Password is incurrect");
        }
    }
}
