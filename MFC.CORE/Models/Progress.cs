using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Models
{
    public class Progress
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // Verbinding met User
        public User User { get; set; }
        public Guid MeditationId { get; set; } // Verbinding met Meditation
        public Meditation Meditation { get; set; }
        public DateTime Date { get; set; }
        public ProgressStatus Status { get; set; }
    }

    public enum ProgressStatus
    {
        Completed,
        InProgress
    }
}
