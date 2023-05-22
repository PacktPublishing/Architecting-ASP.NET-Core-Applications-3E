namespace Mediator;

public interface IMediator
{
    void Send(Message message);
}

public interface IColleague
{
    string Name { get; }
    void ReceiveMessage(Message message);
}

public class Message
{
    public Message(IColleague from, string content)
    {
        Sender = from ?? throw new ArgumentNullException(nameof(from));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public IColleague Sender { get; }
    public string Content { get; }
}

public class ConcreteMediator : IMediator
{
    private readonly List<IColleague> _colleagues;
    public ConcreteMediator(params IColleague[] colleagues)
    {
        ArgumentNullException.ThrowIfNull(colleagues);
        _colleagues = new List<IColleague>(colleagues);
    }

    public void Send(Message message)
    {
        foreach (var colleague in _colleagues)
        {
            colleague.ReceiveMessage(message);
        }
    }
}

public class ConcreteColleague : IColleague
{
    private readonly IMessageWriter<Message> _messageWriter;
    public ConcreteColleague(string name, IMessageWriter<Message> messageWriter)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
    }

    public string Name { get; }

    public void ReceiveMessage(Message message)
    {
        _messageWriter.Write(message);
    }
}
