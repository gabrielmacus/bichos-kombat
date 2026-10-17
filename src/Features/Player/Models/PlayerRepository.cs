using System.Collections.Generic;

namespace BichosKombat.Features.Player.Models;

// Todos los jugadores de la partida, por PlayerId (nunca "el jugador")
public class PlayerRepository
{
    private readonly Dictionary<int, PlayerData> _players = new();

    public void Add(PlayerData player) => _players[player.PlayerId] = player;
    public void Remove(int playerId) => _players.Remove(playerId);
    public PlayerData Get(int playerId) => _players[playerId];
    public IEnumerable<PlayerData> All => _players.Values;
}
