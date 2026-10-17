using Godot;
using BichosKombat.Features.Combat.Models;
using BichosKombat.Shared.IntegrationEvents.Combat;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Combat.UseCases.Attack;

// Cada frame: descuenta el cooldown y, si se apretó la tecla y se puede, pega hacia donde mira el bicho
public class AttackHandler(FighterRepository fighterRepository)
{
    public AttackResult Handle(AttackInput input)
    {
        var fighter = fighterRepository.Get(input.PlayerId);

        // 1. Espera entre golpes
        if (fighter.CooldownLeft > 0) fighter.CooldownLeft -= input.Delta;

        // 2. ¿Pega?
        if (!input.Pressed || !fighter.CanAttack || fighter.CooldownLeft > 0) return new AttackResult(false, Vector2.Zero, 0);

        // 3. El golpe cae adelante del bicho
        fighter.CooldownLeft = 1f / fighter.AttackRate;
        var center = input.Position + input.Facing.Normalized() * fighter.Reach;
        EventBus.Publish(new AttackPerformedEvent(fighter.PlayerId, center, fighter.Radius, fighter.Damage));
        return new AttackResult(true, center, fighter.Radius);
    }
}
