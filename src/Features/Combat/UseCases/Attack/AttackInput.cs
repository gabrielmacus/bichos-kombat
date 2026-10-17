using Godot;

namespace BichosKombat.Features.Combat.UseCases.Attack;

// Se manda cada frame (para descontar el cooldown). Pressed: si el jugador está apretando la tecla de ataque
public record AttackInput(int PlayerId, bool Pressed, Vector2 Position, Vector2 Facing, float Delta);
