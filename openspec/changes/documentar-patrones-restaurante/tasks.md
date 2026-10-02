# Tareas (implementación futura — en esta etapa NO ejecutar)

> Nota: esta etapa es solo documentación OpenSpec. No modificar ningún `.cs`.

## 1. Decorator
- [ ] Crear `src/DecoradorComida.cs` con `DecoradorComida : Comida` que envuelve una `Comida`.
- [ ] Implementar `ConQueso` (+$2000, `" con queso"`, `"Agregando queso..."`).
- [ ] Implementar `ConTocineta` (+$3000, `" con tocineta"`, `"Agregando tocineta..."`).
- [ ] Implementar `ConPapas` (+$4000, `" con papas"`, `"Agregando papas..."`).
- [ ] Implementar `ConPepperoni` (+$3000, `" con pepperoni"`, `"Agregando pepperoni..."`).
- [ ] Implementar `ConChampinones` (+$2000, `" con champiñones"`, `"Agregando champiñones..."`).
- [ ] Implementar `ConAceitunas` (+$2000, `" con aceitunas"`, `"Agregando aceitunas..."`).
- [ ] Implementar `ConMaiz` (+$2000, `" con maíz"`, `"Agregando maíz..."`).
- [ ] Verificar combinación encadenada, precio acumulativo, nombre acumulativo y `Preparar()` en cadena.
- [ ] Verificar que `Hamburguesa`, `Pizza`, `PerroCaliente` y fábricas no requieran cambios.

## 2. Proxy
- [ ] Crear `src/PedidoProxy.cs` con `Pedido` (contiene `Comida`, procesa: prepara + muestra nombre/precio final + confirmación).
- [ ] Implementar `PedidoProxy` (valida pedido y comida; permite o impide procesamiento con mensajes).
- [ ] Verificar pedido válido se procesa y pedido nulo/vacío se bloquea sin llamar `Preparar()`.

## 3. Facade + Program
- [ ] Crear `src/RestauranteFacade.cs` que coordina: fábrica → `CrearComida()` → decoradores permitidos → `Pedido` → `PedidoProxy` → procesar.
- [ ] Actualizar `src/Program.cs`: menú con precios, adicionales solo del tipo elegido, delegar todo a `RestauranteFacade` (sin crear fábricas/decoradores/pedido/proxy directamente).
- [ ] Verificar ejemplo: Hamburguesa + queso + tocineta + papas → `Hamburguesa con queso con tocineta con papas`, `$24000`, pasos en orden.
- [ ] Verificar que Factory Method sigue funcionando y que no se introdujo ningún patrón extra.
- [ ] Compilar (`dotnet build`) y prueba manual de consola.
