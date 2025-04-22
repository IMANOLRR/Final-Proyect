using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LifeCall.Domain;

namespace LifeCall.Infrastructure 
{
    public class LifeCallDbContext : DbContext
    {
        public LifeCallDbContext(DbContextOptions<LifeCallDbContext> options) : base(options) 
        {

        }
        public DbSet<EmergencyReport> EmergencyReports { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitAssignment> UnitAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitAssignment>()
                .HasOne(ua => ua.EmergencyReport)
                .WithMany(r => r.UnitAssignments)
                .HasForeignKey(ua => ua.EmergencyReportId);

            modelBuilder.Entity<UnitAssignment>()
                .HasOne(ua => ua.Unit)
                .WithMany()
                .HasForeignKey(ua => ua.UnitId);
        }
    }
}
