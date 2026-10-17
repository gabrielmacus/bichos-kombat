using BichosKombat.Features.Combat.Models;

namespace BichosKombat.Features.Combat.UseCases.AddFighter;

// Alta del ataque de un jugador (lo llama su AttackController en _Ready)
public class AddFighterHandler(FighterRepository fighterRepository)
{
    public void Handle(int playerId) => fighterRepository.Add(new FighterData(playerId));
}
