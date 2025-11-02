using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Entities.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("UserName already Exists");
            }
            
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            
            if (result is null)
            {
                return BadRequest("Invalid username or password");
            } 
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);

            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token");
            }
            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndPoint()
        {

            return Ok("You are authebticated!");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndPoint()
        {

            return Ok("You are authenticated! And admin");
        }

    }


}
