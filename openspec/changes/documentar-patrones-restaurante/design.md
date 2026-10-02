# Diseño: Decorator + Proxy + Facade sobre Factory Method existente

## Contexto y restricciones
- Conservar `Comida`, `Hamburguesa`, `Pizza`, `PerroCaliente`, `FabricaComida`, `FabricaHamburguesa`, `FabricaPizza`, `FabricaPerroCaliente` y su flujo `Program → FabricaComida → Comida concreta`.
- Nuevos archivos únicamente: `src/DecoradorComida.cs`, `src/PedidoProxy.cs`, `src/RestauranteFacade.cs` (+ ajuste mínimo de `Program.cs` en etapa de implementación).
- Namespace `Restaurante`, consola .NET 8, sin dependencias externas.

## 1. Decorator
- `DecoradorComida : Comida`: recibe `Comida` en constructor, la guarda como componente interno; sobrescribe `Nombre`/`Precio`/`Preparar()` delegando al componente y agregando lo propio.
- Decoradores concretos (todos `: DecoradorComida`, constructor `X(Comida comida)`):
  - `ConQueso`: nombre `+ " con queso"`, precio `+2000`, preparación `+ "Agregando queso..."`.
  - `ConTocineta`: `+ " con tocineta"`, `+3000`, `"Agregando tocineta..."`.
  - `ConPapas`: `+ " con papas"`, `+4000`, `"Agregando papas..."`.
  - `ConPepperoni`: `+ " con pepperoni"`, `+3000`, `"Agregando pepperoni..."`.
  - `ConChampinones`: `+ " con champiñones"`, `+2000`, `"Agregando champiñones..."`.
  - `ConAceitunas`: `+ " con aceitunas"`, `+2000`, `"Agregando aceitunas..."`.
  - `ConMaiz`: `+ " con maíz"`, `+2000`, `"Agregando maíz..."`.
- Combinación por envolvimiento:
  `Comida c = new Hamburguesa(); c = new ConQueso(c); c = new ConTocineta(c); c = new ConPapas(c);`
  → nombre `Hamburguesa con queso con tocineta con papas`, precio `15000+2000+3000+4000=24000`.
- `Preparar()` ejecuta primero el componente y luego el paso del decorador, acumulando en cadena.
- Mapeo permitido por fachada: Hamburguesa → Queso/Tocineta/Papas; Pizza → Pepperoni/Champiñones/Aceitunas; Perro → Queso/Tocineta/Maíz.

## 2. Proxy
- `Pedido`: contiene `Comida Comida`; método `ProcesarPedido()` que muestra inicio de procesamiento, llama `Comida.Preparar()`, muestra `Producto: <Nombre>`, `Precio: $<Precio>` y `Pedido preparado correctamente.`
- `PedidoProxy`: contiene `Pedido`; método `ProcesarPedido()` que valida `pedido != null && pedido.Comida != null`. Si válido: muestra validación correcta y delega a `Pedido.ProcesarPedido()`. Si no: impide procesar y muestra mensaje de pedido vacío/no procesable; nunca llama `Preparar()`.

## 3. Facade
- `RestauranteFacade` con método p. ej. `RealizarPedido(string opcionComida, List<string> ingredientes)`:
  1. Recibe opción de comida.
  2. Selecciona `FabricaHamburguesa` / `FabricaPizza` / `FabricaPerroCaliente`.
  3. Crea `Comida` vía `CrearComida()`.
  4. Revisa ingredientes y envuelve con los decoradores permitidos para ese tipo.
  5. Crea `Pedido`.
  6. Crea `PedidoProxy`.
  7. Llama `proxy.ProcesarPedido()`.
- `Program.cs` solo: muestra menú (`1. Hamburguesa - $15000 / 2. Pizza - $18000 / 3. Perro Caliente - $12000`), muestra adicionales del tipo elegido, recoge selección y llama a la fachada. No crea fábricas, decoradores, `Pedido` ni `PedidoProxy` directamente.

## Flujo completo
`Program.cs → RestauranteFacade → Factory Method → Comida concreta → Decorator(s) → Pedido → PedidoProxy → Preparar() → nombre/precio final`.

## Alternativas descartadas
- Subclases por combinación: descartada por explosión de clases; Decorator la evita.
- Validar dentro de `Pedido`: descartado; la validación es responsabilidad del Proxy.
- Que `Program` coordine fábricas/decoradores/pedido: descartado; esa es la responsabilidad de la Facade.
