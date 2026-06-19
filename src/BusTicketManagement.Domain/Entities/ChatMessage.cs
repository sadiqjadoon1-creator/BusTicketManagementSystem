namespace BusTicketManagement.Domain.Entities;

public class ChatMessage
{
    public int ChatMessageId { get; set; }
    public int ChatSessionId { get; set; }
    public string SenderType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string MessageType { get; set; } = "Text";
    public string IntentType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
}
