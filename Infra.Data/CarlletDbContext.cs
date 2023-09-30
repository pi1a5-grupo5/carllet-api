using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Entities.VehicleNS;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class CarlletDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<UserVehicle> UserVehicles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<MaintenanceExpense> MaintenanceExpenses {get; set;}
        public DbSet<FuelExpense> FuelExpenses { get; set;}
        public DbSet<MaintenanceExpenseType> MaintenanceExpenseTypes { get; set; }
        public DbSet<FuelExpenseType> FuelExpenseTypes { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Earning> Earning { get; set; }
        public CarlletDbContext(DbContextOptions<CarlletDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserVehicle relations
            modelBuilder.Entity<UserVehicle>()
                .HasKey(uv => new { uv.UserId, uv.VehicleId });

            modelBuilder.Entity<UserVehicle>()
                .HasOne<User>(uv => uv.User)
                .WithMany(u => u.UserVehicles)
                .HasForeignKey(uv => uv.UserId);

            modelBuilder.Entity<UserVehicle>()
                .HasOne<Vehicle>(uv => uv.Vehicle)
                .WithMany(v => v.UserVehicles)
                .HasForeignKey(uv => uv.VehicleId);
            #endregion

            #region  Course and Expenses relations
            modelBuilder.Entity<Course>()
                .HasOne<UserVehicle>(c => c.UserVehicle)
                .WithMany(uv => uv.Courses)
                .HasForeignKey(c => c.UserVehicleId);

            modelBuilder.Entity<Expense>()
                 .HasOne<UserVehicle>(e => e.UserVehicle)
                 .WithMany(uv => uv.Expenses)
                 .HasForeignKey(e => e.UserVehicleId);

            #endregion

            #region User and Budget relations
            modelBuilder.Entity<User>()
                 .HasMany<Earning>(u => u.Earnings)
                 .WithOne(e => e.Owner)
                 .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<User>()
                .HasMany<Prevision>(u => u.Previsions)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);
            #endregion

            #region Vehicle Relations
            modelBuilder.Entity<Vehicle>()
                .HasOne<VehicleType>(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);

            modelBuilder.Entity<VehicleType>()
                .HasOne<VehicleBrand>(vt => vt.VehicleBrand)
                .WithMany(vb => vb.VehicleTypes)
                .HasForeignKey(vt => vt.VehicleBrandId);

            #endregion
        }



    }
}
