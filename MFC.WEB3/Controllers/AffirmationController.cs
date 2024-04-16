using Microsoft.AspNetCore.Mvc;
using MFC.CORE.DTOs;
using MFC.DAL.Services;
using MFC.CORE.Interfaces;
using System.Threading.Tasks;
using MFC.CORE.Interfaces.Services;

namespace MFC.WEB3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AffirmationController : ControllerBase
    {
        private readonly IAffirmationService _affirmationService;

        public AffirmationController(IAffirmationService affirmationService)
        {
            _affirmationService = affirmationService;
        }

        [HttpGet("Daily")]
        public async Task<IActionResult> GetDailyAffirmation()
        {
            var affirmation = await _affirmationService.GetTodaysAffirmationAsync();
            if (affirmation == null)
                return NotFound();

            var dto = new DailyAffirmationDto { Message = affirmation.Message };
            return Ok(dto);
        }
    }
}
