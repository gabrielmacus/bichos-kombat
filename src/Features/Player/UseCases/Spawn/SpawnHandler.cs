using BichosKombat.Features.Player.Models;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Player.UseCases.Spawn;

// Entra un jugador a la partida (lo llama su nodo en _Ready)
public class SpawnHandler(PlayerRepository playerRepository)
{
    public void Handle(SpawnInput input)
    {
        playerRepository.Add(new PlayerData(input.PlayerId) { Position = input.Position });
        EventBus.Publish(new PlayerSpawnedEvent(input.PlayerId));
    }
}
