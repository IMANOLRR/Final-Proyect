using Microsoft.EntityFrameworkCore;
using LifeCall.Domain.Entities;

namespace LifeCall.Infrastructure.Persistance
{
    public class LifeCallDbContext : DbContext
    {
        public LifeCallDbContext(DbContextOptions<LifeCallDbContext> options) : base(options) { }

        public DbSet<EmergencyReport> EmergencyReports { get; set; }
    }
}
