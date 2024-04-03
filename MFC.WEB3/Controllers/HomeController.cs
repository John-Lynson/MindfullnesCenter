using Microsoft.AspNetCore.Mvc;
using MFC.CORE.Interfaces.Services; // Zorg ervoor dat je de juiste namespaces gebruikt
using System.Threading.Tasks;
using MFC.CORE.DTOs;

public class HomeController : Controller
{
    private readonly IAffirmationService _affirmationService;

    public HomeController(IAffirmationService affirmationService)
    {
        _affirmationService = affirmationService;
    }

    public async Task<IActionResult> Index()
    {
        var affirmation = await _affirmationService.GetTodaysAffirmationAsync();
        // Optioneel: converteer je entity naar een DTO als je die gebruikt
        var affirmationDto = new DailyAffirmationDto { Message = affirmation?.Message };
        return View(affirmationDto);
    }
}
