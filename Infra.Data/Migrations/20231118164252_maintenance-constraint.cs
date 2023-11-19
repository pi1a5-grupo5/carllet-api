using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class maintenanceconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Expenses_OriginatingExpenseId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Expenses_OriginatingExpenseId",
                table: "Expenses",
                column: "OriginatingExpenseId",
                principalTable: "Expenses",
                principalColumn: "ExpenseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Expenses_OriginatingExpenseId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Expenses_OriginatingExpenseId",
                table: "Expenses",
                column: "OriginatingExpenseId",
                principalTable: "Expenses",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
