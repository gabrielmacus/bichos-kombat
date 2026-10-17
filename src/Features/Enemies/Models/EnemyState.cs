namespace BichosKombat.Features.Enemies.Models;

// Lo que está haciendo el enemigo; lo decide Think y la vista lo traduce
public enum EnemyState
{
    Chasing,
    Hurt,
    Dead,
}
