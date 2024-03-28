using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Models
{
    public class Meditation
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int Duration { get; set; } // Duur in minuten
        public MeditationCategory Category { get; set; }
    }

    public enum MeditationCategory
    {
        StressReduction,
        Sleep,
        Beginners,
        Advanced
    }
}
