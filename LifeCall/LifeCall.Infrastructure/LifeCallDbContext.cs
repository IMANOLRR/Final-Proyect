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
            Database.OpenConnection();
            Database.CloseConnection();
        }
        public DbSet<EmergencyReport> EmergencyReports { get; set; }
    }
}
