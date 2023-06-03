namespace CQRS;

public interface IMessageWriter
{
    void Write(IChatRoom chatRoom, ChatMessage message);
}
