using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Earnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ganho",
                columns: table => new
                {
                    id_ganho = table.Column<Guid>(type: "uuid", nullable: false),
                    id_condutor_ganho = table.Column<Guid>(type: "uuid", nullable: true),
                    dt_hr_insercao_ganho = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_ganho = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganho", x => x.id_ganho);
                    table.ForeignKey(
                        name: "FK_Ganho_usuarios_id_condutor_ganho",
                        column: x => x.id_condutor_ganho,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ganho_id_condutor_ganho",
                table: "Ganho",
                column: "id_condutor_ganho");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ganho");
        }
    }
}
