# SDD - Sistema de Pedidos para Restaurante

| Campo | Valor |
|---|---|
| Tecnología | C# / .NET 8, aplicación de consola |
| Ubicación del código | `src/` |
| Patrones | Factory Method, Decorator, Proxy, Facade |

---

## 1. Problema

El restaurante necesita un sistema sencillo para crear y procesar pedidos de **hamburguesas, pizzas y perros calientes**. Cada producto tiene un precio base y puede recibir complementos que cambian su nombre y su precio.

Sin un diseño adecuado aparecen tres problemas:

1. **Creación acoplada:** el programa principal tendría que conocer y crear cada clase concreta de comida con `new` y `if/switch`.
2. **Explosión de clases:** si cada combinación de complementos fuera una clase (`HamburguesaConQuesoYTocineta`, etc.), habría decenas de clases imposibles de mantener.
3. **Flujo disperso:** la validación, la personalización y el procesamiento quedarían mezclados en `Program.cs`, que terminaría conociendo todos los detalles internos.

**Objetivo:** permitir crear comidas, agregar complementos de forma flexible, validar el pedido antes de procesarlo y coordinar todo con una interfaz simple.

**Usuario:** una persona que, desde la consola, elige el tipo de comida, selecciona complementos y realiza el pedido.

---

## 2. Requisitos

Cada requisito es verificable y se relaciona con un criterio de aceptación (sección 6).

### 2.1 Funcionales

| ID | Requisito | Criterio |
|---|---|---|
| RF-01 | El sistema debe permitir seleccionar hamburguesa, pizza o perro caliente. | CA-01 |
| RF-02 | El sistema debe crear la comida seleccionada mediante Factory Method. | CA-02 |
| RF-03 | El sistema debe permitir agregar complementos disponibles según el tipo de comida. | CA-03 |
| RF-04 | El sistema debe permitir combinar varios complementos en un mismo producto. | CA-04 |
| RF-05 | El sistema debe actualizar nombre y precio de la comida al agregar cada complemento. | CA-05 |
| RF-06 | El sistema debe validar que exista un pedido con una comida válida antes de procesarlo. | CA-06 |
| RF-07 | El sistema debe procesar el pedido únicamente después de validarlo. | CA-06 |
| RF-08 | El programa principal debe usar `RestauranteFacade` para coordinar el proceso. | CA-07 |
| RF-09 | El sistema debe mostrar un mensaje y no procesar el pedido si la opción de comida no existe. | CA-08 |
| RF-10 | El sistema debe mostrar el producto final y su precio. | CA-09 |

### 2.2 No funcionales

| ID | Requisito |
|---|---|
| RNF-01 | Desarrollado en C# con .NET 8. |
| RNF-02 | Código organizado dentro de la carpeta `src/`. |
| RNF-03 | Se ejecuta como aplicación de consola. |
| RNF-04 | Cada patrón debe poder identificarse por el nombre de sus clases. |
| RNF-05 | Agregar un nuevo tipo de comida o complemento no debe modificar clases existentes (solo agregar nuevas). |

### 2.3 Reglas de negocio (valores de referencia)

**Precios base:** Hamburguesa $18.000 · Pizza $25.000 · Perro caliente $12.000

**Complementos disponibles por comida:**

| Complemento | Precio | Hamburguesa | Pizza | Perro caliente |
|---|---|:---:|:---:|:---:|
| `ConQueso` | $2.000 | ✔ | ✔ | ✔ |
| `ConTocineta` | $2.000 | ✔ | | ✔ |
| `ConPapas` | $2.000 | ✔ | | ✔ |
| `ConPepperoni` | $4.000 | | ✔ | |
| `ConChampinones` | $3.000 | | ✔ | |
| `ConAceitunas` | $2.000 | | ✔ | |
| `ConMaiz` | $2.000 | | ✔ | ✔ |

---

## 3. Patrones seleccionados

Se usa **un patrón creacional y tres estructurales**. Cada uno resuelve un problema concreto de la sección 1.

### 3.1 Factory Method (creacional)

