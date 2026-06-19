using BusTicketManagement.Application.DTOs.ChatDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IChatService
{
    Task<List<ChatMessageDto>> GetChatHistoryAsync(int chatSessionId);
    Task<ChatMessageDto> SendMessageAsync(int chatSessionId, SendChatMessageDto messageDto);
    Task<ChatMessageDto> GetAutoResponseAsync(string message);
}
