using System.Collections.Generic;
using System.Linq;
using Godot;
using BichosKombat.Features.Enemies.Models;
using BichosKombat.Shared.IntegrationEvents.Player;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Enemies.UseCases.Think;

// La IA del enemigo, cada frame: perseguir al jugador vivo más cercano, frenar y avisar, pegar, esperar
public class ThinkHandler(EnemyRepository enemyRepository)
{
    public ThinkResult Handle(ThinkInput input)
    {
        var enemy = enemyRepository.Get(input.EnemyId);
        enemy.Position = input.Position;
        if (enemy.State == EnemyState.Dead) return new ThinkResult(Vector2.Zero, EnemyState.Dead);

        enemy.StateTimeLeft -= input.Delta;
        var alive = input.Targets.Where(t => t.Alive).ToList();

        switch (enemy.State)
        {
            case EnemyState.Hurt:
            case EnemyState.Recovering:
                if (enemy.StateTimeLeft <= 0) enemy.State = EnemyState.Chasing;
                return new ThinkResult(Vector2.Zero, enemy.State);

            case EnemyState.WindingUp:
                if (enemy.StateTimeLeft > 0) return new ThinkResult(Vector2.Zero, enemy.State);
                Strike(enemy, alive);
                enemy.State = EnemyState.Recovering;
                enemy.StateTimeLeft = 1f / enemy.AttackRate;
                return new ThinkResult(Vector2.Zero, enemy.State);

            default:
                return Chase(enemy, alive);
        }
    }

    // Va hacia el más cercano; si lo tiene al alcance, frena y empieza a avisar
    private static ThinkResult Chase(EnemyData enemy, List<EnemyTarget> alive)
    {
        if (alive.Count == 0) return new ThinkResult(Vector2.Zero, EnemyState.Chasing);

        var nearest = alive.MinBy(t => enemy.Position.DistanceTo(t.Position))!;
        if (enemy.Position.DistanceTo(nearest.Position) <= enemy.AttackRange)
        {
            enemy.State = EnemyState.WindingUp;
            enemy.StateTimeLeft = enemy.WindupTime;
            return new ThinkResult(Vector2.Zero, enemy.State);
        }
        return new ThinkResult(enemy.Position.DirectionTo(nearest.Position) * enemy.Speed, EnemyState.Chasing);
    }

    // Pega a todos los que sigan al alcance (el que se alejó a tiempo, lo esquivó)
    private static void Strike(EnemyData enemy, List<EnemyTarget> alive)
    {
        foreach (var target in alive.Where(t => enemy.Position.DistanceTo(t.Position) <= enemy.AttackRange))
        {
            EventBus.Publish(new PlayerHitEvent(target.PlayerId, enemy.Damage));
        }
    }
}
