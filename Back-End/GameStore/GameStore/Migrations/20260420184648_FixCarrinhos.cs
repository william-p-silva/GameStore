using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    /// <inheritdoc />
    public partial class FixCarrinhos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinho_Usuarios_UsuarioId",
                table: "Carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItems_Carrinho_CarrinhoId",
                table: "CarrinhoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho");

            migrationBuilder.RenameTable(
                name: "Carrinho",
                newName: "Carrinhos");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinho_UsuarioId",
                table: "Carrinhos",
                newName: "IX_Carrinhos_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItems_Carrinhos_CarrinhoId",
                table: "CarrinhoItems",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinhos_Usuarios_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItems_Carrinhos_CarrinhoId",
                table: "CarrinhoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carrinhos_Usuarios_UsuarioId",
                table: "Carrinhos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinhos",
                table: "Carrinhos");

            migrationBuilder.RenameTable(
                name: "Carrinhos",
                newName: "Carrinho");

            migrationBuilder.RenameIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinho",
                newName: "IX_Carrinho_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinho_Usuarios_UsuarioId",
                table: "Carrinho",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItems_Carrinho_CarrinhoId",
                table: "CarrinhoItems",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
