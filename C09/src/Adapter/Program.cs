var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ExternalGreeter>();
builder.Services.AddSingleton<IGreeter, ExternalGreeterAdapter>();

var app = builder.Build();
app.MapGet("/", (IGreeter greeter) => greeter.Greeting());
app.Run();

public class ExternalGreeter
{
    public string GreetByName(string name)
    {
        return $"Adaptee says: hi {name}!";
    }
}

public interface IGreeter
{
    string Greeting();
}

public class ExternalGreeterAdapter : IGreeter
{
    private readonly ExternalGreeter _adaptee;

    public ExternalGreeterAdapter(ExternalGreeter adaptee)
    {
        _adaptee = adaptee ?? throw new ArgumentNullException(nameof(adaptee));
    }

    public string Greeting()
    {
        return _adaptee.GreetByName("ExternalGreeterAdapter");
    }
}