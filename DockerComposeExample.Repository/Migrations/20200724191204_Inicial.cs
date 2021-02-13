using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DockerComposeExample.Repository.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValorPagamento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorPagoPeloCliente = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrocoItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdPagamento = table.Column<Guid>(nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorItem = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TipoItemTroco = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrocoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrocoItem_Pagamento_IdPagamento",
                        column: x => x.IdPagamento,
                        principalTable: "Pagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrocoItem_IdPagamento",
                table: "TrocoItem",
                column: "IdPagamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrocoItem");

            migrationBuilder.DropTable(
                name: "Pagamento");
        }
    }
}
