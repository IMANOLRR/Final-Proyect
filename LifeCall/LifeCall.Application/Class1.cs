using LifeCall.Domain.Entities;
using LifeCall.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace LifeCall.Application.Services
{
    public class EmergencyService
    {
        private readonly LifeCallDbContext _context;

        public EmergencyService(LifeCallDbContext context)
        {
            _context = context;
        }

        public List<EmergencyReport> GetAllReports()
        {
            return _context.EmergencyReports.ToList();
        }

        public void CreateReport(EmergencyReport report)
        {
            _context.EmergencyReports.Add(report);
            _context.SaveChanges();
        }
    }
}
