IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Categorias] (
    [id_categoria] int NOT NULL IDENTITY,
    [categoria] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([id_categoria])
);

CREATE TABLE [Proveedores] (
    [id_proveedor] int NOT NULL IDENTITY,
    [proveedor] nvarchar(max) NOT NULL,
    [telefono] nvarchar(max) NOT NULL,
    [correo] nvarchar(max) NOT NULL,
    [direccion] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Proveedores] PRIMARY KEY ([id_proveedor])
);

CREATE TABLE [Roles] (
    [id_rol] int NOT NULL IDENTITY,
    [rol] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([id_rol])
);

CREATE TABLE [Inventario] (
    [id_producto] int NOT NULL IDENTITY,
    [producto] nvarchar(max) NOT NULL,
    [stock] int NOT NULL,
    [precio_unitario] decimal(10,2) NOT NULL,
    [proveedor_id] int NOT NULL,
    [categoria_id] int NULL,
    CONSTRAINT [PK_Inventario] PRIMARY KEY ([id_producto]),
    CONSTRAINT [FK_Inventario_Categorias_categoria_id] FOREIGN KEY ([categoria_id]) REFERENCES [Categorias] ([id_categoria]) ON DELETE SET NULL,
    CONSTRAINT [FK_Inventario_Proveedores_proveedor_id] FOREIGN KEY ([proveedor_id]) REFERENCES [Proveedores] ([id_proveedor]) ON DELETE CASCADE
);

CREATE TABLE [Usuarios] (
    [id_usuario] int NOT NULL IDENTITY,
    [nombre] nvarchar(max) NOT NULL,
    [correo] nvarchar(max) NOT NULL,
    [contraseña] nvarchar(max) NOT NULL,
    [rol_id] int NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([id_usuario]),
    CONSTRAINT [FK_Usuarios_Roles_rol_id] FOREIGN KEY ([rol_id]) REFERENCES [Roles] ([id_rol]) ON DELETE SET NULL
);

CREATE TABLE [Movimientos_Inventario] (
    [id_movimiento] int NOT NULL IDENTITY,
    [usuario_id] int NULL,
    [tipo_movimiento] nvarchar(max) NOT NULL,
    [fecha_registro] datetime2 NOT NULL,
    CONSTRAINT [PK_Movimientos_Inventario] PRIMARY KEY ([id_movimiento]),
    CONSTRAINT [FK_Movimientos_Inventario_Usuarios_usuario_id] FOREIGN KEY ([usuario_id]) REFERENCES [Usuarios] ([id_usuario]) ON DELETE SET NULL
);

CREATE TABLE [Detalles_Movimiento] (
    [id_detalle] int NOT NULL IDENTITY,
    [movimiento_id] int NOT NULL,
    [producto_id] int NOT NULL,
    [cantidad] int NOT NULL,
    [precio_unitario] decimal(10,2) NOT NULL,
    [total] decimal(10,2) NULL,
    CONSTRAINT [PK_Detalles_Movimiento] PRIMARY KEY ([id_detalle]),
    CONSTRAINT [FK_Detalles_Movimiento_Inventario_producto_id] FOREIGN KEY ([producto_id]) REFERENCES [Inventario] ([id_producto]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Detalles_Movimiento_Movimientos_Inventario_movimiento_id] FOREIGN KEY ([movimiento_id]) REFERENCES [Movimientos_Inventario] ([id_movimiento]) ON DELETE CASCADE
);

CREATE INDEX [IX_Detalles_Movimiento_movimiento_id] ON [Detalles_Movimiento] ([movimiento_id]);

CREATE INDEX [IX_Detalles_Movimiento_producto_id] ON [Detalles_Movimiento] ([producto_id]);

CREATE INDEX [IX_Inventario_categoria_id] ON [Inventario] ([categoria_id]);

CREATE INDEX [IX_Inventario_proveedor_id] ON [Inventario] ([proveedor_id]);

CREATE INDEX [IX_Movimientos_Inventario_usuario_id] ON [Movimientos_Inventario] ([usuario_id]);

CREATE INDEX [IX_Usuarios_rol_id] ON [Usuarios] ([rol_id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250315091146_Initial', N'9.0.3');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250315211228_NombreDeLaMigracion', N'9.0.3');

COMMIT;
GO

