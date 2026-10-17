using Godot;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.UseCases.Move;

// Cada frame: decide velocidad, hacia dónde mira y el estado. El nodo aplica la velocidad y dibuja el estado
public class MoveHandler(PlayerRepository playerRepository)
{
    public MoveResult Handle(MoveInput input)
    {
        var player = playerRepository.Get(input.PlayerId);
        player.Position = input.Position;

        // 1. Muerto: no se mueve más
        if (player.State == PlayerState.Dead) return new MoveResult(Vector2.Zero, player.Facing, PlayerState.Dead);

        // 2. Se descuenta el tiempo de Hurt
        if (player.HurtTimeLeft > 0) player.HurtTimeLeft -= input.Delta;

        // 3. Dirección: nunca más rápido en diagonal; si se mueve, mira hacia donde va
        var direction = input.Direction.LimitLength(1f);
        if (!direction.IsZeroApprox()) player.Facing = direction.Normalized();

        // 4. Estado
        if (player.HurtTimeLeft > 0) player.State = PlayerState.Hurt;
        else if (direction.IsZeroApprox()) player.State = PlayerState.Idle;
        else player.State = PlayerState.Moving;

        return new MoveResult(direction * player.Speed, player.Facing, player.State);
    }
}
