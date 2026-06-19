using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace BusTicketManagement.Infrastructure.Repository;

public interface IChatRepository
{
    Task<List<ChatMessage>> GetChatHistoryAsync(int chatSessionId);
    Task<ChatMessage> SaveMessageAsync(ChatMessage message);
}

public class ChatRepository : BaseRepository, IChatRepository
{
    public ChatRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) { }

    public async Task<List<ChatMessage>> GetChatHistoryAsync(int chatSessionId)
    {
        const string sql = "SELECT ChatMessageId, ChatSessionId, SenderType, Message, MessageType, IntentType, Timestamp, IsRead FROM ChatMessages WHERE ChatSessionId = @ChatSessionId ORDER BY Timestamp";
        var parameters = new[] { new SqlParameter("@ChatSessionId", chatSessionId) };
        return await ExecuteReaderAsync(sql, MapChatMessage, CommandType.Text, parameters);
    }

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage message)
    {
        const string sql = @"INSERT INTO ChatMessages (ChatSessionId, SenderType, Message, MessageType, IntentType, Timestamp, IsRead) 
                            VALUES (@ChatSessionId, @SenderType, @Message, @MessageType, @IntentType, @Timestamp, @IsRead);
                            SELECT SCOPE_IDENTITY();";
        
        var parameters = new[]
        {
            new SqlParameter("@ChatSessionId", message.ChatSessionId),
            new SqlParameter("@SenderType", message.SenderType),
            new SqlParameter("@Message", message.Message),
            new SqlParameter("@MessageType", message.MessageType),
            new SqlParameter("@IntentType", message.IntentType),
            new SqlParameter("@Timestamp", message.Timestamp),
            new SqlParameter("@IsRead", message.IsRead)
        };

        var id = await ExecuteScalarAsync<int>(sql, CommandType.Text, parameters);
        message.ChatMessageId = id;
        return message;
    }

    private ChatMessage MapChatMessage(SqlDataReader reader) => new()
    {
        ChatMessageId = reader.GetInt32(0),
        ChatSessionId = reader.GetInt32(1),
        SenderType = reader.GetString(2),
        Message = reader.GetString(3),
        MessageType = reader.GetString(4),
        IntentType = reader.GetString(5),
        Timestamp = reader.GetDateTime(6),
        IsRead = reader.GetBoolean(7)
    };
}
