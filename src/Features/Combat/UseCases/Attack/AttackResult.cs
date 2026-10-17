using Godot;

namespace BichosKombat.Features.Combat.UseCases.Attack;

public record AttackResult(bool Attacked, Vector2 Center, float Radius);
