# Especificación: Restaurante — Decorator, Proxy y Facade sobre Factory Method

## Escenario 1: Crear Hamburguesa mediante Factory Method
DADO que existe `FabricaHamburguesa`
CUANDO se llama `CrearComida()`
ENTONCES se obtiene una `Hamburguesa` con `Nombre="Hamburguesa"` y `Precio=15000`.

## Escenario 2: Crear Pizza mediante Factory Method
DADO que existe `FabricaPizza`
CUANDO se llama `CrearComida()`
ENTONCES se obtiene una `Pizza` con `Nombre="Pizza"` y `Precio=18000`.

## Escenario 3: Crear Perro Caliente mediante Factory Method
DADO que existe `FabricaPerroCaliente`
CUANDO se llama `CrearComida()`
ENTONCES se obtiene un `PerroCaliente` con `Nombre="Perro Caliente"` y `Precio=12000`.

## Escenario 4: Agregar ingredientes a Hamburguesa (Decorator)
DADO una `Hamburguesa`
CUANDO se envuelve con `ConQueso`, `ConTocineta` o `ConPapas`
ENTONCES el precio aumenta en 2000 / 3000 / 4000 respectivamente, sin modificar la clase `Hamburguesa`.

## Escenario 5: Agregar ingredientes a Pizza (Decorator)
DADO una `Pizza`
CUANDO se envuelve con `ConPepperoni` (+3000), `ConChampinones` (+2000) o `ConAceitunas` (+2000)
ENTONCES el precio y el nombre reflejan el ingrediente y no se ofrecen queso/tocineta/papas/maíz para Pizza.

## Escenario 6: Agregar ingredientes a Perro Caliente (Decorator)
DADO un `PerroCaliente`
CUANDO se envuelve con `ConQueso` (+2000), `ConTocineta` (+3000) o `ConMaiz` (+2000)
ENTONCES el precio y el nombre reflejan el ingrediente y no se ofrecen pepperoni/champiñones/aceitunas/papas para Perro.

## Escenario 7: Combinar varios decoradores
DADO `Comida c = new Hamburguesa()`
CUANDO `c = new ConQueso(c); c = new ConTocineta(c); c = new ConPapas(c);`
ENTONCES el objeto final conserva todos los comportamientos sin crear clases por combinación.

## Escenario 8: Precio acumulativo correcto
DADO la combinación del escenario 7
CUANDO se lee `Precio`
ENTONCES vale `15000 + 2000 + 3000 + 4000 = 24000`.

## Escenario 9: Nombre final incluye ingredientes
DADO la combinación del escenario 7
CUANDO se lee `Nombre`
ENTONCES es `Hamburguesa con queso con tocineta con papas`.

## Escenario 10: Preparación incluye ingredientes agregados
DADO una comida decorada
CUANDO se llama `Preparar()`
ENTONCES primero se ejecutan los pasos de la comida base y después cada `Agregando ...` del decorador en orden de envolvimiento (p. ej. queso, tocineta, papas).

## Escenario 11: Proxy permite pedido válido
DADO un `Pedido` con una `Comida` válida envuelto en `PedidoProxy`
CUANDO se solicita procesar
ENTONCES el proxy muestra validación correcta, delega a `Pedido`, se prepara la comida y se muestran nombre final, precio final y confirmación.

## Escenario 12: Proxy impide pedido vacío
DADO un `Pedido` nulo o sin `Comida`
CUANDO se solicita procesar vía `PedidoProxy`
ENTONCES se impide el procesamiento, se muestra mensaje de pedido vacío/no procesable y nunca se llama `Preparar()`.

## Escenario 13: Facade coordina el proceso completo
DADO una opción de comida y lista de ingredientes
CUANDO `RestauranteFacade` ejecuta el pedido
ENTONCES selecciona la fábrica, crea con Factory Method, aplica solo decoradores permitidos, crea `Pedido` y `PedidoProxy`, y procesa vía el proxy.

## Escenario 14: Program.cs usa la Facade
DADO la consola iniciada
CUANDO el usuario elige comida y adicionales (Hamburguesa: Queso/Tocineta/Papas; Pizza: Pepperoni/Champiñones/Aceitunas; Perro: Queso/Tocineta/Maíz)
ENTONCES `Program.cs` delega a `RestauranteFacade` y no instancia directamente fábricas, decoradores, `Pedido` ni `PedidoProxy`.

## Escenario 15: Factory Method sigue funcionando
DADO el sistema con Decorator, Proxy y Facade agregados
CUANDO se crea cualquier comida
ENTONCES la creación sigue pasando por `FabricaHamburguesa` / `FabricaPizza` / `FabricaPerroCaliente.CrearComida()` y las clases `Comida`, `Hamburguesa`, `Pizza`, `PerroCaliente` y fábricas se conservan sin rediseño.
