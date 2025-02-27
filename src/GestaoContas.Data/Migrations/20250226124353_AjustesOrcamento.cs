using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoContas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesOrcamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Orcamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ano",
                table: "Orcamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "Orcamentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
