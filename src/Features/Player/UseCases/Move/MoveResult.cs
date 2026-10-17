using Godot;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.UseCases.Move;

public record MoveResult(Vector2 Velocity, Vector2 Facing, PlayerState State);
