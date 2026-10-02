# Propuesta: Agregar Decorator, Proxy y Facade al Restaurante (conservando Factory Method)

## Estado inicial
Aplicación de consola C# .NET 8, namespace `Restaurante`.
Factory Method funcional en `src/`:
- Producto abstracto: `Comida` (`Nombre`, `Precio`, `Preparar()`).
- Productos concretos: `Hamburguesa` ($15000), `Pizza` ($18000), `PerroCaliente` ($12000).
- Creador abstracto: `FabricaComida.CrearComida()`.
- Creadores concretos: `FabricaHamburguesa`, `FabricaPizza`, `FabricaPerroCaliente`.
- Cliente: `Program.cs` elige fábrica con `if/else`, crea con `CrearComida()` y llama `Preparar()`.

## Objetivo
Evolucionar el proyecto agregando exactamente tres patrones estructurales/de acceso, integrados con Factory Method, sin reemplazarlo:
1. **Decorator** — ingredientes adicionales combinables.
2. **Proxy** — validación del pedido antes de procesarlo.
3. **Facade** — interfaz sencilla que coordina todo desde `Program.cs`.

Resultado esperado: seleccionar comida → crearla por Factory Method → decorarla → crear `Pedido` → validar/procesar vía `PedidoProxy` → mostrar nombre final, precio final y pasos de preparación.

## Alcance
- `src/DecoradorComida.cs`: base `DecoradorComida : Comida` + 7 decoradores: `ConQueso`, `ConTocineta`, `ConPapas`, `ConPepperoni`, `ConChampinones`, `ConAceitunas`, `ConMaiz`.
- `src/PedidoProxy.cs`: `Pedido` + `PedidoProxy`.
- `src/RestauranteFacade.cs`: `RestauranteFacade`.
- `src/Program.cs`: usar solo la fachada; mostrar menú y adicionales por tipo de comida.
- No se tocan innecesariamente `Comida`, `Hamburguesa`, `Pizza`, `PerroCaliente`, `FabricaComida.cs`.

## No objetivos (Non-goals)
- No eliminar ni rediseñar Factory Method; no crear Abstract Factory, Singleton, Builder, Adapter, Composite, Strategy, Observer, Repository ni DI.
- No cambiar a web/API, no agregar DB, persistencia, auth, usuarios, inventario, pagos ni librerías externas.
- No cambiar .NET 8 ni namespace `Restaurante`.
- No crear clases por combinación (`HamburguesaConQueso`, etc.).
- En esta etapa solo documentación OpenSpec; sin modificar `.cs`.

## Ingredientes por comida (restricción estricta)
- Hamburguesa: `ConQueso` (+$2000), `ConTocineta` (+$3000), `ConPapas` (+$4000).
- Pizza: `ConPepperoni` (+$3000), `ConChampinones` (+$2000), `ConAceitunas` (+$2000).
- Perro Caliente: `ConQueso` (+$2000), `ConTocineta` (+$3000), `ConMaiz` (+$2000).
- `ConQueso` y `ConTocineta` se reutilizan en Hamburguesa y Perro porque operan sobre `Comida`.

## Éxito
Los 15 escenarios de `specs/restaurante/spec.md` se cumplen y Factory Method sigue funcionando tras integrar los tres patrones.
