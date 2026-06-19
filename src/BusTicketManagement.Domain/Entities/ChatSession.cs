namespace BusTicketManagement.Domain.Entities;

public class ChatSession
{
    public int ChatSessionId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string SessionCode { get; set; } = string.Empty;
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }
    public string Status { get; set; } = "Active";
    public bool IsActive { get; set; } = true;
}
