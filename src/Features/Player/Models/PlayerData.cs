using Godot;

namespace BichosKombat.Features.Player.Models;

// El cuerpo del bicho de un jugador. Nivel, ataque e ítems son de otros módulos
public class PlayerData(int playerId)
{
    public int PlayerId { get; } = playerId;

    public Vector2 Position { get; set; }
    public Vector2 Facing { get; set; } = Vector2.Down;     // hacia dónde mira (vista desde arriba: cualquier dirección)
    public float Speed { get; set; } = 120f;                // píxeles por segundo

    public int MaxHealth { get; set; } = 100;
    public int Health { get; set; } = 100;
    public int Armor { get; set; }                          // total: lo que sumen ítems y niveles

    public PlayerState State { get; set; } = PlayerState.Idle;
    public float HurtTimeLeft { get; set; }                  // segundos que le quedan de Hurt (mientras tanto no recibe daño)
}
