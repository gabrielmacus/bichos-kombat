using System.Collections.Generic;

namespace BichosKombat.Features.Enemies.Models;

// Todos los enemigos vivos, por EnemyId. El repositorio reparte los IDs
public class EnemyRepository
{
    private readonly Dictionary<int, EnemyData> _enemies = new();
    private int _nextId = 1;

    public int NextId() => _nextId++;
    public void Add(EnemyData enemy) => _enemies[enemy.EnemyId] = enemy;
    public void Remove(int enemyId) => _enemies.Remove(enemyId);
    public EnemyData Get(int enemyId) => _enemies[enemyId];
    public IEnumerable<EnemyData> All => _enemies.Values;
}
