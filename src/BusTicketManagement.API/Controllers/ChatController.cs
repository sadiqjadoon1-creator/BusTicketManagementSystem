using BusTicketManagement.Application.DTOs.ChatDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BusTicketManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly ILogger<ChatController> _logger;

    public ChatController(IChatService chatService, IHubContext<ChatHub> hubContext, ILogger<ChatController> logger)
    {
        _chatService = chatService;
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpPost("message")]
    public async Task<ActionResult<ApiResponse<ChatMessageDto>>> SendMessage([FromBody] SendChatMessageDto messageDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ChatMessageDto>.FailureResponse("Invalid message data"));

            var userMessage = await _chatService.SendMessageAsync(1, messageDto); // Mock chat session ID
            var autoResponse = await _chatService.GetAutoResponseAsync(messageDto.Message);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", userMessage);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", autoResponse);

            return Ok(ApiResponse<ChatMessageDto>.SuccessResponse(userMessage, "Message sent successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending message");
            return BadRequest(ApiResponse<ChatMessageDto>.FailureResponse(ex.Message));
        }
    }

    [HttpGet("history/{sessionId}")]
    public async Task<ActionResult<ApiResponse<List<ChatMessageDto>>>> GetChatHistory(int sessionId)
    {
        try
        {
            var messages = await _chatService.GetChatHistoryAsync(sessionId);
            return Ok(ApiResponse<List<ChatMessageDto>>.SuccessResponse(messages, "Chat history retrieved successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving chat history");
            return BadRequest(ApiResponse<List<ChatMessageDto>>.FailureResponse(ex.Message));
        }
    }
}

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
