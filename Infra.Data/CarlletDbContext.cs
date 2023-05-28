using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class CarlletDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Course> Course { get; set; }
        public CarlletDbContext(DbContextOptions<CarlletDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
            .HasKey(c => new { c.OwnerId, c.VehicleId });

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.OwnerId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Vehicle)
                .WithMany(v => v.Courses)
                .HasForeignKey(c => c.VehicleId);
        }


    }
}
