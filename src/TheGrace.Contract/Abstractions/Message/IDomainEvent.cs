using MediatR;

namespace TheGrace.Contract.Abstractions;

public interface IDomainEvent : INotification
{
    public string ReceiverId { get; init; }

    public int Id { get; init; }
}

public interface IDomainEventWithIds : INotification
{
    public string ReceiverId { get; init; }

    public List<int> Ids { get; init; }
}
