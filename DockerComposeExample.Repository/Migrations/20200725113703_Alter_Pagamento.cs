using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DockerComposeExample.Repository.Migrations
{
    public partial class Alter_Pagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorPagoPeloCliente",
                table: "Pagamento",
                newName: "ValorPagoCliente");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataCadastro",
                table: "Pagamento",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorPagoCliente",
                table: "Pagamento",
                newName: "ValorPagoPeloCliente");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCadastro",
                table: "Pagamento",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
