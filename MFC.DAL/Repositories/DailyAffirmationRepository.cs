using MFC.CORE.Models;
using MFC.DAL.Database;
using MFC.CORE.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.DAL.Repositories
{
    public class DailyAffirmationRepository : IDailyAffirmationRepository
    {
        private readonly MFCContext _context;

        public DailyAffirmationRepository(MFCContext context)
        {
            _context = context;
        }

        public async Task<DailyAffirmation> GetAffirmationAsync(int id)
        {
            return await _context.DailyAffirmations.FindAsync(id);
        }


        public async Task<IEnumerable<DailyAffirmation>> GetAllAffirmationsAsync()
        {
            return await _context.DailyAffirmations.ToListAsync();
        }

        public async Task AddAffirmationAsync(DailyAffirmation affirmation)
        {
            await _context.DailyAffirmations.AddAsync(affirmation);
            await _context.SaveChangesAsync();
        }
    }
}
