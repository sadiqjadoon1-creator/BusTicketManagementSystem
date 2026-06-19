namespace BusTicketManagement.Application.DTOs.AuthDTOs;

public class RegisterDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
}

public class RegisterResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
