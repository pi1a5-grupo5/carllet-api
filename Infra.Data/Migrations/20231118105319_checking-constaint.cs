using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class checkingconstaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_FuelExpenseTypes_FuelExpenseTypeId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "FuelTypeId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_FuelExpenseTypes_FuelExpenseTypeId",
                table: "Expenses",
                column: "FuelExpenseTypeId",
                principalTable: "FuelExpenseTypes",
                principalColumn: "FuelExpenseTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_FuelExpenseTypes_FuelExpenseTypeId",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId",
                table: "Expenses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_FuelExpenseTypes_FuelExpenseTypeId",
                table: "Expenses",
                column: "FuelExpenseTypeId",
                principalTable: "FuelExpenseTypes",
                principalColumn: "FuelExpenseTypeId");
        }
    }
}
