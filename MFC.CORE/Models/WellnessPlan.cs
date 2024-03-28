using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Models
{
    public class WellnessPlan
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // Verbinding met User
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
