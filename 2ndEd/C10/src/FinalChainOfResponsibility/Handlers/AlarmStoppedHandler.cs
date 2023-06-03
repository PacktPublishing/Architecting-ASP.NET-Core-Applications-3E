namespace FinalChainOfResponsibility;

public class AlarmStoppedHandler : SingleMessageHandlerBase
{
    protected override string HandledMessageName => "AlarmStopped";

    public AlarmStoppedHandler(IMessageHandler? next = null)
        : base(next) { }

    protected override void Process(Message message)
    {
        // Do something clever with the Payload
    }
}
