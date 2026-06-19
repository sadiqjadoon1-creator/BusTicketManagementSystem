using BusTicketManagement.Application.DTOs.AuthDTOs;
using BusTicketManagement.Application.Interfaces;

namespace BusTicketManagement.Application.Services;

public class AuthService : IAuthService
{
    public Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
