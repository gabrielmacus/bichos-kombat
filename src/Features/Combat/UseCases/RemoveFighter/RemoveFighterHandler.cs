using BichosKombat.Features.Combat.Models;

namespace BichosKombat.Features.Combat.UseCases.RemoveFighter;

// Baja del ataque de un jugador (lo llama su AttackController en _ExitTree)
public class RemoveFighterHandler(FighterRepository fighterRepository)
{
    public void Handle(int playerId) => fighterRepository.Remove(playerId);
}
