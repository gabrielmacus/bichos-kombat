using BichosKombat.Features.Combat.Models;
using BichosKombat.Features.Combat.UseCases.AddFighter;
using BichosKombat.Features.Combat.UseCases.Attack;
using BichosKombat.Features.Combat.UseCases.Disarm;
using BichosKombat.Features.Combat.UseCases.RemoveFighter;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Combat;

public static class CombatModuleExtensions
{
    public static AddFighterHandler AddFighter { get; private set; } = null!;
    public static RemoveFighterHandler RemoveFighter { get; private set; } = null!;
    public static AttackHandler Attack { get; private set; } = null!;
    public static DisarmHandler Disarm { get; private set; } = null!;

    public static void AddCombatModule()
    {
        var repository = new FighterRepository();
        AddFighter = new AddFighterHandler(repository);
        RemoveFighter = new RemoveFighterHandler(repository);
        Attack = new AttackHandler(repository);
        Disarm = new DisarmHandler(repository);

        EventBus.Register(Disarm);
    }
}
