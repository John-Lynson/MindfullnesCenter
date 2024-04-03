using MFC.CORE.Models;
using MFC.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MFC.DAL.Services
{
    public class AffirmationService
    {
        private readonly MFCContext _context;

        public AffirmationService(MFCContext context)
        {
            _context = context;
        }

        public async Task<DailyAffirmation> GetTodaysAffirmationAsync()
        {
            return await _context.DailyAffirmations
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }
    }
}
