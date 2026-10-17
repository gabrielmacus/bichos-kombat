# Bichos Kombat

Roguelike multijugador de insectos que pelean, hecho en la **Paraná Game Jam 2026** con **Godot 4.7 (.NET) + C#**.

Varios jugadores, cada uno con su bicho, aguantan hordas de enemigos que se vuelven cada vez más fuertes y suben de nivel en cada partida.

Por ahora el proyecto trae solo la infraestructura: un bus de eventos y un punto de entrada. Los módulos del juego se definen en equipo.

> Si sos un agente de IA, leé también [`AGENTS.md`](AGENTS.md).

## Cómo correrlo

Abrir `project.godot` con Godot 4.7 (.NET) y presionar **F5**. Requiere el SDK de .NET 8.

Controles de prueba: **jugador 1** con WASD y ataca con Espacio; **jugador 2** con las flechas y ataca con Enter. Las acciones se llaman `p1_left`, `p2_up`, etc. (`[input]` en `project.godot`); para un jugador 3 se agregan `p3_*` y otra instancia de `scenes/Player.tscn` con `PlayerId = 3`.

## Módulos

| Módulo | Qué hace hoy |
|---|---|
| **Player** | Cuerpo del bicho de cada jugador: moverse (`Move`), entrar y salir (`Spawn`/`Despawn`), recibir daño con armadura (`TakeDamage`, escucha `PlayerHitEvent`). Greybox: cuadrado de color con una raya hacia donde mira; blanco = herido, gris = muerto |
| **Combat** | Ataque con tecla hacia donde mira el bicho, con cadencia `AttackRate` en golpes por segundo (`Attack`). Publica `AttackPerformedEvent` (círculo de golpe + daño); al morir el jugador deja de atacar (`Disarm`). Greybox: círculo blanco un instante |

## La idea

El código del juego se organiza como un backend:

- **Módulos** (`src/Features/[Módulo]/`): un tema del juego con sus propios datos y sus propias reglas.
- **Casos de uso** (`Handlers`): cada acción o regla es una clase. Por ejemplo, `CollectTaxesHandler`.
- **Eventos**: cuando algo pasa, el módulo lo avisa por el `EventBus`. Los demás deciden si les importa. Quien publica no conoce a quien escucha.
- **Godot es la vista**: los nodos capturan entradas (teclado, clics, colisiones), llaman a los casos de uso y muestran el resultado.

| En este proyecto | En un backend |
|---|---|
| `[Entidad]Data` | Entidad / fila |
| `[Entidad]Repository` | Repositorio / `DbSet` |
| `[Acción]Handler` | Caso de uso |
| `[Algo]Input` / `[Algo]Result` | DTO de entrada / salida |
| `[Hecho]Event` | Evento de dominio |
| `[Módulo]ModuleExtensions` | Registro en DI (`services.AddXxx()`) |
| `GameBootstrap` | `Program.cs` |
| Nodo de Godot | Controller / vista |

### ¿Qué es un módulo?

Algo es un módulo si responde que sí a tres preguntas:

1. ¿Tiene **datos propios**?
2. ¿Tiene **reglas propias**?
3. ¿Lo puede programar **una persona sin tocar el código de otra**?

Las variantes de una misma cosa van dentro del mismo módulo: todos los enemigos en uno, todas las mejoras en otro.

## Estructura

```text
bichos-kombat/
├── project.godot
├── scenes/
│   └── Main.tscn                         <-- escena principal (tiene el GameBootstrap)
└── src/
    ├── Shared/
    │   ├── Messaging/                    <-- EventBus, IEvent, IEventHandler
    │   ├── IntegrationEvents/            <-- eventos que se mandan ENTRE módulos (el contrato del equipo)
    │   │   └── [Módulo]/                 <-- una carpeta por módulo: el del tema del evento
    │   └── Bootstrap/GameBootstrap.cs    <-- registra los módulos
    └── Features/
        └── [Módulo]/
            ├── [Módulo]ModuleExtensions.cs   <-- Add[Módulo]Module(): crea repositorio y Handlers
            ├── Models/                       <-- entidades, repositorios y enums del módulo
            ├── UseCases/
            │   └── [Acción]/                 <-- una carpeta por caso de uso
            │       ├── [Acción]Handler.cs
            │       ├── [Algo]Input.cs        <-- opcional
            │       └── [Hecho]Event.cs       <-- eventos que produce este caso de uso
            ├── Events/                       <-- eventos internos que producen VARIOS casos de uso del módulo
            └── Presentation/                 <-- nodos de Godot (Controller, View)
```

