namespace BichosKombat.Features.Enemies.Models;

// Los tipos de enemigo. Uno nuevo: agregarlo acá y crear su .tres en resources/enemies/
public enum EnemyKind
{
    Ant,        // hormiga: el enemigo normal, va directo
    Fly,        // mosca: rápida y con poca vida
    Beetle,     // escarabajo: lento, mucha vida y mucho daño
}
