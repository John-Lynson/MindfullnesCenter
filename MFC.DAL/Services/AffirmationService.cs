using MFC.CORE.Interfaces.Services; 
using MFC.CORE.Models;
using MFC.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MFC.DAL.Services
{
    // Update de klasse om IAffirmationService te implementeren
    public class AffirmationService : IAffirmationService
    {
        private readonly MFCContext _context;

        public AffirmationService(MFCContext context)
        {
            _context = context;
        }

        public async Task<DailyAffirmation> GetTodaysAffirmationAsync()
        {
            // Jouw bestaande logica om de dagelijkse affirmatie op te halen
            return await _context.DailyAffirmations
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }
    }
}
