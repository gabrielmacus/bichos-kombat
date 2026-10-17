using System;
using System.Collections.Generic;

namespace BichosKombat.Shared.Messaging;

// Bus de eventos en memoria: quien publica no conoce a quien escucha (1 a N)
public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public static void Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        Subscribe<TEvent>(handler.Handle);
    }

    public static void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
    {
        var type = typeof(TEvent);
        if (!_subscribers.ContainsKey(type))
        {
            _subscribers[type] = new List<Delegate>();
        }
        _subscribers[type].Add(callback);
    }

    public static void Unsubscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
    {
        if (_subscribers.TryGetValue(typeof(TEvent), out var list))
        {
            list.Remove(callback);
        }
    }

    public static void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (_subscribers.TryGetValue(typeof(TEvent), out var list))
        {
            // Se recorre una copia: así un suscriptor puede desuscribirse sin romper el foreach
            foreach (var subscriber in list.ToArray())
            {
                ((Action<TEvent>)subscriber)(@event);
            }
        }
    }

    public static void Clear()
    {
        _subscribers.Clear();
    }
}
