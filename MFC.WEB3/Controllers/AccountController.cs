using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MFC.CORE.Interfaces.Services;
using System.Threading.Tasks;

namespace MFC.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService; 

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = "/")
        {
            // Challenge to trigger the Auth0 authentication
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Auth0");
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Signs out of the Auth0 and cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
            return Ok();
        }

        [Authorize]
        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            var userProfile = _accountService.GetUserProfile();
            if (userProfile == null)
                return NotFound("User profile is not available.");

            return Ok(userProfile);
        }

    }
}
