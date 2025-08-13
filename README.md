

# 🏢 Sistema Gestor de Productos

> 💼 Sistema de escritorio para la gestión integral de productos, usuarios y opciones empresariales

[![Estado del Proyecto](https://img.shields.io/badge/Estado-En%20Desarrollo-yellow.svg)](https://github.com/usuario/sistema-gestor-productos)
[![Base de Datos](https://img.shields.io/badge/Base%20de%20Datos-SQL%20Server-blue.svg)](https://www.microsoft.com/sql-server)
[![Licencia](https://img.shields.io/badge/Licencia-MIT-green.svg)](LICENSE)

## 📋 Descripción

El **Sistema Gestor de Productos** es una aplicación de escritorio diseñada para empresas que necesitan administrar eficientemente su inventario de productos, gestionar usuarios del sistema y configurar opciones específicas para cada producto. 

### ✨ Características Principales

- 🔐 **Sistema de autenticación** con login y registro de usuarios
- 📦 **Gestión completa de productos** con filtros avanzados
- 👥 **Administración de usuarios** con validaciones robustas
- ⚙️ **Configuración de opciones** por producto (tamaños, materiales, etc.)
- 🔍 **Filtros inteligentes** por estado y búsqueda
- 📊 **Base de datos SQL Server** optimizada

## 🗄️ Estructura de la Base de Datos

La base de datos `SistemaGestorProductosDB` contiene tres tablas principales: 

### 👤 Tabla Usuarios
- **Campos**: ID, NombreUsuario, Contraseña, Nombre, Apellido, Correo, Teléfono, FechaCreación
- **Validaciones**: Correo único, teléfono único, formato específico

### 📦 Tabla Productos  
- **Campos**: ID, Código, Nombre, Existencia, Estado, NombreProveedor
- **Estados**: Activo/Inactivo para control de visibilidad

### ⚙️ Tabla Opciones
- **Campos**: ID, Nombre, ProductoId, Estado
- **Relación**: Cada opción está vinculada a un producto específico

## 🚀 Instalación

### Prerrequisitos

- 🖥️ Windows 10 o superior
- 🗃️ SQL Server 2019 o superior
- 🔧 .NET Framework 4.8 o superior

### Pasos de Instalación

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/usuario/sistema-gestor-productos.git
   cd sistema-gestor-productos
   ```

2. **Configurar la base de datos**
   ```sql
   -- Ejecutar el script database/SistemaGestorProductosDB.sql
   -- en SQL Server Management Studio
   ```

3. **Configurar la cadena de conexión**
   ```xml
   <!-- Actualizar app.config con tu servidor SQL -->
   <connectionStrings>
     <add name="DefaultConnection" 
          connectionString="Server=tu-servidor;Database=SistemaGestorProductosDB;Integrated Security=true;" />
   </connectionStrings>
   ```

4. **Compilar y ejecutar**
   ```bash
   dotnet build
   dotnet run
   ```

## 🎯 Funcionalidades

### 🔐 Sistema de Autenticación
- **Login**: Acceso con usuario y contraseña
- **Registro**: Creación de nuevos usuarios con validaciones:
  - ✅ Correo: formato `usuario@dominio.com`
  - ✅ Teléfono: formato `0000-0000`

### 📦 Gestión de Productos
- **Visualización**: Lista completa de productos
- **Filtros**:
  - 🔍 Búsqueda por nombre o código
  - 📊 Filtro por estado (Activo/Inactivo)
- **Información**: Código, nombre, existencia, proveedor

### ⚙️ Gestión de Opciones
- **Visualización**: Opciones asociadas a cada producto
- **Gestión**: Agregar y modificar opciones
- **Ejemplos**: 
  - Vaso → Grande, Mediano, Pequeño
  - Plato → Cerámica, Plástico

## 🛠️ Tecnologías Utilizadas

- **Frontend**: Windows Forms
- **Backend**: C# .NET Framework
- **Base de Datos**: Microsoft SQL Server
- **ORM**: Entity Framework

## 🔧 Configuración

### Variables de Entorno
```env
DB_SERVER=localhost
DB_NAME=SistemaGestorProductosDB
DB_INTEGRATED_SECURITY=true
```

### Validaciones del Sistema
- **Correo**: Expresión regular para formato válido
- **Teléfono**: Patrón `\d{4}-\d{4}`
- **Contraseñas**: Encriptación antes del almacenamiento

## 📊 Datos de Prueba

El sistema incluye datos de ejemplo: 
- **10 productos** predefinidos (vasos, platos, cubiertos, etc.)
- **6 opciones** de ejemplo para diferentes productos
- **Proveedores** asociados a cada producto


## 👨‍💻 Autor

**Kenley Gaitan**
- 📧 Email: kenleyjos619@gmail.com
- 💼 LinkedIn: [linkedin.com/in/kenley-gaitan](https://linkedin.com/in/kenley-gaitan)

---



