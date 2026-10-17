using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Player.UseCases.Spawn;

public record PlayerSpawnedEvent(int PlayerId) : IEvent;
