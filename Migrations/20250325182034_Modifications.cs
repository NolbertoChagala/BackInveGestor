using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_gestorinv.Migrations
{
    /// <inheritdoc />
    public partial class Modifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stock_anterior",
                table: "Detalles_Movimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stock_nuevo",
                table: "Detalles_Movimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stack_trace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_code = table.Column<int>(type: "int", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.id_log);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropColumn(
                name: "stock_anterior",
                table: "Detalles_Movimiento");

            migrationBuilder.DropColumn(
                name: "stock_nuevo",
                table: "Detalles_Movimiento");
        }
    }
}
