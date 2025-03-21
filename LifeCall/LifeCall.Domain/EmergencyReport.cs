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
    }
}
