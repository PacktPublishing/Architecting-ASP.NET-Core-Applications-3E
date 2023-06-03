namespace CQRS;

public class ListParticipants
{
    public class Query : IQuery<IEnumerable<IParticipant>>
    {
        public Query(IChatRoom chatRoom, IParticipant requester)
        {
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
        }

        public IParticipant Requester { get; }
        public IChatRoom ChatRoom { get; }
    }

    public class Handler : IQueryHandler<Query, IEnumerable<IParticipant>>
    {
        public IEnumerable<IParticipant> Handle(Query query)
        {
            return query.ChatRoom.ListParticipants();
        }
    }
}

public class ListMessages
{
    public class Query : IQuery<IEnumerable<ChatMessage>>
    {
        public Query(IChatRoom chatRoom, IParticipant requester)
        {
            Requester = requester ?? throw new ArgumentNullException(nameof(requester));
            ChatRoom = chatRoom ?? throw new ArgumentNullException(nameof(chatRoom));
        }

        public IParticipant Requester { get; }
        public IChatRoom ChatRoom { get; }
    }

    public class Handler : IQueryHandler<Query, IEnumerable<ChatMessage>>
    {
        public IEnumerable<ChatMessage> Handle(Query query)
        {
            return query.ChatRoom.ListMessages();
        }
    }
}
