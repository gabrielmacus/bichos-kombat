using Godot;
using BichosKombat.Features.Enemies.Models;
using BichosKombat.Shared.IntegrationEvents.Combat;
using BichosKombat.Shared.IntegrationEvents.Enemies;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Enemies.UseCases.TakeHits;

// Un jugador pegó: los enemigos dentro del golpe pierden vida; los que llegan a 0 mueren
public class TakeHitsHandler(EnemyRepository enemyRepository) : IEventHandler<AttackPerformedEvent>
{
    public float BodyRadius { get; set; } = 10f;        // cuenta como adentro si el golpe toca el cuerpo, no solo el centro
    public float HurtDuration { get; set; } = 0.2f;

    public void Handle(AttackPerformedEvent @event)
    {
        foreach (var enemy in enemyRepository.All)
        {
            if (enemy.State == EnemyState.Dead) continue;
            if (enemy.Position.DistanceTo(@event.Center) > @event.Radius + BodyRadius) continue;

            enemy.Health -= @event.Damage;
            if (enemy.Health <= 0)
            {
                enemy.State = EnemyState.Dead;
                EventBus.Publish(new EnemyDiedEvent(enemy.EnemyId, enemy.Position, enemy.Experience, @event.PlayerId));
            }
            else
            {
                enemy.State = EnemyState.Hurt;     // el golpe le corta el ataque que estaba preparando
                enemy.StateTimeLeft = HurtDuration;
            }
        }
    }
}
