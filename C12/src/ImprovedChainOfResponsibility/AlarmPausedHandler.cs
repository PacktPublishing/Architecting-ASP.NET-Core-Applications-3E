namespace ImprovedChainOfResponsibility;

public class AlarmPausedHandler : MessageHandlerBase
{
    protected override string HandledMessageName => "AlarmPaused";

    public AlarmPausedHandler(IMessageHandler? next = null) : base(next) { }

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
