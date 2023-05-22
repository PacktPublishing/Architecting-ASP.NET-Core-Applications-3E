namespace FinalChainOfResponsibility;

public class AlarmPausedHandler : SingleMessageHandlerBase
{
    protected override string HandledMessageName => "AlarmPaused";

    public AlarmPausedHandler(IMessageHandler? next = null)
        : base(next) { }

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
