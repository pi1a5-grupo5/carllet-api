﻿// <auto-generated />
using System;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Data.Migrations
{
    [DbContext(typeof(CarlletDbContext))]
    [Migration("20231118110138_adding-constraint")]
    partial class addingconstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Budget.Earning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_ganho");

                    b.Property<decimal>("EarningValue")
                        .HasColumnType("numeric")
                        .HasColumnName("valor_ganho");

                    b.Property<DateTime>("InsertionDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_hr_insercao_ganho");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_condutor_ganho");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Ganho");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.Expense", b =>
                {
                    b.Property<Guid>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserVehicleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("ExpenseId");

                    b.HasIndex("UserVehicleId");

                    b.ToTable("Expenses");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Expense");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.FuelExpenseType", b =>
                {
                    b.Property<int>("FuelExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FuelExpenseTypeId"));

                    b.Property<string>("FuelExpenseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FuelExpenseTypeId");

                    b.ToTable("FuelExpenseTypes");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.MaintenanceExpenseType", b =>
                {
                    b.Property<int>("MaintenanceExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MaintenanceExpenseTypeId"));

                    b.Property<string>("MaintenanceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MaintenanceExpenseTypeId");

                    b.ToTable("MaintenanceExpenseTypes");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.OtherExpenseType", b =>
                {
                    b.Property<int>("OtherExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OtherExpenseTypeId"));

                    b.Property<string>("OtherExpenseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("OtherExpenseTypeId");

                    b.ToTable("OtherExpenseTypes");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Prevision", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_previsao");

                    b.Property<double>("EarningValue")
                        .HasColumnType("double precision")
                        .HasColumnName("valor_previsao");

                    b.Property<DateTime>("InsertionDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dt_hr_insercao_previsao");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_condutor_previsao");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Prevision");
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CourseEndTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_fim_percurso");

                    b.Property<float>("CourseLength")
                        .HasColumnType("real")
                        .HasColumnName("distancia_percurso");

                    b.Property<Guid>("UserVehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserVehicleId");

                    b.ToTable("Percurso");
                });

            modelBuilder.Entity("Domain.Entities.Goal", b =>
                {
                    b.Property<Guid>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("GoalValue")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("GoalId");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text")
                        .HasColumnName("access_token");

                    b.Property<DateTime?>("AccessTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("access_token_expiration");

                    b.Property<string>("Cellphone")
                        .HasColumnType("text")
                        .HasColumnName("telefone");

                    b.Property<string>("Cnh")
                        .HasColumnType("text")
                        .HasColumnName("numero_cnh");

                    b.Property<int?>("DaysWorked")
                        .HasColumnType("integer")
                        .HasColumnName("dias_trabalhados");

                    b.Property<string>("DeviceId")
                        .HasColumnType("text")
                        .HasColumnName("deviceid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("Exclusive")
                        .HasColumnType("boolean")
                        .HasColumnName("exclusivo");

                    b.Property<double?>("Goal")
                        .HasColumnType("double precision")
                        .HasColumnName("meta");

                    b.Property<bool>("HavePlan")
                        .HasColumnType("boolean")
                        .HasColumnName("possui_plano");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("refresh_token_expiration");

                    b.Property<bool>("ResetPassword")
                        .HasColumnType("boolean")
                        .HasColumnName("reset_password");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("text")
                        .HasColumnName("reset_password_token");

                    b.Property<DateTime?>("ResetPasswordTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("reset_password_token_expiration");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("text")
                        .HasColumnName("verification_token");

                    b.Property<DateTime?>("VerificationTokenExpiration")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("verificationh_token_expiration");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean")
                        .HasColumnName("verified");

                    b.HasKey("Id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("Domain.Entities.UserVehicle", b =>
                {
                    b.Property<Guid>("UserVehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_condutor");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("id_veiculo");

                    b.HasKey("UserVehicleId");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Condutores_Veiculos");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id_veiculo")
                        .HasColumnOrder(1);

                    b.Property<short>("FabricationDate")
                        .HasColumnType("smallint")
                        .HasColumnName("ano_fabricacao");

                    b.Property<int>("Odometer")
                        .HasColumnType("integer")
                        .HasColumnName("hodometro");

                    b.Property<bool>("Rented")
                        .HasColumnType("boolean")
                        .HasColumnName("is_alugado");

                    b.Property<int>("VehicleTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("modelo");

                    b.HasKey("VehicleId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("veiculos");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.VehicleBrand", b =>
                {
                    b.Property<int>("VehicleBrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_marca")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VehicleBrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VehicleBrandId");

                    b.ToTable("marca");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.VehicleType", b =>
                {
                    b.Property<int>("VehicleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_modelo")
                        .HasColumnOrder(1);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VehicleTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VehicleBrandId")
                        .HasColumnType("integer")
                        .HasColumnName("id_marca");

                    b.HasKey("VehicleTypeId");

                    b.HasIndex("VehicleBrandId");

                    b.ToTable("modelo");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.FuelExpense", b =>
                {
                    b.HasBaseType("Domain.Entities.Budget.Expenses.Expense");

                    b.Property<int>("FuelExpenseTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Liters")
                        .HasColumnType("numeric");

                    b.HasIndex("FuelExpenseTypeId");

                    b.HasDiscriminator().HasValue("FuelExpense");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.MaintenanceExpense", b =>
                {
                    b.HasBaseType("Domain.Entities.Budget.Expenses.Expense");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaintenanceExpenseTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("OriginatingExpenseId")
                        .HasColumnType("uuid");

                    b.HasIndex("MaintenanceExpenseTypeId");

                    b.HasIndex("OriginatingExpenseId");

                    b.HasDiscriminator().HasValue("MaintenanceExpense");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.OtherExpense", b =>
                {
                    b.HasBaseType("Domain.Entities.Budget.Expenses.Expense");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OtherExpenseTypeId")
                        .HasColumnType("integer");

                    b.HasIndex("OtherExpenseTypeId");

                    b.ToTable("Expenses", t =>
                        {
                            t.Property("Details")
                                .HasColumnName("OtherExpense_Details");
                        });

                    b.HasDiscriminator().HasValue("OtherExpense");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Earning", b =>
                {
                    b.HasOne("Domain.Entities.User", "Owner")
                        .WithMany("Earnings")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.Expense", b =>
                {
                    b.HasOne("Domain.Entities.UserVehicle", "UserVehicle")
                        .WithMany("Expenses")
                        .HasForeignKey("UserVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserVehicle");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Prevision", b =>
                {
                    b.HasOne("Domain.Entities.User", "Owner")
                        .WithMany("Previsions")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.HasOne("Domain.Entities.UserVehicle", "UserVehicle")
                        .WithMany("Courses")
                        .HasForeignKey("UserVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserVehicle");
                });

            modelBuilder.Entity("Domain.Entities.Goal", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserVehicle", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserVehicles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.VehicleNS.Vehicle", "Vehicle")
                        .WithMany("UserVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.Vehicle", b =>
                {
                    b.HasOne("Domain.Entities.VehicleNS.VehicleType", "VehicleType")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.VehicleType", b =>
                {
                    b.HasOne("Domain.Entities.VehicleNS.VehicleBrand", "VehicleBrand")
                        .WithMany("VehicleTypes")
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleBrand");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.FuelExpense", b =>
                {
                    b.HasOne("Domain.Entities.Budget.Expenses.FuelExpenseType", "FuelExpenseType")
                        .WithMany()
                        .HasForeignKey("FuelExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelExpenseType");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.MaintenanceExpense", b =>
                {
                    b.HasOne("Domain.Entities.Budget.Expenses.MaintenanceExpenseType", "MaintenanceExpenseType")
                        .WithMany()
                        .HasForeignKey("MaintenanceExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Budget.Expenses.MaintenanceExpense", "OriginatingExpense")
                        .WithMany()
                        .HasForeignKey("OriginatingExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaintenanceExpenseType");

                    b.Navigation("OriginatingExpense");
                });

            modelBuilder.Entity("Domain.Entities.Budget.Expenses.OtherExpense", b =>
                {
                    b.HasOne("Domain.Entities.Budget.Expenses.OtherExpenseType", "OtherExpenseType")
                        .WithMany()
                        .HasForeignKey("OtherExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OtherExpenseType");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Earnings");

                    b.Navigation("Previsions");

                    b.Navigation("UserVehicles");
                });

            modelBuilder.Entity("Domain.Entities.UserVehicle", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.Vehicle", b =>
                {
                    b.Navigation("UserVehicles");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.VehicleBrand", b =>
                {
                    b.Navigation("VehicleTypes");
                });

            modelBuilder.Entity("Domain.Entities.VehicleNS.VehicleType", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
