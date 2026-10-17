using Godot;

namespace BichosKombat.Features.Enemies.UseCases.Think;

// Lo que el enemigo percibe de un jugador
public record EnemyTarget(int PlayerId, Vector2 Position, bool Alive);
