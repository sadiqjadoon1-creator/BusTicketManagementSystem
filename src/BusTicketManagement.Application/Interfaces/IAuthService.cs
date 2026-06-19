using BusTicketManagement.Application.DTOs.AuthDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string userId);
}
