using Godot;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.IntegrationEvents.Enemies;

// Murió un enemigo. KillerPlayerId: quién le dio el último golpe (para la experiencia)
public record EnemyDiedEvent(int EnemyId, Vector2 Position, int Experience, int KillerPlayerId) : IEvent;
