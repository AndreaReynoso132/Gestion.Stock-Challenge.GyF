# Gestion.Stock-Challenge.GyF             
# Stock Management API

Este proyecto es una solución de gestión de stock desarrollada en .NET 6 que permite la gestión de productos y usuarios mediante una API RESTful. La solución incluye autenticación de usuarios utilizando JWT (JSON Web Tokens) y varios endpoints para realizar operaciones CRUD sobre productos, así como la obtención filtrada de productos.

## Requisitos Previos

- **.NET 6 SDK** o superior: Puedes descargarlo desde [aquí](https://dotnet.microsoft.com/download/dotnet/6.0).
- **SQL Server** (local o remoto) para la base de datos.
- **Visual Studio 2022** o cualquier IDE compatible con .NET 6.
- **Git** para clonar el repositorio.
- **Postman** o alguna herramienta similar para probar la API (opcional).

## Instrucciones de Configuración

### 1. Clonar el Repositorio
### 2. Configurar la Base de Datos
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StockManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  
}
### 2. Configurar la Base de Datos
Scripts de Creación de Base de Datos
CREATE DATABASE StockManagementDb;

USE StockManagementDb;

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(255) NOT NULL

);

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Precio DECIMAL(18,2) NOT NULL,
    FechaCarga DATETIME NOT NULL,
    Categoria NVARCHAR(50) NOT NULL
);

### 3. Credenciales
usuario1	contraseña123
usuario2	prueba123

### 4. Autenticación de Usuario
Autentícate utilizando el endpoint /api/Usuario/authenticate para recibir un token JWT. Este token debe ser incluido en el encabezado Authorization como Bearer {token} para acceder a los endpoints protegidos.

### 5. Mejoras y Consideraciones
-Seguridad del Token
Actualmente, la clave secreta (JWT Key) se encuentra hardcoded en el código. 
-Mejora sugerida: Almacenar la clave en un entorno seguro (por ejemplo, variables de entorno o un servicio de gestión de secretos) y cargarla dinámicamente durante la ejecución.

-Almacenamiento seguro de contraseñas: las contraseñas deben estar hasheadas antes de ser almacenadas en la base de datos.

-Cifrado de la clave JWT: Podrías cifrar la clave JWT antes de guardarla en un entorno seguro.





