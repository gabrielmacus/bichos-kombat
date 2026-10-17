# Instrucciones para agentes de IA

Proyecto Godot 4.7 (.NET) + C# 12 (`net8.0`), namespace raíz `BichosKombat`. La explicación completa está en [`README.md`](README.md). Respondé en español.

Juego: roguelike multijugador de insectos que pelean contra hordas. Cantidad de jugadores y si es local o en red: **sin definir**.

## Forma de trabajar

1. Hacé solo lo pedido; si ves algo más para mejorar, proponelo y esperá aprobación.
2. Solución mínima, que se entienda de una lectura.
3. Nada de `git commit` ni comandos destructivos sin autorización explícita.
4. Verificá las APIs de Godot contra el assembly real (`.godot/mono/temp/bin/Debug/GodotSharp.dll`) antes de entregar.
5. Si no compilaste ni probaste, decilo.
6. Al mover o renombrar un `.cs`, mové también su `.cs.uid`.
7. Si cambia una regla o la estructura, actualizá `README.md` y este archivo.

## Reglas

| # | Regla |
|---|---|
| 1 | El caso de uso es el Handler (`[Acción]Handler`); no hay clases `...UseCase` aparte |
| 2 | Constructor primario en toda clase con dependencias |
| 3 | Los nodos de Godot no deciden: arman un `Input`, llaman al Handler y aplican el `Result` |
| 4 | Una clase o record por archivo, con su nombre exacto; namespace = carpeta |
| 5 | Eventos que escucha **otro módulo**: en `src/Shared/IntegrationEvents/[Módulo]/`, en la carpeta del módulo del que trata el evento (no se cambian sin avisar). Eventos internos: junto al Handler que los produce; si los producen varios casos de uso del módulo, en `[Módulo]/Events/` |
| 6 | Datos en `[Entidad]Data`, colección en `[Entidad]Repository` (nombre de la entidad); enums también en `Models/` |
| 7 | Los Handlers que escuchan eventos implementan `IEventHandler<T>` y se registran con `EventBus.Register` en `[Módulo]ModuleExtensions` |
| 8 | Los Handlers **nunca** usan otros Handlers: entre módulos, solo eventos. Los `Models` y `UseCases` de un módulo nunca hacen `using` de otro módulo, solo de `Shared`. Los nodos de `Presentation/` sí pueden llamar a Handlers y consultas de otros módulos |
| 9 | `EventBus` solo para hechos puntuales, nunca para algo de cada frame |
| 10 | ¿Dura o pasa una vez? Lo que dura lo devuelve el Handler en su `Result` y el nodo lo lee cada frame; lo que pasa una vez es un evento |
| 11 | Los controllers no se suscriben al `EventBus`; solo las vistas, y solo para efectos de un instante |
| 12 | Nodo con datos en un repositorio: alta en `_Ready`, baja en `_ExitTree`. Nodo suscripto: `Subscribe` en `_Ready`, `Unsubscribe` en `_ExitTree` |
| 13 | Valores iniciales con Handlers de consulta (`Get[Algo]Handler`): solo leen y devuelven un valor o un `Result`, nunca la entidad |
| 14 | El flujo de la aplicación (reiniciar, cambiar de escena, pausar) lo maneja `GameBootstrap` |
| 15 | Siempre `using` + nombre corto; nunca nombres completos en el código |
| 16 | Banderas en un repositorio solo para lógica que consume otro Handler; nunca para efectos visuales o de sonido |
| 17 | Multijugador: nunca hay "el jugador". Todo lo que es de un jugador lleva `PlayerId` (datos, `Input`, eventos) y los repositorios guardan a todos los jugadores |
| 18 | El nodo del jugador sabe su `PlayerId`; qué teclas o qué mando usa lo resuelve el nodo, nunca el Handler |
| 19 | Una partida nueva = recargar la escena. Nada de estado de partida en campos `static` fuera de `ModuleExtensions` |

## Antes de entregar

- [ ] Hice solo lo pedido.
- [ ] Cada clase en su archivo, namespace = carpeta, sin nombres completos.
- [ ] Ninguna decisión nueva en nodos de Godot.
- [ ] Handlers nuevos creados en su `ModuleExtensions` (y registrados si escuchan eventos).
- [ ] Toda suscripción o alta tiene su baja en `_ExitTree`.
- [ ] Nada asume un solo jugador: todo lleva `PlayerId`.
- [ ] APIs de Godot verificadas contra `GodotSharp.dll`.
- [ ] README y AGENTS actualizados si cambió alguna regla.
- [ ] Aviso explícito si no compilé ni probé.
