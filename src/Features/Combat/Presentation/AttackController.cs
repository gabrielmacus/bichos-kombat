using Godot;
using BichosKombat.Features.Combat.UseCases.Attack;
using BichosKombat.Features.Player;
using BichosKombat.Features.Player.Presentation;

namespace BichosKombat.Features.Combat.Presentation;

// Va dentro de Player.tscn. Lee la tecla de ataque de su jugador, llama a Attack y dibuja el golpe un instante (greybox)
public partial class AttackController : Node2D
{
    private static readonly Color SwingColor = new(1f, 1f, 1f, 0.45f);

    private int _playerId;
    private double _swingTimeLeft;
    private Vector2 _swingCenter;
    private float _swingRadius;

    public override void _Ready()
    {
        _playerId = GetParent<PlayerController>().PlayerId;
        CombatModuleExtensions.AddFighter.Handle(_playerId);
    }

    public override void _ExitTree()
    {
        CombatModuleExtensions.RemoveFighter.Handle(_playerId);
    }

    public override void _PhysicsProcess(double delta)
    {
        // 1. Hacia dónde mira el bicho: se lo pregunta a Player (Combat no guarda una copia)
        var player = PlayerModuleExtensions.GetPlayer.Handle(_playerId);

        // 2. Mantener apretado = pega cada vez que termina el cooldown
        var pressed = Input.IsActionPressed($"p{_playerId}_attack");
        var result = CombatModuleExtensions.Attack.Handle(new AttackInput(_playerId, pressed, GlobalPosition, player.Facing, (float)delta));

        // 3. Dibujar el golpe un instante
        if (result.Attacked)
        {
            _swingTimeLeft = 0.1;
            _swingCenter = result.Center;
            _swingRadius = result.Radius;
        }
        _swingTimeLeft -= delta;
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (_swingTimeLeft > 0) DrawCircle(ToLocal(_swingCenter), _swingRadius, SwingColor);
    }
}
