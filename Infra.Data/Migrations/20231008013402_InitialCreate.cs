using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelExpenseTypes",
                columns: table => new
                {
                    FuelExpenseTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FuelExpenseName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelExpenseTypes", x => x.FuelExpenseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceExpenseTypes",
                columns: table => new
                {
                    MaintenanceExpenseTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaintenanceName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceExpenseTypes", x => x.MaintenanceExpenseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "marca",
                columns: table => new
                {
                    id_marca = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marca", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    numero_cnh = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    telefone = table.Column<string>(type: "text", nullable: true),
                    deviceid = table.Column<string>(type: "text", nullable: true),
                    verification_token = table.Column<string>(type: "text", nullable: true),
                    verificationh_token_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    access_token = table.Column<string>(type: "text", nullable: true),
                    access_token_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modelo",
                columns: table => new
                {
                    id_modelo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    id_marca = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelo", x => x.id_modelo);
                    table.ForeignKey(
                        name: "FK_modelo_marca_id_marca",
                        column: x => x.id_marca,
                        principalTable: "marca",
                        principalColumn: "id_marca",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Prevision",
                columns: table => new
                {
                    id_previsao = table.Column<Guid>(type: "uuid", nullable: false),
                    id_condutor_previsao = table.Column<Guid>(type: "uuid", nullable: true),
                    dt_hr_insercao_previsao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_previsao = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevision", x => x.id_previsao);
                    table.ForeignKey(
                        name: "FK_Prevision_usuarios_id_condutor_previsao",
                        column: x => x.id_condutor_previsao,
                        principalTable: "usuarios",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                columns: table => new
                {
                    id_veiculo = table.Column<Guid>(type: "uuid", nullable: false),
                    modelo = table.Column<int>(type: "integer", nullable: false),
                    ano_fabricacao = table.Column<short>(type: "smallint", nullable: false),
                    hodometro = table.Column<int>(type: "integer", nullable: false),
                    is_alugado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculos", x => x.id_veiculo);
                    table.ForeignKey(
                        name: "FK_veiculos_modelo_modelo",
                        column: x => x.modelo,
                        principalTable: "modelo",
                        principalColumn: "id_modelo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Condutores_Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_veiculo = table.Column<Guid>(type: "uuid", nullable: false),
                    id_condutor = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condutores_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condutores_Veiculos_usuarios_id_condutor",
                        column: x => x.id_condutor,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Condutores_Veiculos_veiculos_id_veiculo",
                        column: x => x.id_veiculo,
                        principalTable: "veiculos",
                        principalColumn: "id_veiculo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserVehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Liters = table.Column<decimal>(type: "numeric", nullable: true),
                    FuelTypeId = table.Column<int>(type: "integer", nullable: true),
                    FuelExpenseTypeId = table.Column<int>(type: "integer", nullable: true),
                    MaintenanceExpenseTypeId = table.Column<int>(type: "integer", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    OriginatingExpenseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Condutores_Veiculos_UserVehicleId",
                        column: x => x.UserVehicleId,
                        principalTable: "Condutores_Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Expenses_OriginatingExpenseId",
                        column: x => x.OriginatingExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_FuelExpenseTypes_FuelExpenseTypeId",
                        column: x => x.FuelExpenseTypeId,
                        principalTable: "FuelExpenseTypes",
                        principalColumn: "FuelExpenseTypeId");
                    table.ForeignKey(
                        name: "FK_Expenses_MaintenanceExpenseTypes_MaintenanceExpenseTypeId",
                        column: x => x.MaintenanceExpenseTypeId,
                        principalTable: "MaintenanceExpenseTypes",
                        principalColumn: "MaintenanceExpenseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Percurso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserVehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    distancia_percurso = table.Column<float>(type: "real", nullable: false),
                    data_fim_percurso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Percurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Percurso_Condutores_Veiculos_UserVehicleId",
                        column: x => x.UserVehicleId,
                        principalTable: "Condutores_Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Condutores_Veiculos_id_condutor",
                table: "Condutores_Veiculos",
                column: "id_condutor");

            migrationBuilder.CreateIndex(
                name: "IX_Condutores_Veiculos_id_veiculo",
                table: "Condutores_Veiculos",
                column: "id_veiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FuelExpenseTypeId",
                table: "Expenses",
                column: "FuelExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_MaintenanceExpenseTypeId",
                table: "Expenses",
                column: "MaintenanceExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OriginatingExpenseId",
                table: "Expenses",
                column: "OriginatingExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserVehicleId",
                table: "Expenses",
                column: "UserVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganho_id_condutor_ganho",
                table: "Ganho",
                column: "id_condutor_ganho");

            migrationBuilder.CreateIndex(
                name: "IX_modelo_id_marca",
                table: "modelo",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_Percurso_UserVehicleId",
                table: "Percurso",
                column: "UserVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Prevision_id_condutor_previsao",
                table: "Prevision",
                column: "id_condutor_previsao");

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_modelo",
                table: "veiculos",
                column: "modelo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Ganho");

            migrationBuilder.DropTable(
                name: "Percurso");

            migrationBuilder.DropTable(
                name: "Prevision");

            migrationBuilder.DropTable(
                name: "FuelExpenseTypes");

            migrationBuilder.DropTable(
                name: "MaintenanceExpenseTypes");

            migrationBuilder.DropTable(
                name: "Condutores_Veiculos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "veiculos");

            migrationBuilder.DropTable(
                name: "modelo");

            migrationBuilder.DropTable(
                name: "marca");
        }
    }
}
