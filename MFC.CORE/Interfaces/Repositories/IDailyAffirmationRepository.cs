using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFC.CORE.Models;

namespace MFC.CORE.Interfaces
{
    public interface IDailyAffirmationRepository
    {
        Task<DailyAffirmation> GetAffirmationAsync(int id);
        Task<IEnumerable<DailyAffirmation>> GetAllAffirmationsAsync();
        Task AddAffirmationAsync(DailyAffirmation affirmation);
        // Andere relevante methoden...
    }
}

