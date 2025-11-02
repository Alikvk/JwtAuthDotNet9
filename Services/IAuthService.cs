using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Entities.Models;

namespace JwtAuthDotNet9.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);

    }
}
