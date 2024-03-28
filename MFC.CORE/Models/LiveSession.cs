using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Models
{
    public class LiveSession
    {
        public Guid Id { get; set; }
        public string CoachId { get; set; } // Verbinding met User
        public User Coach { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; } // Duur in minuten
    }
}
