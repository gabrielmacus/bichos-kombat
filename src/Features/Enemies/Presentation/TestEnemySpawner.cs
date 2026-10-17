using System;
using Godot;
using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.Presentation;

// PROVISORIO, solo para probar: cada tanto tira un enemigo al azar en un círculo alrededor del centro.
// Lo reemplaza el módulo Horde (que va a decidir cuántos y de qué tipo)
public partial class TestEnemySpawner : Node2D
{
    [Export] public float Interval = 1.5f;
    [Export] public int MaxEnemies = 12;
    [Export] public float Distance = 380f;

    private PackedScene _enemyScene = null!;
    private EnemyStats[] _kinds = null!;
    private double _timeLeft;

    public override void _Ready()
    {
        _enemyScene = GD.Load<PackedScene>("res://scenes/Enemy.tscn");
        _kinds =
        [
            GD.Load<EnemyStats>("res://resources/enemies/ant.tres"),
            GD.Load<EnemyStats>("res://resources/enemies/fly.tres"),
            GD.Load<EnemyStats>("res://resources/enemies/beetle.tres"),
        ];
    }

    public override void _Process(double delta)
    {
        _timeLeft -= delta;
        if (_timeLeft > 0 || GetChildCount() >= MaxEnemies) return;
        _timeLeft = Interval;

        var enemy = _enemyScene.Instantiate<EnemyController>();
        enemy.Stats = _kinds[Random.Shared.Next(_kinds.Length)];
        enemy.Position = Vector2.FromAngle(Random.Shared.NextSingle() * Mathf.Tau) * Distance;
        AddChild(enemy);
    }
}
