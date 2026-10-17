using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.UseCases.Despawn;

// Sale un enemigo (lo llama su nodo en _ExitTree)
public class DespawnHandler(EnemyRepository enemyRepository)
{
    public void Handle(int enemyId) => enemyRepository.Remove(enemyId);
}
