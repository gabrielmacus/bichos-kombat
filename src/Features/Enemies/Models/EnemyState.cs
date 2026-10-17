namespace BichosKombat.Features.Enemies.Models;

// Lo que está haciendo el enemigo; lo decide Think y la vista lo traduce
public enum EnemyState
{
    Chasing,      // va hacia el jugador más cercano
    WindingUp,    // frenado, avisando que va a pegar
    Recovering,   // acaba de pegar, espera antes de volver a perseguir
    Hurt,         // recibió un golpe
    Dead,
}
