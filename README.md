# ClienteApp - Web API .NET

Esta es una solución estructurada en **Modelo en Capas** utilizando **.NET 6/7**, **Entity Framework Core**, y **SQL Server 2019** ejecutado en un contenedor Docker. La API está completamente documentada y es interactiva a través de **Swagger**.

---

## 🚀 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado lo siguiente en tu máquina local:
*   [Docker Desktop](https://docker.com)
*   [Visual Studio 2022](https://microsoft.com) (con la carga de trabajo *Desarrollo de ASP.NET y web*)
*   [.NET 6.0 SDK o .NET 7.0 SDK](https://microsoft.com)
*   Un cliente SQL como **SQL Server Management Studio (SSMS)** o **Azure Data Studio** (opcional, para verificar datos).

---

## 🛠️ Paso 1: Configurar la Base de Datos en Docker

Para no depender de una instalación local de SQL Server, levantaremos una instancia oficial de **SQL Server 2019** en Docker.

1. Abre tu terminal (PowerShell, CMD o Bash) y ejecuta el siguiente comando para descargar la imagen y arrancar el contenedor:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourSecurePassWord123!" -p 1433:1433 --name sql2019_clientes -d ://microsoft.com
   ```

   *Note: Si deseas cambiar la contraseña (`YourSecurePassWord123!`), recuerda actualizarla también en el paso de configuración de la API.*

2. Verifica que el contenedor esté corriendo de forma correcta:
   ```bash
   docker ps
   ```

---

## 📜 Paso 2: Inicializar la Base de Datos (Scripts)

Conéctate a tu base de datos mediante tu herramienta favorita (SSMS / Azure Data Studio) usando los siguientes datos de conexión:
*   **Servidor:** `localhost,1433`
*   **Autenticación:** SQL Server Authentication
*   **Usuario:** `sa`
*   **Contraseña:** `YourSecurePassWord123!`

Ejecuta el siguiente script completo para crear la base de datos, la tabla, un registro de prueba y el Procedimiento Almacenado:

```sql
-- 1. Crear Base de Datos
CREATE DATABASE DBClientes;
GO

USE DBClientes;
GO

-- 2. Crear Tabla Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- Insertar un dato de prueba
INSERT INTO Clientes (Identificacion, Nombre, Apellido, Email) 
VALUES ('12345678', 'Juan', 'Pérez', 'juan.perez@email.com');
GO

-- 3. Crear Stored Procedure
CREATE PROCEDURE sp_ObtenerClientePorIdentificacion
    @Identificacion VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, Identificacion, Nombre, Apellido, Email, FechaCreacion
    FROM Clientes
    WHERE Identificacion = @Identificacion;
END;
GO
```

---

## ⚙️ Paso 3: Configurar la Aplicación (.NET API)

1. Dirígete al proyecto **ClienteApp.API** y abre el archivo `appsettings.json`.
2. Asegúrate de que la cadena de conexión (`DefaultConnection`) apunte al contenedor de Docker con las credenciales configuradas:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=DBClientes;User Id=sa;Password=YourSecurePassWord123!;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🏃‍♂️ Paso 4: Ejecutar la Aplicación

### Opción A: Desde Visual Studio 2022
1. Abre el archivo de solución `ClienteSln.sln` en Visual Studio.
2. Haz clic derecho sobre el proyecto **ClienteApp.API** y selecciona **Establecer como proyecto de inicio**.
3. Presiona **F5** o haz clic en el botón **Play (ClienteApp.API)**.
4. Se abrirá automáticamente el navegador en la ruta de Swagger: `https://localhost:XXXX/swagger/index.html`.

### Opción B: Desde la Consola (.NET CLI)
1. Abre una terminal en la raíz de la solución y navega hasta la carpeta de la API:
   ```bash
   cd ClienteApp.API
   ```
2. Ejecuta el comando de arranque:
   ```bash
   dotnet run
   ```
3. Copia la URL de tipo `https://localhost:XXXX` que arroje la consola y añade `/swagger` al final en tu navegador para interactuar con la app.

---

## 🧪 Paso 5: Probar el Endpoint en Swagger

1. En la página de Swagger, expande el endpoint `GET /api/clientes/{identificacion}`.
2. Haz clic en el botón **Try it out**.
3. En el campo `identificacion`, escribe el valor de prueba: `12345678`.
4. Presiona el botón azul **Execute**.
5. Deberías recibir una respuesta con estado `200 OK` y el siguiente cuerpo JSON:

```json
{
  "identificacion": "12345678",
  "nombreCompleto": "Juan Pérez",
  "email": "juan.perez@email.com"
}
```

---

## 🏗️ Estructura del Proyecto (Arquitectura en Capas)

*   **ClienteApp.API**: Controladores expuestos, configuración del pipeline HTTP (`Program.cs`) e integración de Swagger.
*   **ClienteApp.Services**: Capa de negocio. Transforma las entidades del repositorio en DTOs y maneja las reglas lógicas.
*   **ClienteApp.Repositories**: Acceso a datos. Incluye el `ApplicationDbContext`, la definición de entidades y la ejecución del Stored Procedure con Entity Framework Core.
*   **ClienteApp.DTOs**: Objetos planos (Data Transfer Objects) usados para moldear de forma segura las entradas y salidas de la API.

  
