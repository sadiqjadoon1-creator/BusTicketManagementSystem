using BusTicketManagement.Application.DTOs.ChatDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ChatService> _logger;

    public ChatService(IChatRepository chatRepository, IMapper mapper, ILogger<ChatService> logger)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ChatMessageDto>> GetChatHistoryAsync(int chatSessionId)
    {
        try
        {
            _logger.LogInformation($"Fetching chat history for session {chatSessionId}");
            var messages = await _chatRepository.GetChatHistoryAsync(chatSessionId);
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching chat history");
            throw;
        }
    }

    public async Task<ChatMessageDto> SendMessageAsync(int chatSessionId, SendChatMessageDto messageDto)
    {
        try
        {
            _logger.LogInformation($"Sending message in chat session {chatSessionId}");
            
            var message = new ChatMessage
            {
                ChatSessionId = chatSessionId,
                SenderType = "User",
                Message = messageDto.Message,
                MessageType = "Text",
                IntentType = DetectIntent(messageDto.Message),
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            var savedMessage = await _chatRepository.SaveMessageAsync(message);
            _logger.LogInformation($"Message saved with ID: {savedMessage.ChatMessageId}");
            
            return _mapper.Map<ChatMessageDto>(savedMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message");
            throw;
        }
    }

    public async Task<ChatMessageDto> GetAutoResponseAsync(string message)
    {
        try
        {
            _logger.LogInformation($"Getting auto response for: {message}");
            
            var intent = DetectIntent(message);
            var response = GetResponseByIntent(intent);

            var autoResponse = new ChatMessage
            {
                SenderType = "System",
                Message = response,
                MessageType = "Text",
                IntentType = intent,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            return _mapper.Map<ChatMessageDto>(autoResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting auto response");
            throw;
        }
    }

    private string DetectIntent(string message)
    {
        var lowerMessage = message.ToLower();
        
        if (lowerMessage.Contains("book") || lowerMessage.Contains("booking"))
            return "booking";
        if (lowerMessage.Contains("cancel") || lowerMessage.Contains("cancellation"))
            return "cancel";
        if (lowerMessage.Contains("price") || lowerMessage.Contains("cost") || lowerMessage.Contains("fare"))
            return "price";
        if (lowerMessage.Contains("schedule") || lowerMessage.Contains("timing") || lowerMessage.Contains("time"))
            return "schedule";
        if (lowerMessage.Contains("seat") || lowerMessage.Contains("availability"))
            return "availability";
        if (lowerMessage.Contains("refund") || lowerMessage.Contains("money"))
            return "refund";
        if (lowerMessage.Contains("payment") || lowerMessage.Contains("pay"))
            return "payment";
        
        return "general";
    }

    private string GetResponseByIntent(string intent)
    {
        return intent switch
        {
            "booking" => "To book a ticket: 1) Select your source and destination 2) Choose a date 3) Select a bus schedule 4) Pick your seat(s) 5) Review and confirm booking 6) Make payment.",
            "cancel" => "Ticket cancellation is allowed up to 48 hours before departure. Cancellations made within 48 hours will incur a 30% deduction from the ticket fare.",
            "price" => "Ticket prices vary based on the route distance and bus type. Our fares are competitive and include all taxes.",
            "schedule" => "You can view all available bus schedules by selecting your source city, destination city, and travel date.",
            "availability" => "Seat availability is shown in real-time on the booking page. Green seats are available, and booked seats appear in red.",
            "refund" => "Refunds are processed according to our cancellation policy. Refunds are typically processed within 5-7 business days.",
            "payment" => "We accept multiple payment methods: Credit Card, Debit Card, Bank Transfer, and Mobile Wallet.",
            _ => "Thank you for your query. How can I help you today?"
        };
    }
}
