using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullable_owner_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso",
                column: "id_condutor",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso",
                column: "id_veiculo",
                principalTable: "veiculos",
                principalColumn: "id_veiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso",
                column: "id_condutor",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso",
                column: "id_veiculo",
                principalTable: "veiculos",
                principalColumn: "id_veiculo");
        }
    }
}
