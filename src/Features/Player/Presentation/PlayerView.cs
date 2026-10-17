using Godot;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.Presentation;

// Greybox: un cuadrado del color del jugador y una raya hacia donde mira. Solo traduce el estado a colores
public partial class PlayerView : Node2D
{
    public Color Color { get; set; } = Colors.White;

    private PlayerState _state = PlayerState.Idle;
    private Vector2 _facing = Vector2.Down;

    public void Render(PlayerState state, Vector2 facing)
    {
        _state = state;
        _facing = facing;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var color = _state switch
        {
            PlayerState.Hurt => Colors.White,
            PlayerState.Dead => Colors.DimGray,
            _ => Color,
        };
        DrawRect(new Rect2(-12, -12, 24, 24), color);
        DrawLine(Vector2.Zero, _facing * 16, Colors.Black, 3);
    }
}
