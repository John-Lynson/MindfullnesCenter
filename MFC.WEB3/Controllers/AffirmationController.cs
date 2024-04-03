using Microsoft.AspNetCore.Mvc;
using MFC.CORE.DTOs;
using MFC.DAL.Services;
using MFC.CORE.Interfaces;

namespace MFC.WEB3.Controllers
{
    public class AffirmationController : Controller
    {
        private readonly AffirmationService _affirmationService;

        public AffirmationController(AffirmationService affirmationService)
        {
            _affirmationService = affirmationService;
        }

        public async Task<IActionResult> Daily()
        {
            var affirmation = await _affirmationService.GetTodaysAffirmationAsync();
            var dto = new DailyAffirmationDto { Message = affirmation.Message };
            return View(dto);
        }
    }
}
