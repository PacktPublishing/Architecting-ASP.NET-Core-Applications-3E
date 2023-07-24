namespace FinalChainOfResponsibility;

public class SomeMultiHandler : MultipleMessageHandlerBase
{
    public SomeMultiHandler(IMessageHandler? next = null)
        : base(next) { }

    protected override string[] HandledMessagesName
        => new[] { "Foo", "Bar", "Baz" };

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
