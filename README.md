# ApiKnowledgeMap — Sistema de Gestión del Conocimiento Universitario

![.NET Version](https://img.shields.io/badge/.NET-9.0-blue?logo=dotnet)
![Database](https://img.shields.io/badge/DB-SQL_Server_%7C_Postgres_%7C_MySQL-brightgreen)
![Auth](https://img.shields.io/badge/Auth-JWT_%26_BCrypt-gold)
![Architecture](https://img.shields.io/badge/Architecture-Clean_%26_SOLID-orange)

API REST genérica para operaciones CRUD sobre la base de datos `knowledge_map_db`.  
Un solo controlador maneja todas las tablas dinámicamente.

---

## Entregable 1 — Tablas sin FK

| Tabla | Columnas |
|-------|----------|
| `car_innovacion` | id, nombre, descripcion, tipo |
| `enfoque` | id, nombre, descripcion |
| `practica_estrategia` | id, tipo, nombre, descripcion |
| `aspecto_normativo` | id, tipo, descripcion, fuente |
| `universidad` | id, nombre, tipo, ciudad |

---

## Endpoints disponibles

Todos los endpoints siguen el patrón `/api/{tabla}`:

```
GET    /api/{tabla}                          → Listar todos los registros
GET    /api/{tabla}/{nombreClave}/{valor}    → Obtener por clave
POST   /api/{tabla}                          → Crear registro (body JSON)
PUT    /api/{tabla}/{nombreClave}/{valor}    → Actualizar registro (body JSON)
DELETE /api/{tabla}/{nombreClave}/{valor}    → Eliminar registro
```

### Ejemplos con las 5 tablas del Entregable 1

```http
# Listar todas las características de innovación
GET /api/car_innovacion

# Obtener un enfoque por ID
GET /api/enfoque/id/1

# Crear una practica_estrategia
POST /api/practica_estrategia
Content-Type: application/json
{ "id": 1, "tipo": "Pedagogica", "nombre": "Aprendizaje basado en proyectos", "descripcion": "ABP" }

# Actualizar un aspecto_normativo
PUT /api/aspecto_normativo/id/3
Content-Type: application/json
{ "tipo": "Legal", "descripcion": "Resolución actualizada", "fuente": "MEN" }

# Eliminar una universidad
DELETE /api/universidad/id/2
```

### Parámetros opcionales de query string

| Parámetro | Descripción | Ejemplo |
|-----------|-------------|---------|
| `esquema` | Esquema SQL (default: `dbo`) | `?esquema=dbo` |
| `limite` | Máximo de registros (default: 1000) | `?limite=50` |
| `camposEncriptar` | Campos a encriptar con BCrypt | `?camposEncriptar=contrasena` |

---

## Instalación

### Prerrequisitos
- .NET 9.0 SDK
- SQL Server o SQL Server LocalDB
- Base de datos `knowledge_map_db` creada con el script SQL

### Pasos

```bash
# 1. Clonar / abrir el repositorio
cd ApiKnowledgeMap

# 2. Restaurar paquetes NuGet
dotnet restore

# 3. Configurar la cadena de conexión en appsettings.Development.json
#    Editar la sección "ConnectionStrings" → "LocalDb" o "SqlServer"

# 4. Compilar
dotnet build

# 5. Ejecutar
dotnet run

# 6. Abrir Swagger en el navegador
# http://localhost:{puerto}/swagger
```

---

## Configuración (appsettings.json)

```json
{
  "DatabaseProvider": "LocalDb",
  "ConnectionStrings": {
    "LocalDb": "Server=(localdb)\\MSSQLLocalDB;Database=knowledge_map_db;Integrated Security=True;TrustServerCertificate=True;"
  },
  "TablasProhibidas": []
}
```

Cambiar `DatabaseProvider` para usar otra BD: `SqlServer`, `Postgres`, `MariaDB`, `MySQL`.

---

## Estructura del proyecto

```
ApiKnowledgeMap/
├── Controllers/
│   └── EntidadesController.cs       ← CAPA PRESENTACIÓN: endpoints HTTP
├── Modelos/
│   └── ConfiguracionJwt.cs          ← Configuración JWT
├── Servicios/
│   ├── Abstracciones/
│   │   ├── IProveedorConexion.cs    ← Contrato para cadenas de conexión
│   │   ├── IServicioCrud.cs         ← Contrato del servicio CRUD
│   │   └── IPoliticaTablasProhibidas.cs
│   ├── Conexion/
│   │   └── ProveedorConexion.cs     ← Lee appsettings.json
│   ├── Politicas/
│   │   └── PoliticaTablasProhibidasDesdeJson.cs
│   ├── Utilidades/
│   │   └── EncriptacionBCrypt.cs    ← Hash de contraseñas
│   └── ServicioCrud.cs              ← CAPA NEGOCIO: validaciones y lógica
├── Repositorios/
│   ├── Abstracciones/
│   │   └── IRepositorioLecturaTabla.cs  ← Contrato de acceso a datos
│   └── RepositorioLecturaSqlServer.cs   ← CAPA DATOS: SQL Server
├── Program.cs                       ← Inyección de dependencias y middleware
├── appsettings.json
└── appsettings.Development.json
```

---

## Principios SOLID aplicados

| Principio | Aplicación |
|-----------|------------|
| **S** - Single Responsibility | Controllers, Servicios, Repositorios separados |
| **O** - Open/Closed | Nueva BD = nueva clase, sin tocar las existentes |
| **L** - Liskov Substitution | Cambiar proveedor en config, todo sigue funcionando |
| **I** - Interface Segregation | `IServicioCrud` vs `IProveedorConexion` específicas |
| **D** - Dependency Inversion | Servicios reciben interfaces, no clases concretas |

---

## Commits sugeridos (por Parte del tutorial)

```bash
git commit -m "Parte 3: Crear estructura de carpetas del proyecto"
git commit -m "Parte 4: Agregar interfaz IRepositorioLecturaTabla con operaciones CRUD asíncronas"
git commit -m "Parte 5: Instalar paquetes NuGet y crear interfaz IProveedorConexion"
git commit -m "Parte 6: Crear implementación ProveedorConexion que lee de appsettings.json"
git commit -m "Parte 7: Crear utilidad EncriptacionBCrypt para hashear contraseñas"
git commit -m "Parte 8: Crear RepositorioLecturaSqlServer con detección automática de tipos"
git commit -m "Parte 11: Crear capa de servicio: IServicioCrud, ServicioCrud, política de tablas prohibidas"
git commit -m "Parte 12: Crear EntidadesController con endpoints CRUD genéricos"
git commit -m "Partes 18-19: Agregar appsettings.json y Program.cs con DI y middleware"
```