El namespace sigue a la carpeta: `src/Features/Economy/UseCases/CollectTaxes/` → `BichosKombat.Features.Economy.UseCases.CollectTaxes`.

## Las piezas, con un ejemplo

```csharp
// Models: los datos
public class BudgetData { public int Money { get; set; } = 1000; }

// UseCases/CollectTaxes: la regla, y el aviso de lo que pasó
public class CollectTaxesHandler(BudgetRepository budgets) : IEventHandler<MonthPassedEvent>
{
    public void Handle(MonthPassedEvent @event)
    {
        var budget = budgets.Get();
        budget.Money += 100;
        EventBus.Publish(new BudgetChangedEvent(budget.Money));
    }
}

// EconomyModuleExtensions: el registro
public static void AddEconomyModule()
{
    var budgets = new BudgetRepository();
    CollectTaxes = new CollectTaxesHandler(budgets);
    EventBus.Register(CollectTaxes);
}

// GameBootstrap._EnterTree(): se registran todos los módulos
EconomyModuleExtensions.AddEconomyModule();
```

## Dos formas de comunicarse

| | Llamada directa (1 a 1) | Evento (1 a N) |
|---|---|---|
| Cuándo | Un nodo necesita que se aplique una regla | Pasó algo y a otros les puede interesar |
| Cómo | `EconomyModuleExtensions.Build.Handle(...)` | `EventBus.Publish(new BudgetChangedEvent(...))` |

**Los Handlers nunca llaman a otros Handlers**: entre módulos se habla solo por eventos.

### Eventos de integración

`src/Shared/IntegrationEvents/` es la API pública de los módulos: lo que se avisan entre ellos.

- **Si lo escucha otro módulo**, el evento va en `IntegrationEvents/[Módulo]/`, en la carpeta del módulo del que **trata** el evento: `PlayerHitEvent` va en `Player/` aunque lo publique Enemies. Namespace: `BichosKombat.Shared.IntegrationEvents.Player`.
- El responsable de cada carpeta es quien programa ese módulo: para cambiar un evento, se le pregunta a él.
- **Si es interno**, va en el módulo, junto al Handler que lo produce.
- Un módulo puede escuchar un evento aunque el módulo que lo publica todavía no exista: se acuerda el evento primero y cada uno programa por su lado.
- Los eventos de esa carpeta **no se cambian sin avisar** al equipo: alguien más depende de ellos.
- Los `Models` y `UseCases` de un módulo nunca hacen `using` de otro módulo, solo de `Shared`. Los nodos (`Presentation/`) sí pueden usar Handlers y consultas de otros módulos: por ejemplo, el ataque le pregunta a Player hacia dónde mira el bicho.

## ¿Estado o evento?

Para cualquier cosa que tenga que llegar a la pantalla, la pregunta es: **¿dura o pasa una vez?**

| | Dura (estado) | Pasa una vez (evento) |
|---|---|---|
| Ejemplos | Un personaje corriendo, un edificio en obra | Se construyó algo, entró plata, hubo un incendio |
| Quién lo decide | El Handler, que lo devuelve en su `Result` | El Handler, que publica el evento |
| Cómo llega a la pantalla | El nodo lo lee en cada frame | La vista lo escucha |

- Un número que cambia de vez en cuando (plata, población) puede ir por evento. El valor inicial se pide una vez con un Handler de consulta (`GetBudgetHandler`), que solo lee.
- **El `EventBus` es solo para hechos puntuales**, nunca para algo que pasa en cada frame.

## Reglas de ciclo de vida

Godot llama a `_ExitTree()` siempre que un nodo sale de la escena. Por eso:

