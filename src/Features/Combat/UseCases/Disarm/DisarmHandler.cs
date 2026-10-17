using BichosKombat.Features.Combat.Models;
using BichosKombat.Shared.IntegrationEvents.Player;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Combat.UseCases.Disarm;

// Murió el jugador: deja de poder atacar
public class DisarmHandler(FighterRepository fighterRepository) : IEventHandler<PlayerDiedEvent>
{
    public void Handle(PlayerDiedEvent @event)
    {
        var fighter = fighterRepository.Find(@event.PlayerId);
        if (fighter != null) fighter.CanAttack = false;
    }
}
