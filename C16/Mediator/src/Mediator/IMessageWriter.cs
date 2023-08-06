namespace Mediator;

public interface IMessageWriter<TMessage>
{
    void Write(TMessage message);
}
