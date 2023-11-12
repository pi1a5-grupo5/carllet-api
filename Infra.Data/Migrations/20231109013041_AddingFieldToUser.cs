using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dias_trabalhados",
                table: "usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "exclusivo",
                table: "usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "meta",
                table: "usuarios",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "possui_plano",
                table: "usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpenseDate",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dias_trabalhados",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "exclusivo",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "meta",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "possui_plano",
                table: "usuarios");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpenseDate",
                table: "Expenses",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
