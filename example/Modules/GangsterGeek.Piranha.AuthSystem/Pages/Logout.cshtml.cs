using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Piranha.AspNetCore.Identity.Data;
using Piranha.Manager.LocalAuth;

namespace GangsterGeek.Piranha.AuthSystem.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ISecurity _service;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<User> signInManager, ISecurity service, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            await CleanSignout();
            _logger.LogInformation("User logged out.");
            return LocalRedirect("~/");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await CleanSignout();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
                return LocalRedirect(returnUrl);
            else
                return LocalRedirect("~/");
        }

        private async Task CleanSignout()
        {
            await _service.SignOut(HttpContext);
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }
}
