using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class OtherExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherExpenseTypeId",
                table: "Expenses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherExpense_Details",
                table: "Expenses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoalValue = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_Goals_usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherExpenseTypes",
                columns: table => new
                {
                    MaintenanceExpenseTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaintenanceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherExpenseTypes", x => x.MaintenanceExpenseTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OtherExpenseTypeId",
                table: "Expenses",
                column: "OtherExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_OtherExpenseTypes_OtherExpenseTypeId",
                table: "Expenses",
                column: "OtherExpenseTypeId",
                principalTable: "OtherExpenseTypes",
                principalColumn: "MaintenanceExpenseTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_OtherExpenseTypes_OtherExpenseTypeId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "OtherExpenseTypes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_OtherExpenseTypeId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "OtherExpenseTypeId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "OtherExpense_Details",
                table: "Expenses");
        }
    }
}
