using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeCall.Domain
{
    public class EmergencyReport
    {
        public int Id { get; set; }
        public string CallerName { get; set; }
        public string Description { get; set; }
        public DateTime DateReport { get; set; }
        public string Status { get; set; }
        public ICollection<UnitAssignment> UnitAssignments { get; set; } = new List<UnitAssignment>();
    }
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class UnitAssignment
    {
        public int Id { get; set; }
        public int EmergencyReportId { get; set; }
        public int UnitId { get; set; }
        public DateTime AssignmentDate { get; set; }

        public EmergencyReport EmergencyReport { get; set; }
        public Unit Unit { get; set; }
    }

}
