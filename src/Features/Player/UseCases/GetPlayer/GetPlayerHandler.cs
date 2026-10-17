using BichosKombat.Features.Player.Models;
using BichosKombat.Features.Player.UseCases.GetPlayers;

namespace BichosKombat.Features.Player.UseCases.GetPlayer;

// Consulta: los datos de un jugador (para nodos de otros módulos, por ejemplo el ataque)
public class GetPlayerHandler(PlayerRepository playerRepository)
{
    public PlayerResult Handle(int playerId)
    {
        var p = playerRepository.Get(playerId);
        return new PlayerResult(p.PlayerId, p.Position, p.Facing, p.Health, p.MaxHealth, p.Armor, p.State);
    }
}
