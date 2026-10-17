using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.IntegrationEvents.Player;

public record PlayerDamagedEvent(int PlayerId, int Damage, int Health, int MaxHealth) : IEvent;
