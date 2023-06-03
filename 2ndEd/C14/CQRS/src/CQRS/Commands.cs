namespace CQRS;

public class JoinChatRoom
{
    public class Command : ICommand
    {
        public Command(IChatRoom chatRoom, IParticipant requester)
        {
            ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        public IChatRoom ChatRoom { get; }
        public IParticipant Requester { get; }
    }

    public class Handler : ICommandHandler<Command>
    {
        public void Handle(Command command)
        {
            command.ChatRoom.Add(command.Requester);
        }
    }
}

public class LeaveChatRoom
{
    public class Command : ICommand
    {
        public Command(IChatRoom chatRoom, IParticipant requester)
        {
            ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        public IChatRoom ChatRoom { get; }
        public IParticipant Requester { get; }
    }

    public class Handler : ICommandHandler<Command>
    {
        public void Handle(Command command)
        {
            command.ChatRoom.Remove(command.Requester);
        }
    }
}

public class SendChatMessage
{
    public class Command : ICommand
    {
        public Command(IChatRoom chatRoom, ChatMessage message)
        {
            ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public IChatRoom ChatRoom { get; }
        public ChatMessage Message { get; }
    }

    public class Handler : ICommandHandler<Command>
    {
        public void Handle(Command command)
        {
            command.ChatRoom.Add(command.Message);
            foreach (var participant in command.ChatRoom.ListParticipants())
            {
                participant.NewMessageReceivedFrom(command.ChatRoom, command.Message);
            }
        }
    }
}
