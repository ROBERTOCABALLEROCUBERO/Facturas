using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facturas.Migrations
{
    /// <inheritdoc />
    public partial class Nueva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIF = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Poblacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    NumeroFactura = table.Column<int>(type: "int", nullable: false),
                    ClienteID = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.NumeroFactura);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineasFactura",
                columns: table => new
                {
                    LineaFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unidades = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacturaNumeroFactura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasFactura", x => x.LineaFacturaId);
                    table.ForeignKey(
                        name: "FK_LineasFactura_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "NumeroFactura",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineasFactura_Facturas_FacturaNumeroFactura",
                        column: x => x.FacturaNumeroFactura,
                        principalTable: "Facturas",
                        principalColumn: "NumeroFactura");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteID",
                table: "Facturas",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_LineasFactura_FacturaId",
                table: "LineasFactura",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasFactura_FacturaNumeroFactura",
                table: "LineasFactura",
                column: "FacturaNumeroFactura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasFactura");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
