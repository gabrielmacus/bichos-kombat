using BichosKombat.Features.Player.Models;
using BichosKombat.Features.Player.UseCases.Despawn;
using BichosKombat.Features.Player.UseCases.GetPlayer;
using BichosKombat.Features.Player.UseCases.GetPlayers;
using BichosKombat.Features.Player.UseCases.Move;
using BichosKombat.Features.Player.UseCases.Spawn;
using BichosKombat.Features.Player.UseCases.TakeDamage;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Player;

public static class PlayerModuleExtensions
{
    public static SpawnHandler Spawn { get; private set; } = null!;
    public static DespawnHandler Despawn { get; private set; } = null!;
    public static MoveHandler Move { get; private set; } = null!;
    public static TakeDamageHandler TakeDamage { get; private set; } = null!;
    public static GetPlayersHandler GetPlayers { get; private set; } = null!;
    public static GetPlayerHandler GetPlayer { get; private set; } = null!;

    public static void AddPlayerModule()
    {
        var repository = new PlayerRepository();
        Spawn = new SpawnHandler(repository);
        Despawn = new DespawnHandler(repository);
        Move = new MoveHandler(repository);
        TakeDamage = new TakeDamageHandler(repository);
        GetPlayers = new GetPlayersHandler(repository);
        GetPlayer = new GetPlayerHandler(repository);

        EventBus.Register(TakeDamage);
    }
}
