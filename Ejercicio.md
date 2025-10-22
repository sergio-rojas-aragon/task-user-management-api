# 🧩 ERPFlow – Sistema de Gestión de Pedidos, Tareas y Usuarios

## 🎯 Descripción General

**ERPFlow** es un proyecto de práctica full-stack que integra un **backend .NET 8 Web API** con **Entity Framework Core**, **SQLite** y un **frontend Angular**, implementando autenticación **JWT (Bearer)** y un modelo de datos completo para gestionar:

- Usuarios y roles  
- Tareas con estados  
- Clientes  
- Productos  
- Pedidos y detalles de pedido  

El objetivo es dominar **EF Core**, **autenticación**, **relaciones entre entidades** y **consultas avanzadas** mediante informes complejos.

---

## ⚙️ Backend (.NET 8 + EF Core + SQLite)

### 🧱 Modelado de Datos

#### 1. **Roles**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdRol` | INT, PK | Identificador único del rol |
| `Nombre` | VARCHAR | Ej: “Administrador”, “Empleado”, “Vendedor” |

- Un **rol** puede estar asociado a muchos **usuarios**.

---

#### 2. **Usuarios**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdUsuario` | INT, PK | Identificador del usuario |
| `Nombre` | VARCHAR | Nombre completo |
| `Email` | VARCHAR (único) | Correo electrónico |
| `PasswordHash` | VARCHAR | Contraseña hasheada |
| `IdRol` | INT, FK → `Roles(IdRol)` | Rol asignado |

- Un usuario pertenece a un **rol**.  
- Puede gestionar **pedidos** y tener **tareas** asignadas.

---

#### 3. **EstadosTarea**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdEstado` | INT, PK | Identificador del estado |
| `Nombre` | VARCHAR | Ej: “Pendiente”, “En Proceso”, “Completada”, “Cancelada” |

- Un estado puede aplicarse a muchas **tareas**.

---

#### 4. **Tareas**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdTarea` | INT, PK | Identificador de la tarea |
| `Titulo` | VARCHAR | Título |
| `Descripcion` | TEXT | Descripción |
| `FechaCreacion` | DATETIME | Fecha de creación |
| `IdUsuario` | INT, FK → `Usuarios(IdUsuario)` | Usuario asignado |
| `IdEstado` | INT, FK → `EstadosTarea(IdEstado)` | Estado actual |
| `IdCliente` | INT, FK → `Clientes(IdCliente)` (nullable) | Cliente asociado |
| `IdPedido` | INT, FK → `Pedidos(IdPedido)` (nullable) | Pedido asociado |

- Una tarea pertenece a un usuario.  
- Puede estar vinculada opcionalmente a un cliente o pedido.

---

#### 5. **Productos**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdProducto` | INT, PK | Identificador |
| `Nombre` | VARCHAR | Nombre |
| `Categoria` | VARCHAR | Categoría |
| `Precio` | DECIMAL | Precio |
| `FechaCreacion` | DATETIME | Fecha de creación |
| `FechaActualizacion` | DATETIME | Última actualización |

- Un producto puede figurar en muchos **detalles de pedido**.

---

#### 6. **Clientes**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdCliente` | INT, PK | Identificador |
| `Nombre` | VARCHAR | Nombre o razón social |
| `Email` | VARCHAR | Correo del cliente |

- Un cliente puede tener muchos **pedidos** y **tareas** asociadas.

---

#### 7. **Pedidos**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdPedido` | INT, PK | Identificador |
| `IdCliente` | INT, FK → `Clientes(IdCliente)` | Cliente asociado |
| `IdUsuario` | INT, FK → `Usuarios(IdUsuario)` | Usuario que gestiona |
| `Fecha` | DATETIME | Fecha del pedido |

- Un pedido pertenece a un cliente y usuario.  
- Tiene múltiples **detalles** y puede tener **tareas**.

---

#### 8. **Detalles_Pedido**
| Campo | Tipo | Descripción |
|--------|------|-------------|
| `IdDetalle` | INT, PK | Identificador |
| `IdPedido` | INT, FK → `Pedidos(IdPedido)` | Pedido asociado |
| `IdProducto` | INT, FK → `Productos(IdProducto)` | Producto asociado |
| `Cantidad` | INT | Cantidad pedida |

- Relación N:M entre **Pedidos** y **Productos**.

---

## 🔐 Autenticación y Autorización

- Sistema de **autenticación JWT Bearer**.  
- Endpoints:
  - `POST /api/auth/register`
  - `POST /api/auth/login`
- Middleware de autorización por roles (desde la tabla `Roles`).
- Control de acceso:  
  - Solo administradores pueden crear o eliminar productos.  
  - Los empleados gestionan pedidos y tareas propias.

---

## 📊 Consultas e Informes con EF Core

Ejemplos de informes a implementar:

- Total de ventas por mes, cliente o categoría.  
- Productos más vendidos.  
- Pedidos gestionados por usuario.  
- Tareas pendientes por usuario o estado.  
- Tiempo promedio de resolución de tareas.  
- Clientes con más pedidos o reclamos.  
- Análisis de productos según antigüedad (`FechaCreacion`, `FechaActualizacion`).

Estas consultas deben practicar:
- `.Include()` / `.ThenInclude()`
- `GroupBy()` y `Join()`
- Proyecciones a DTOs
- Consultas asíncronas y expresiones LINQ complejas.

---

## 💻 Frontend (Angular)

### Módulos sugeridos

1. **Login / Registro**
   - Autenticación JWT.
2. **Dashboard**
   - Resumen de tareas, pedidos y ventas.
3. **Tareas**
   - CRUD + filtros por usuario, estado, cliente o pedido.
4. **Clientes**
   - CRUD + historial de pedidos.
5. **Productos**
   - CRUD + control de fechas y precios.
6. **Pedidos**
   - Crear pedidos seleccionando cliente y productos.
7. **Reportes**
   - Gráficas y estadísticas con filtros dinámicos.

### Integración
- Uso de `HttpClient` con **interceptor** para enviar token Bearer.
- Guards de rutas por rol y autenticación.
- UI moderna con **Angular Material** o **TailwindCSS**.

---

## 🧠 Objetivos de Aprendizaje

- Configurar **EF Core con migraciones y relaciones** (1:N, N:M).  
- Implementar **autenticación JWT** y roles.  
- Practicar **consultas avanzadas EF Core**.  
- Desarrollar un **frontend Angular** conectado a la API.  
- Aplicar **arquitectura limpia y separación por capas**.  
- Crear un sistema base tipo **ERP modular**.

---

## 🚀 Estructura sugerida en Git

```
erpflow/
├── backend/
│   ├── ERPFlow.API/
│   ├── ERPFlow.Core/
│   ├── ERPFlow.Infrastructure/
│   └── ERPFlow.sln
└── frontend/
    └── erpflow-client/
```

**Repositorios:**
- 🔹 `erpflow-api` → Backend .NET 8  
- 🔹 `erpflow-client` → Frontend Angular  

---

## 📅 Próximos pasos

1. Crear el proyecto .NET 8 con Web API.
2. Configurar EF Core + SQLite y crear migraciones.
3. Implementar autenticación JWT.
4. Desarrollar CRUDs base.
5. Implementar consultas avanzadas e informes.
6. Construir el frontend Angular.
7. Integrar ambos y desplegar.

---

> 💬 *ERPFlow: una práctica completa para dominar EF Core, JWT y Angular en un entorno realista de gestión empresarial.*
