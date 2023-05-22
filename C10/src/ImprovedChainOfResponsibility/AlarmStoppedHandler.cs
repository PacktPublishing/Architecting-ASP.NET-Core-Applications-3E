namespace ImprovedChainOfResponsibility;

public class AlarmStoppedHandler : MessageHandlerBase
{
    protected override string HandledMessageName => "AlarmStopped";

    public AlarmStoppedHandler(IMessageHandler? next = null) : base(next) { }

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
