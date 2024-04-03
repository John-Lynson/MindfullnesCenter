﻿using MFC.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFC.CORE.Interfaces
{
    public interface IAffirmationService
    {
        Task<DailyAffirmation> GetTodaysAffirmationAsync();
    }
}
