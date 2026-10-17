using System.Linq;
using Godot;
using BichosKombat.Features.Enemies.Models;
using BichosKombat.Features.Enemies.UseCases.Spawn;
using BichosKombat.Features.Enemies.UseCases.Think;
using BichosKombat.Features.Player;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Enemies.Presentation;

// Un enemigo en la escena. No decide nada: le pasa a Think lo que percibe y aplica el resultado
public partial class EnemyController : CharacterBody2D
{
    [Export] public EnemyStats Stats { get; set; } = null!;

    private EnemyView _view = null!;
    private int _enemyId;

    public override void _Ready()
    {
        _view = GetNode<EnemyView>("View");
        _view.Setup(Stats.Kind, Stats.AttackRange);
        _enemyId = EnemiesModuleExtensions.Spawn.Handle(new SpawnInput(Stats, GlobalPosition));
    }

    public override void _ExitTree()
    {
        EnemiesModuleExtensions.Despawn.Handle(_enemyId);
    }

    public override void _PhysicsProcess(double delta)
    {
        // 1. Lo que percibe: dónde están los jugadores (se lo pregunta a Player)
        var targets = PlayerModuleExtensions.GetPlayers.Handle()
            .Select(p => new EnemyTarget(p.PlayerId, p.Position, p.State != PlayerState.Dead))
            .ToList();

        // 2. Think decide; el nodo aplica
        var result = EnemiesModuleExtensions.Think.Handle(new ThinkInput(_enemyId, GlobalPosition, targets, (float)delta));
        if (result.State == EnemyState.Dead)
        {
            QueueFree();
            return;
        }
        Velocity = result.Velocity;
        MoveAndSlide();

        // 3. La vista dibuja el estado
        _view.Render(result.State);
    }
}
