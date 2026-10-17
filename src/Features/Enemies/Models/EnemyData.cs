using Godot;

namespace BichosKombat.Features.Enemies.Models;

// Un enemigo. Los valores iniciales salen de EnemyStatsTable al hacer Spawn
public class EnemyData(int enemyId, EnemyKind kind)
{
    public int EnemyId { get; } = enemyId;
    public EnemyKind Kind { get; } = kind;

    public Vector2 Position { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }

    public int MaxHealth { get; set; }
    public int Health { get; set; }

    public EnemyState State { get; set; } = EnemyState.Chasing;
    public float HurtTimeLeft { get; set; }             // segundos que le quedan de Hurt
    public float AttackCooldownLeft { get; set; }       // para no pegarle al jugador en todos los frames que lo toca
}
