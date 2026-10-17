using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.IntegrationEvents.Player;

public record PlayerDiedEvent(int PlayerId) : IEvent;
