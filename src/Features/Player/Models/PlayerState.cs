namespace BichosKombat.Features.Player.Models;

// Lo que está haciendo el bicho; lo decide un Handler y la vista lo traduce a una animación
public enum PlayerState
{
    Idle,
    Moving,
    Hurt,
    Dead,
}
