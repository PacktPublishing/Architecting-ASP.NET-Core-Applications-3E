using FinalChainOfResponsibility;

var builder = WebApplication.CreateBuilder(args);
// Create the chain of responsibility,
// ordered by the most called (or the ones to execute earlier)
// to the less called handler (or the ones that take more time to execute),
// followed by the DefaultHandler.
builder.Services.AddSingleton<IMessageHandler>(
    new AlarmTriggeredHandler(
        new AlarmPausedHandler(
            new AlarmStoppedHandler(
                new DefaultHandler()
            ))));

var app = builder.Build();
app.MapPost(
    "/handle",
    (Message message, IMessageHandler messageHandler) =>
    {
        try
        {
            messageHandler.Handle(message);
            return $"Message '{message.Name}' handled successfully.";
        }
        catch (NotSupportedException ex)
        {
            return ex.Message;
        }
    });
app.Run();
