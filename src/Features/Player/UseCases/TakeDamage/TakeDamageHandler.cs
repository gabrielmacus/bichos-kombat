using System;
using BichosKombat.Features.Player.Models;
using BichosKombat.Shared.IntegrationEvents.Player;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Features.Player.UseCases.TakeDamage;

// El bicho recibe un golpe: la armadura lo reduce, queda un rato en Hurt (sin recibir más daño) o muere
public class TakeDamageHandler(PlayerRepository playerRepository) : IEventHandler<PlayerHitEvent>
{
    public float HurtDuration { get; set; } = 0.5f;

    public void Handle(PlayerHitEvent @event)
    {
        var player = playerRepository.Get(@event.PlayerId);
        if (player.State == PlayerState.Dead || player.HurtTimeLeft > 0) return;

        // 1. La armadura descuenta, pero todo golpe hace al menos 1
        int damage = Math.Max(1, @event.Damage - player.Armor);
        player.Health = Math.Max(0, player.Health - damage);
        EventBus.Publish(new PlayerDamagedEvent(player.PlayerId, damage, player.Health, player.MaxHealth));

        // 2. Muere o queda en Hurt
        if (player.Health == 0)
        {
            player.State = PlayerState.Dead;
            EventBus.Publish(new PlayerDiedEvent(player.PlayerId));
            return;
        }
        player.State = PlayerState.Hurt;
        player.HurtTimeLeft = HurtDuration;
    }
}
