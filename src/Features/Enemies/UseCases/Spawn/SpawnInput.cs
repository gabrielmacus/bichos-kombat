using Godot;
using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.UseCases.Spawn;

public record SpawnInput(EnemyStats Stats, Vector2 Position);
