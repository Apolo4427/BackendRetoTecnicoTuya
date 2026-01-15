# BackendTuya – Prueba Técnica Backend (.NET 6)

API en ASP.NET Core (.NET 6) con arquitectura hexagonal (Domain / Application / Infrastructure / Presentation),
CQRS con MediatR, EF Core + SQL Server, manejo centralizado de errores y tests unitarios del caso de uso principal
(OrderService).

## Tecnologías
- .NET 6
- ASP.NET Core Web API
- EF Core 6 + SQL Server Provider
- MediatR
- Swagger (Swashbuckle)
- xUnit + Moq (tests)

---

## Arquitectura (alto nivel)
- **Domain**: Entidades `Customer`, `Order`, y contratos `ICustomerRepository`, `IOrderRepository`.
- **Application**: Casos de uso (`OrderService`) + comandos/queries (MediatR) y handlers.
- **Infrastructure**: `AppDbContext` (EF Core) + repositorios concretos.
- **Presentation (API)**: Controllers + Middleware de excepciones.

---

## Requisitos para ejecutar
- .NET SDK 6
- Una instancia de **SQL Server** (puede ser LocalDB/Express/Developer)

---

## Configuración rápida (para que el evaluador lo ejecute sin fricción)
Esta API usa una connection string llamada `DefaultConnection`.

## appsettings.Development.json
Crea/edita `appsettings.Development.json` en la raíz del proyecto con:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BackendTuyaDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
---

## Creación automática de base de datos (sin migraciones)

Para simplificar la ejecución en evaluación, el proyecto está configurado para crear la base automáticamente
al iniciar (con db.Database.EnsureCreated()).

---

## Ejecutar la API

Desde la carpeta del proyecto:

- dotnet restore
- dotnet run


### Swagger UI queda disponible en:

mirar el puerto en consola, ejemplos

- `http://localhost:5000/swagger`

- `https://localhost:5001/swagger`

---

## Endpoints

### Customers
- `GET /customers` → lista de clientes
- `POST /customers` → crea cliente

Body ejemplo (`data/createCustomer.json`):
```json
{ "name": "Ana", "email": "ana@test.com" }
```

### Orders
- `POST /orders` → crea orden
- `POST /orders/{orderId}/cancel` → cancela orden

Body ejemplo (`data/createOrder.json`):
```json
{ "customerId": "<GUID>", "description": "Laptop", "total": 1500 }
```

---

## Manejo de errores
Se incluye `ExceptionHandlingMiddleware` para estandarizar errores con `ProblemDetails`:
- `NotFoundException` → **404**
- `ArgumentException` → **400**
- Otros → **500**

---

Incluye tests unitarios para `OrderService` usando **xUnit** + **Moq**.

---

## Estructura del repo
```
.
├── BackendTuya.csproj
├── Program.cs
├── README.md
├── .gitignore
├── src
│   ├── Api
│   ├── Application
│   ├── Domain
│   └── Infrastructure
├── tests
│   └── BackendTuya.Application.Tests
├── scripts
│   └── http
└── data
```

---

## Notas de diseño (para sustentación)
- Reglas de negocio en el **dominio**: `Customer.CreateOrder(...)` y `Order.Cancel()`.
- Orquestación en **Application**: `OrderService` valida existencia y persiste.
- Infra desacoplada mediante **repositorios** y DI.
- API delgada con **MediatR**: controllers delegan a handlers.

