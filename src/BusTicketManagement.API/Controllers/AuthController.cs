using BusTicketManagement.Application.DTOs.AuthDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RegisterResponseDto>>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RegisterResponseDto>.FailureResponse("Invalid registration data"));

            var result = await _authService.RegisterAsync(registerDto);
            return Ok(ApiResponse<RegisterResponseDto>.SuccessResponse(result, "User registered successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration error");
            return BadRequest(ApiResponse<RegisterResponseDto>.FailureResponse(ex.Message));
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LoginResponseDto>.FailureResponse("Invalid login data"));

            var result = await _authService.LoginAsync(loginDto);
            return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Login successful"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login error");
            return Unauthorized(ApiResponse<LoginResponseDto>.FailureResponse("Invalid credentials"));
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> RefreshToken([FromBody] string refreshToken)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Token refreshed successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token refresh error");
            return Unauthorized(ApiResponse<LoginResponseDto>.FailureResponse("Invalid refresh token"));
        }
    }
}
