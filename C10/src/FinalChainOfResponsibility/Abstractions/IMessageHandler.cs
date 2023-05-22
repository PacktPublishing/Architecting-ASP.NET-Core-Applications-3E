namespace FinalChainOfResponsibility;

public interface IMessageHandler
{
    void Handle(Message message);
}
