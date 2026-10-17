using Godot;
using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.UseCases.Think;

public record ThinkResult(Vector2 Velocity, EnemyState State);