- **Problema que resuelve:** creación acoplada.
- **Aplicación:** `FabricaComida` (creadora abstracta) declara `CrearComida()`. Cada fábrica concreta (`FabricaHamburguesa`, `FabricaPizza`, `FabricaPerroCaliente`) decide qué producto instanciar.
- **Justificación:** el cliente trabaja con la abstracción `Comida` y no conoce las clases concretas. Agregar una comida nueva implica crear una clase producto y su fábrica, sin tocar el código existente (RNF-05).
- **Por qué no Abstract Factory:** no hay familias de productos relacionados; solo un tipo de producto con varias variantes.

### 3.2 Decorator (estructural)

- **Problema que resuelve:** explosión de clases por combinaciones de complementos.
- **Aplicación:** `DecoradorComida` hereda de `Comida` y envuelve otra `Comida`. Cada complemento (`ConQueso`, `ConTocineta`, `ConPapas`, `ConPepperoni`, `ConChampinones`, `ConAceitunas`, `ConMaiz`) añade su texto al nombre y su valor al precio.
- **Justificación:** los complementos se apilan en tiempo de ejecución y en cualquier orden. 7 decoradores cubren todas las combinaciones, en lugar de una clase por combinación. Respeta el principio abierto/cerrado.
- **Por qué no herencia:** generaría una clase por cada combinación posible.

### 3.3 Proxy (estructural)

- **Problema que resuelve:** el procesamiento no debe ejecutarse sin validar.
- **Aplicación:** `Pedido` y `PedidoProxy` comparten la interfaz `IPedido`. `PedidoProxy` verifica que el pedido y la comida no sean nulos antes de delegar en `Pedido.Procesar()`.
- **Justificación:** `Pedido` se limita a procesar y la validación queda separada (responsabilidad única). Al ser un proxy de protección, el cliente no puede saltarse la validación.

### 3.4 Facade (estructural)

- **Problema que resuelve:** flujo disperso en `Program.cs`.
- **Aplicación:** `RestauranteFacade` expone un método simple (por ejemplo, `RealizarPedido(tipo, complementos)`) y coordina internamente: elegir fábrica → crear comida → aplicar decoradores → crear pedido → validar con proxy → procesar.
- **Justificación:** `Program.cs` solo maneja la interacción con el usuario y desconoce los patrones internos, lo que reduce el acoplamiento y facilita las pruebas.

---

## 4. Diseño propuesto

### 4.1 Flujo del sistema

```text
Usuario
  ↓
Program
  ↓
RestauranteFacade
  ↓
Factory Method      → crea la comida
  ↓
Decorator           → agrega complementos
  ↓
Pedido
  ↓
Proxy               → valida
  ↓
Procesamiento del pedido
```

### 4.2 Jerarquía de clases

```text
Comida (abstracta)
├── Hamburguesa
├── Pizza
├── PerroCaliente
└── DecoradorComida (abstracta, contiene una Comida)
    ├── ConQueso
    ├── ConTocineta
    ├── ConPapas
    ├── ConPepperoni
    ├── ConChampinones
    ├── ConAceitunas
    └── ConMaiz

FabricaComida (abstracta)
├── FabricaHamburguesa
├── FabricaPizza
└── FabricaPerroCaliente

IPedido
├── Pedido
└── PedidoProxy (contiene un Pedido)

RestauranteFacade
Program
```

### 4.3 Responsabilidad de las clases

| Clase | Responsabilidad |
|---|---|
| `Comida` | Define `Nombre` y `Precio` del producto base. |
| `Hamburguesa`, `Pizza`, `PerroCaliente` | Productos concretos con su nombre y precio base. |
| `FabricaComida` | Declara el método de creación de productos. |
| `FabricaHamburguesa`, `FabricaPizza`, `FabricaPerroCaliente` | Crean el producto concreto correspondiente. |
| `DecoradorComida` | Envuelve una `Comida` y permite extenderla. |
| `ConQueso`, `ConTocineta`, `ConPapas`, `ConPepperoni`, `ConChampinones`, `ConAceitunas`, `ConMaiz` | Agregan su complemento al nombre y suman su precio. |
| `Pedido` | Representa y procesa el pedido. |
| `PedidoProxy` | Valida el pedido antes de permitir su procesamiento. |
| `RestauranteFacade` | Coordina todo el proceso y ofrece una interfaz simple. |
| `Program` | Interacción con el usuario; usa solo la fachada. |

