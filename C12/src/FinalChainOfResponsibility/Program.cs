#define USE_SCRUTOR
using FinalChainOfResponsibility;

var builder = WebApplication.CreateBuilder(args);
// Create the chain of responsibility,
// ordered by the most called (or the ones to execute earlier)
// to the less called handler (or the ones that take more time to execute),
// followed by the DefaultHandler.
#if USE_SCRUTOR
// When using the Decorate method (from Scrutor), we must register the chain
// in the reverse order.
builder.Services
    .AddSingleton<IMessageHandler, DefaultHandler>()
    .Decorate<IMessageHandler, AlarmStoppedHandler>()
    .Decorate<IMessageHandler, AlarmPausedHandler>()
    .Decorate<IMessageHandler, AlarmTriggeredHandler>()
;
#else
builder.Services.AddSingleton<IMessageHandler>(
    new AlarmTriggeredHandler(
        new AlarmPausedHandler(
            new AlarmStoppedHandler(
                new DefaultHandler()
            ))));
#endif

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
