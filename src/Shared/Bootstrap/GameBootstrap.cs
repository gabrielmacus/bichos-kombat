using Godot;
using BichosKombat.Features.Combat;
using BichosKombat.Features.Player;
using BichosKombat.Shared.Messaging;

namespace BichosKombat.Shared.Bootstrap;

// Punto de entrada (como Program.cs): registra los módulos antes de que arranque cualquier nodo
public partial class GameBootstrap : Node
{
    public override void _EnterTree()
    {
        // Limpieza de suscriptores al recargar la escena
        EventBus.Clear();

        // Registrar acá cada módulo, en el orden en que deben reaccionar a los eventos:
        PlayerModuleExtensions.AddPlayerModule();
        CombatModuleExtensions.AddCombatModule();
    }
}
