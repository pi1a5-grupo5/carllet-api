using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class OtherExpenseTypesChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaintenanceName",
                table: "OtherExpenseTypes",
                newName: "OtherExpenseName");

            migrationBuilder.RenameColumn(
                name: "MaintenanceExpenseTypeId",
                table: "OtherExpenseTypes",
                newName: "OtherExpenseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherExpenseName",
                table: "OtherExpenseTypes",
                newName: "MaintenanceName");

            migrationBuilder.RenameColumn(
                name: "OtherExpenseTypeId",
                table: "OtherExpenseTypes",
                newName: "MaintenanceExpenseTypeId");
        }
    }
}
