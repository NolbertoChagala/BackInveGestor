# Gestor de Inventario

## Problematica

Muchas empresas enfrentan dificultades para gestionar su inventario de manera eficiente, lo que puede ocasionar errores en los registros, pérdidas de productos y problemas en el control de stock. La falta de información organizada y la ausencia de herramientas automatizadas dificultan la toma de decisiones, generando retrasos y desorganización en el almacén. Además, la gestión manual puede derivar en inconsistencias en los datos, afectando el abastecimiento y la relación con los proveedores. Un sistema web optimizaría estos procesos, permitiendo un mejor control de los productos, facilitando la actualización del inventario y proporcionando reportes que ayuden en la planificación y administración eficiente del almacén.

## Integrantes

Alvarado Salazar Anthony Willians - 22393157
Git: WilliansAS

Ortega Mazun Miguel Angel - 22393179
Git: 22393179 Miguel Ortega

Canche Ramirez Angel Julian - 22393136
Git: JuL14N64

Coto Chagala Nolberto - 21393137
Git: NolbertoChagala

## Librerías

ASP.NET Core

Entity Framework

BCrypt.Net-Next

Microsoft.AspNetCore.Authentication.JwtBearer

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

Swashbuckle (Swagger)


## Como correr el proyecto

#### Primero clonamos el repositorio

sh
git clone https://github.com/NolbertoChagala/BackInveGestor.git


#### Una vez clando, lo abrimos y ejecutamos el siguiente comando en la terminal del proyecto

sh
dotnet restore


#### Continuamos con el appsettings.json, modificalo y asigna la base de datos correspondiente a tu SQLServer


sh
"ConnectionStrings": {
  "DefaultConnection": "Server=Servidor_SQL;Database=BackGes;Trusted_Connection=True;Encrypt=False"
}



#### Volvimos abrir la consola y ejecutamos el ultimo comando

sh
update-database
