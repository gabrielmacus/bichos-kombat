using Godot;

namespace BichosKombat.Features.Enemies.Models;

// Los valores base de un tipo de enemigo. Un archivo .tres por tipo (resources/enemies/), editable desde el inspector
[GlobalClass]
public partial class EnemyStats : Resource
{
    [Export] public EnemyKind Kind { get; set; } = EnemyKind.Ant;
    [Export] public int MaxHealth { get; set; } = 20;
    [Export] public float Speed { get; set; } = 60f;

    [Export] public int Damage { get; set; } = 10;
    [Export] public float AttackRange { get; set; } = 30f;     // a qué distancia pega
    [Export] public float WindupTime { get; set; } = 0.5f;     // cuánto avisa antes de pegar (segundos)
    [Export] public float AttackRate { get; set; } = 1f;       // cadencia: ataques por segundo

    [Export] public int Experience { get; set; } = 1;          // lo que da al morir
}
