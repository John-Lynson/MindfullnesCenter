using MFC.CORE.DTOs;
using MFC.CORE.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// Wijzig ControllerBase naar Controller voor ondersteuning van views
public class HomeController : Controller
{
    private readonly IAffirmationService _affirmationService;

    public HomeController(IAffirmationService affirmationService)
    {
        _affirmationService = affirmationService;
    }

    // Deze actie zal nu een view retourneren
    public async Task<IActionResult> Index()
    {
        var affirmation = await _affirmationService.GetTodaysAffirmationAsync();
        if (affirmation == null)
        {
            // Optioneel, pas aan om een specifieke "NotFound" view te retourneren
            return View("NotFound");
        }

        // Retourneert een view met de affirmationDto als model
        var affirmationDto = new DailyAffirmationDto { Message = affirmation.Message };
        return View(affirmationDto);  // Zorgt ervoor dat de Index.cshtml view het model ontvangt
    }
}
