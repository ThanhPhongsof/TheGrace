using MediatR;

namespace TheGrace.Contract.Abstractions;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
}

//public interface IDomainEventWithIdsHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEventWithIds
//{
//}

//public interface IDomainEventWithHandlerReceiverIds<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEventWithReceiverIds
//{
//}

//public interface IDomainEventWithHandlerSchedule<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEventWithSchedule
//{
//}

//public interface IDomainEventWithHandlerSRNesux<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEventWithSRNesux
//{
//}
