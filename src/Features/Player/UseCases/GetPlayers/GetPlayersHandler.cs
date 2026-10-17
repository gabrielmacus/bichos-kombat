using System.Collections.Generic;
using System.Linq;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.UseCases.GetPlayers;

// Consulta: solo lee (para el HUD y otras vistas al arrancar)
public class GetPlayersHandler(PlayerRepository playerRepository)
{
    public List<PlayerResult> Handle() =>
        playerRepository.All
            .Select(p => new PlayerResult(p.PlayerId, p.Position, p.Facing, p.Health, p.MaxHealth, p.Armor, p.State))
            .ToList();
}
