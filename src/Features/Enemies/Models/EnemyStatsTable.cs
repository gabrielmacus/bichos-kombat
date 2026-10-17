using System.Collections.Generic;

namespace BichosKombat.Features.Enemies.Models;

// La tabla de valores por tipo: acá se balancea
public static class EnemyStatsTable
{
    public static readonly Dictionary<EnemyKind, EnemyStats> Stats = new()
    {
        [EnemyKind.Ant] = new EnemyStats(MaxHealth: 20, Speed: 60f, Damage: 10),
        [EnemyKind.Fly] = new EnemyStats(MaxHealth: 10, Speed: 110f, Damage: 5),
        [EnemyKind.Beetle] = new EnemyStats(MaxHealth: 60, Speed: 35f, Damage: 20),
    };
}
