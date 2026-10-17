using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.IntegrationEvents.Player;

// Algo le pegó a un jugador (lo publica quien pega: Enemies, trampas...). Player decide cuánto le saca
public record PlayerHitEvent(int PlayerId, int Damage) : IEvent;
