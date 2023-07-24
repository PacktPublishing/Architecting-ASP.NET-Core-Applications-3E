namespace FinalChainOfResponsibility;

public class AlarmTriggeredHandler : SingleMessageHandlerBase
{
    protected override string HandledMessageName => "AlarmTriggered";

    public AlarmTriggeredHandler(IMessageHandler? next = null)
        : base(next) { }

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