### 4.4 Estructura de carpetas

```text
openspec/
└── sdd-restaurante.md
src/
├── Program.cs
├── Comidas/        (Comida, Hamburguesa, Pizza, PerroCaliente)
├── Fabricas/       (FabricaComida y fábricas concretas)
├── Decoradores/    (DecoradorComida y complementos)
├── Pedidos/        (IPedido, Pedido, PedidoProxy)
└── Facade/         (RestauranteFacade)
```

---

## 5. Decisiones de diseño

| Decisión | Motivo |
|---|---|
| El decorador hereda de `Comida` | Permite tratar una comida decorada igual que una simple (transparencia). |
| `Pedido` y `PedidoProxy` implementan `IPedido` | El cliente no distingue entre uno y otro; así el proxy puede interponerse. |
| La validación vive en el proxy, no en `Program` | Evita que el flujo dependa de que el usuario "se acuerde" de validar. |
| Los complementos válidos por comida se definen en la fachada | `Program` no necesita conocer las reglas de negocio. |
| Opción inválida → mensaje y no se crea pedido | Cumple RF-09 sin lanzar excepciones al usuario. |

---

## 6. Criterios de aceptación

La solución se considera correcta cuando cumple todos los criterios siguientes.

| ID | Criterio | Verificación |
|---|---|---|
| CA-01 | **Selección de comida.** El usuario puede seleccionar Hamburguesa, Pizza o Perro caliente. | Ejecutar y elegir cada opción; las tres se aceptan. |
| CA-02 | **Creación mediante Factory Method.** Al seleccionar una comida, el sistema crea el producto correspondiente con la fábrica apropiada. | Revisar que `RestauranteFacade` obtenga la comida desde una subclase de `FabricaComida`, sin `new Hamburguesa()` directo. |
| CA-03 | **Agregar complementos.** El usuario puede agregar los complementos disponibles para cada tipo de comida. | Probar cada complemento de la tabla 2.3. |
| CA-04 | **Combinación de complementos.** Se pueden agregar varios complementos al mismo producto. | Agregar 3 complementos a una misma comida; todos aparecen en el nombre. |
| CA-05 | **Actualización del precio.** Al agregar complementos, el precio aumenta según el valor definido para cada uno. | Precio final = precio base + suma de complementos. |
| CA-06 | **Validación mediante Proxy.** El sistema valida que exista un pedido y que contenga una comida antes de procesarlo. | Intentar procesar un pedido sin comida: es rechazado y no se procesa. |
| CA-07 | **Coordinación mediante Facade.** `Program` usa `RestauranteFacade` para coordinar creación, personalización y procesamiento. | `Program.cs` no referencia fábricas, decoradores ni `PedidoProxy`. |
| CA-08 | **Opción inválida.** Si se selecciona una comida inexistente, se muestra "opción no válida" y el pedido no se procesa. | Ingresar una opción fuera del menú. |
| CA-09 | **Resultado final.** Con un pedido válido, el sistema muestra el producto final y su precio. | Comparar la salida con el ejemplo de la sección 7. |

---

## 7. Resultado esperado

El sistema debe permitir realizar un pedido siguiendo el flujo:

**Factory Method → Decorator → Pedido → Proxy**

Todo el proceso es coordinado por `RestauranteFacade`.

**Ejemplo:** hamburguesa con queso, tocineta y papas.

```text
Producto: Hamburguesa con queso con tocineta con papas
Precio: $24000
```

Cálculo: 18.000 (base) + 2.000 (queso) + 2.000 (tocineta) + 2.000 (papas) = **24.000**

**Ejemplo de opción inválida:**

```text
Opción no válida. El pedido no fue procesado.
```
