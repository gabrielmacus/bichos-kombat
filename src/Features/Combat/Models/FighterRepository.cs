using System.Collections.Generic;

namespace BichosKombat.Features.Combat.Models;

// Un luchador por jugador, por PlayerId
public class FighterRepository
{
    private readonly Dictionary<int, FighterData> _fighters = new();

    public void Add(FighterData fighter) => _fighters[fighter.PlayerId] = fighter;
    public void Remove(int playerId) => _fighters.Remove(playerId);
    public FighterData Get(int playerId) => _fighters[playerId];
    public FighterData? Find(int playerId) => _fighters.GetValueOrDefault(playerId);   // null si no está
}
