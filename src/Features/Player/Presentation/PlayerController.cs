using Godot;
using BichosKombat.Features.Player.UseCases.Move;
using BichosKombat.Features.Player.UseCases.Spawn;

namespace BichosKombat.Features.Player.Presentation;

// El bicho de un jugador en la escena. No decide nada: lee sus teclas, llama a Move y aplica el resultado
public partial class PlayerController : CharacterBody2D
{
    [Export] public int PlayerId = 1;
    [Export] public Color Color = new(0.4f, 0.9f, 0.4f);

    private PlayerView _view = null!;

    public override void _Ready()
    {
        _view = GetNode<PlayerView>("View");
        _view.Color = Color;
        PlayerModuleExtensions.Spawn.Handle(new SpawnInput(PlayerId, GlobalPosition));
    }

    public override void _ExitTree()
    {
        PlayerModuleExtensions.Despawn.Handle(PlayerId);
    }

    public override void _PhysicsProcess(double delta)
    {
        // 1. Las teclas de este jugador: acciones p1_left, p2_left... (ver [input] en project.godot)
        var direction = Input.GetVector($"p{PlayerId}_left", $"p{PlayerId}_right", $"p{PlayerId}_up", $"p{PlayerId}_down");

        // 2. El Handler decide; el nodo aplica
        var result = PlayerModuleExtensions.Move.Handle(new MoveInput(PlayerId, direction, GlobalPosition, (float)delta));
        Velocity = result.Velocity;
        MoveAndSlide();

        // 3. La vista dibuja el estado
        _view.Render(result.State, result.Facing);
    }
}
