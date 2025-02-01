using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoContas.Shared.Migrations
{
    /// <inheritdoc />
    public partial class Orcamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orcamento_Categorias_CategoriaId",
                table: "Orcamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Orcamento_Usuarios_UsuarioId",
                table: "Orcamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orcamento",
                table: "Orcamento");

            migrationBuilder.RenameTable(
                name: "Orcamento",
                newName: "Orcamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Orcamento_UsuarioId",
                table: "Orcamentos",
                newName: "IX_Orcamentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Orcamento_CategoriaId",
                table: "Orcamentos",
                newName: "IX_Orcamentos_CategoriaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Limite",
                table: "Orcamentos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orcamentos",
                table: "Orcamentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamentos_Categorias_CategoriaId",
                table: "Orcamentos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamentos_Usuarios_UsuarioId",
                table: "Orcamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orcamentos_Categorias_CategoriaId",
                table: "Orcamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Orcamentos_Usuarios_UsuarioId",
                table: "Orcamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orcamentos",
                table: "Orcamentos");

            migrationBuilder.RenameTable(
                name: "Orcamentos",
                newName: "Orcamento");

            migrationBuilder.RenameIndex(
                name: "IX_Orcamentos_UsuarioId",
                table: "Orcamento",
                newName: "IX_Orcamento_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Orcamentos_CategoriaId",
                table: "Orcamento",
                newName: "IX_Orcamento_CategoriaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Limite",
                table: "Orcamento",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orcamento",
                table: "Orcamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamento_Categorias_CategoriaId",
                table: "Orcamento",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamento_Usuarios_UsuarioId",
                table: "Orcamento",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
