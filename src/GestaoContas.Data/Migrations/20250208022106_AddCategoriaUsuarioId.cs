using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoContas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Categorias",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Categorias");
        }
    }
}