| Si el nodo... | En `_Ready` | En `_ExitTree` |
|---|---|---|
| tiene datos en un repositorio | se da de alta | se da de baja |
| escucha eventos (solo vistas) | `EventBus.Subscribe(...)` | `EventBus.Unsubscribe(...)` |

## Multijugador

Todavía no está decidido si se juega en la misma PC o en red. Para que el código sirva en los dos casos:

- **Nunca hay "el jugador"**: todo lo que es de un jugador lleva su `PlayerId` (datos, `Input`, eventos).
- **Un repositorio guarda a todos los jugadores** (`PlayerRepository` con un diccionario por `PlayerId`), nunca uno solo.
- **El nodo del jugador sabe su `PlayerId`** y lo manda en cada `Input`. Qué teclas o qué mando usa cada uno lo resuelve el nodo, no el Handler.
- Si después se juega en red, los Handlers corren en la PC que hace de servidor y las demás solo muestran. Por eso es clave que las reglas no estén en los nodos.

## Una partida (run)

- Cada partida nueva es una recarga de la escena: `GameBootstrap` vuelve a crear los módulos y los repositorios arrancan vacíos.
- Lo que tiene que sobrevivir entre partidas (desbloqueos, récords) no va en un repositorio común: se guarda aparte, cuando se defina.

## Convenciones

- Una clase o record por archivo, con su nombre exacto.
- Constructores primarios en las clases con dependencias.
- Siempre `using` + nombre corto, nunca nombres completos en el código.
- El repositorio lleva el nombre de la entidad que guarda (`BudgetRepository` guarda `BudgetData`).
- Los eventos y Handlers sobre una entidad llevan su ID (`PlayerId`, `EnemyId`...).
- Si un evento lo escuchan varios módulos y el orden importa, el orden es el de registro en `GameBootstrap`.

## Recetas

**Un caso de uso nuevo:** crear `UseCases/[Acción]/[Acción]Handler.cs`, crearlo en `[Módulo]ModuleExtensions` y llamarlo desde el nodo que corresponda (o registrarlo con `EventBus.Register` si reacciona a un evento).

**Un módulo nuevo:** crear `src/Features/[Módulo]/` con lo que haga falta, su `[Módulo]ModuleExtensions.Add[Módulo]Module()`, y llamarlo desde `GameBootstrap._EnterTree()`.

**Trabajo en equipo:** una persona por módulo y una escena por persona (los `.tscn` dan conflictos feos en git).

## Ideas anotadas para más adelante

**IA de los enemigos más compleja.** Toda la decisión está en un solo caso de uso, `Think`: entra lo que el enemigo percibe (su posición, la de los jugadores) y sale lo que decide (velocidad, estado, si dispara). Mientras ese contrato no cambie, adentro puede crecer:

1. Hoy: un `switch` sobre `Kind`.
2. Comportamientos distintos: una clase por tipo dentro de Enemies (`AntBehavior`, `FlyBehavior`...) y `Think` elige cuál usar.
3. Estados (patrullar, perseguir, huir, cargar): más valores en `EnemyState` y temporizadores en `EnemyData`.
4. Esquivar obstáculos: el nodo usa `NavigationAgent2D` y le pasa a `Think` el próximo punto del camino.

**Enemigos que disparan (módulo Projectiles).**

1. `Think` devuelve además una `ShootDirection` (cero = no dispara); la cadencia es del enemigo (`ShootCooldownLeft` en `EnemyData`).
2. `EnemyController` ve la dirección e instancia `Projectile.tscn` con posición, dirección, daño y bando.
3. Projectiles: `Spawn`/`Despawn` (alta y baja del nodo), `Fly` (cada frame: velocidad y si venció), `Hit` (tocó a un jugador: publica `PlayerHitEvent`).
4. `ProjectileData` lleva `Team`: los de enemigos le pegan a jugadores (`PlayerHitEvent`); los de jugadores, a enemigos (publican `AttackPerformedEvent` con un círculo chico, y Enemies lo resuelve como cualquier golpe).

Player no cambia: le da igual si el golpe viene de una hormiga o de una bala, a los dos los recibe como `PlayerHitEvent`.
