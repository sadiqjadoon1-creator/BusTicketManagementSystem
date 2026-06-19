namespace BusTicketManagement.Application.DTOs.ChatDTOs;

public class ChatMessageDto
{
    public int ChatMessageId { get; set; }
    public int ChatSessionId { get; set; }
    public string SenderType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public string IntentType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

public class SendChatMessageDto
{
    public string Message { get; set; } = string.Empty;
}
