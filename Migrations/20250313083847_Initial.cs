using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_gestorinv.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.id_proveedor);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    proveedor_id = table.Column<int>(type: "int", nullable: false),
                    categoria_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "Categorias",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_proveedor_id",
                        column: x => x.proveedor_id,
                        principalTable: "Proveedores",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "Roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "movimientosInventario",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: true),
                    tipo_movimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimientosInventario", x => x.id_movimiento);
                    table.ForeignKey(
                        name: "FK_movimientosInventario_Productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "Productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movimientosInventario_Usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movimientosInventario_producto_id",
                table: "movimientosInventario",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_movimientosInventario_usuario_id",
                table: "movimientosInventario",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_categoria_id",
                table: "Productos",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_proveedor_id",
                table: "Productos",
                column: "proveedor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_rol_id",
                table: "Usuarios",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimientosInventario");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
