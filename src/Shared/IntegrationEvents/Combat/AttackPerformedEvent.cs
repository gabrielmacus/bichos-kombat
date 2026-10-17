using Godot;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.IntegrationEvents.Combat;

// Un jugador pegó: todo lo que esté dentro del círculo (Center, Radius) recibe Damage. Lo resuelve quien tenga enemigos
public record AttackPerformedEvent(int PlayerId, Vector2 Center, float Radius, int Damage) : IEvent;
