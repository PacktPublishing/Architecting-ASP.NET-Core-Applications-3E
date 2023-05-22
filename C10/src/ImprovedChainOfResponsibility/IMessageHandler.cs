namespace ImprovedChainOfResponsibility;

public interface IMessageHandler
{
    void Handle(Message message);
}
