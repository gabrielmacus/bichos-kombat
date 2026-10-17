using BichosKombat.Features.Enemies.Models;
using BichosKombat.Features.Enemies.UseCases.Despawn;
using BichosKombat.Features.Enemies.UseCases.Spawn;
using BichosKombat.Features.Enemies.UseCases.TakeHits;
using BichosKombat.Features.Enemies.UseCases.Think;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Enemies;

public static class EnemiesModuleExtensions
{
    public static SpawnHandler Spawn { get; private set; } = null!;
    public static DespawnHandler Despawn { get; private set; } = null!;
    public static ThinkHandler Think { get; private set; } = null!;
    public static TakeHitsHandler TakeHits { get; private set; } = null!;

    public static void AddEnemiesModule()
    {
        var repository = new EnemyRepository();
        Spawn = new SpawnHandler(repository);
        Despawn = new DespawnHandler(repository);
        Think = new ThinkHandler(repository);
        TakeHits = new TakeHitsHandler(repository);

        EventBus.Register(TakeHits);
    }
}
