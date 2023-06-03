namespace ChainOfResponsibility;

public interface IMessageHandler
{
    void Handle(Message message);
}
