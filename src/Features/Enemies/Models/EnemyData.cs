using Godot;

namespace BichosKombat.Features.Enemies.Models;

// Un enemigo. Los valores iniciales salen de su EnemyStats (.tres) al hacer Spawn
public class EnemyData(int enemyId, EnemyKind kind)
{
    public int EnemyId { get; } = enemyId;
    public EnemyKind Kind { get; } = kind;

    public Vector2 Position { get; set; }
    public float Speed { get; set; }

    public int MaxHealth { get; set; }
    public int Health { get; set; }

    public int Damage { get; set; }
    public float AttackRange { get; set; }
    public float WindupTime { get; set; }
    public float AttackRate { get; set; }
    public int Experience { get; set; }

    public EnemyState State { get; set; } = EnemyState.Chasing;
    public float StateTimeLeft { get; set; }     // cuánto le queda al estado actual (WindingUp, Recovering, Hurt)
}
