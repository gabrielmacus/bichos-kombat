namespace BichosKombat.Features.Combat.Models;

// El ataque del bicho de un jugador. Las mejoras (nivel, ítems) cambian estos números
public class FighterData(int playerId)
{
    public int PlayerId { get; } = playerId;

    public int Damage { get; set; } = 10;
    public float AttackRate { get; set; } = 2.5f;   // cadencia: golpes por segundo
    public float CooldownLeft { get; set; }         // segundos que faltan para poder volver a pegar
    public float Reach { get; set; } = 24f;         // qué tan adelante del bicho cae el golpe
    public float Radius { get; set; } = 20f;        // tamaño del golpe

    public bool CanAttack { get; set; } = true;     // false cuando el jugador murió
}
