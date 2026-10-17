namespace BichosKombat.Shared.Messaging;

// Un caso de uso que reacciona a un evento. Se registra con EventBus.Register(handler)
public interface IEventHandler<TEvent> where TEvent : IEvent
{
    void Handle(TEvent @event);
}
