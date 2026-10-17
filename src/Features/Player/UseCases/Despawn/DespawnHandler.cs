using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.UseCases.Despawn;

// Sale un jugador de la partida (lo llama su nodo en _ExitTree)
public class DespawnHandler(PlayerRepository playerRepository)
{
    public void Handle(int playerId) => playerRepository.Remove(playerId);
}
