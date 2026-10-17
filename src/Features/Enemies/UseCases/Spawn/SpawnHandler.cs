using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.UseCases.Spawn;

// Aparece un enemigo (lo llama su nodo en _Ready). Copia los valores de su tipo y devuelve el EnemyId
public class SpawnHandler(EnemyRepository enemyRepository)
{
    public int Handle(SpawnInput input)
    {
        var stats = input.Stats;
        var enemy = new EnemyData(enemyRepository.NextId(), stats.Kind)
        {
            Position = input.Position,
            Speed = stats.Speed,
            MaxHealth = stats.MaxHealth,
            Health = stats.MaxHealth,
            Damage = stats.Damage,
            AttackRange = stats.AttackRange,
            WindupTime = stats.WindupTime,
            AttackRate = stats.AttackRate,
            Experience = stats.Experience,
        };
        enemyRepository.Add(enemy);
        return enemy.EnemyId;
    }
}
