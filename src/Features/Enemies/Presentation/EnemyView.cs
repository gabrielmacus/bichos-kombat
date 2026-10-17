using System.Collections.Generic;
using Godot;
using BichosKombat.Features.Enemies.Models;

namespace BichosKombat.Features.Enemies.Presentation;

// Greybox: un cuadrado por tipo. Rojo con el círculo de alcance mientras avisa; blanco al recibir un golpe
public partial class EnemyView : Node2D
{
    private static readonly Dictionary<EnemyKind, (Color Color, float Size)> Looks = new()
    {
        [EnemyKind.Ant] = (new Color(0.45f, 0.25f, 0.15f), 16f),
        [EnemyKind.Fly] = (new Color(0.55f, 0.6f, 0.75f), 12f),
        [EnemyKind.Beetle] = (new Color(0.15f, 0.4f, 0.25f), 26f),
    };
    private static readonly Color WindupColor = new(0.95f, 0.2f, 0.2f);
    private static readonly Color RangeColor = new(0.95f, 0.2f, 0.2f, 0.6f);

    private EnemyKind _kind;
    private float _attackRange;
    private EnemyState _state = EnemyState.Chasing;

    public void Setup(EnemyKind kind, float attackRange)
    {
        _kind = kind;
        _attackRange = attackRange;
    }

    public void Render(EnemyState state)
    {
        _state = state;
        QueueRedraw();
    }

    public override void _Draw()
    {
        var (color, size) = Looks[_kind];
        if (_state == EnemyState.WindingUp)
        {
            color = WindupColor;
            DrawArc(Vector2.Zero, _attackRange, 0, Mathf.Tau, 32, RangeColor, 2);
        }
        else if (_state == EnemyState.Hurt)
        {
            color = Colors.White;
        }
        DrawRect(new Rect2(-size / 2, -size / 2, size, size), color);
    }
}
