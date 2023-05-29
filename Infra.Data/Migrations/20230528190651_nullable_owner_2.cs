using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullable_owner_2 : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Percurso",
                table: "Percurso");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Percurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id_veiculo",
                table: "Percurso",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "id_condutor",
                table: "Percurso",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Percurso",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId1",
                table: "Percurso",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Percurso",
                table: "Percurso",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Percurso_id_condutor",
                table: "Percurso",
                column: "id_condutor");

            migrationBuilder.CreateIndex(
                name: "IX_Percurso_UserId",
                table: "Percurso",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Percurso_VehicleId1",
                table: "Percurso",
                column: "VehicleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_usuarios_UserId",
                table: "Percurso",
                column: "UserId",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso",
                column: "id_condutor",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_veiculos_VehicleId1",
                table: "Percurso",
                column: "VehicleId1",
                principalTable: "veiculos",
                principalColumn: "id_veiculo");

            migrationBuilder.AddForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso",
                column: "id_veiculo",
                principalTable: "veiculos",
                principalColumn: "id_veiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_usuarios_UserId",
                table: "Percurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_usuarios_id_condutor",
                table: "Percurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_veiculos_VehicleId1",
                table: "Percurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Percurso_veiculos_id_veiculo",
                table: "Percurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Percurso",
                table: "Percurso");

            migrationBuilder.DropIndex(
                name: "IX_Percurso_id_condutor",
                table: "Percurso");

            migrationBuilder.DropIndex(
                name: "IX_Percurso_UserId",
                table: "Percurso");

            migrationBuilder.DropIndex(
                name: "IX_Percurso_VehicleId1",
                table: "Percurso");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Percurso");

            migrationBuilder.DropColumn(
                name: "VehicleId1",
                table: "Percurso");

            migrationBuilder.AlterColumn<int>(
                name: "id_veiculo",
                table: "Percurso",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_condutor",
                table: "Percurso",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Percurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Percurso",
                table: "Percurso",
                columns: new[] { "id_condutor", "id_veiculo" });

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
    }
}
