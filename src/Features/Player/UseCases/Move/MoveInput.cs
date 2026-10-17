using Godot;

namespace BichosKombat.Features.Player.UseCases.Move;

// Direction: lo que pide el jugador (largo 0 a 1). Position: dónde lo dejó Godot en el frame anterior
public record MoveInput(int PlayerId, Vector2 Direction, Vector2 Position, float Delta);
