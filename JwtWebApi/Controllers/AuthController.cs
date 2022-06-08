using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace JwtWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User userMock = new User();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Regiser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userMock.Username = request.Username;
            userMock.PasswordHash = passwordHash;
            userMock.PasswordSalt = passwordSalt;
            return Ok(userMock);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if(userMock.Username != request.Username)
            {
                return BadRequest("User not found");
            }

            return Ok("MY Crazy");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  
            }  
        }
    }
}
