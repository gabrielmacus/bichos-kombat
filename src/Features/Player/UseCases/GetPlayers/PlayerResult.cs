using Godot;
using BichosKombat.Features.Player.Models;

namespace BichosKombat.Features.Player.UseCases.GetPlayers;

public record PlayerResult(int PlayerId, Vector2 Position, Vector2 Facing, int Health, int MaxHealth, int Armor, PlayerState State);
