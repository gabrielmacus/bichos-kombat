using System.Collections.Generic;
using Godot;

namespace BichosKombat.Features.Enemies.UseCases.Think;

public record ThinkInput(int EnemyId, Vector2 Position, IReadOnlyList<EnemyTarget> Targets, float Delta);
