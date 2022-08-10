using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Piranha.Manager;
using Piranha.Manager.LocalAuth;
using System.ComponentModel.DataAnnotations;
using Piranha.AspNetCore.Identity.Data;

namespace GangsterGeek.Piranha.AuthSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISecurity _service;
        private readonly ManagerLocalizer _localizer;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service">The current security service</param>
        /// <param name="localizer">The manager localizer</param>
        public LoginModel(SignInManager<User> signInManager, ISecurity service, ManagerLocalizer localizer)
        {
            _signInManager = signInManager;
            _service = service;
            _localizer = localizer;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Model for form data.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets/sets the user name.
            /// </summary>
            [Required]
            public string Username { get; set; }

            /// <summary>
            /// Gets/sets the password.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Handles authorization after a post.
        /// </summary>
        /// <param name="returnUrl">The optional return url</param>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await CleanSignout();

            if (!ModelState.IsValid || !await _service.SignIn(HttpContext, Input.Username, Input.Password))
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, _localizer.General["Username and/or password are incorrect."].Value);
                return Page();
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect($"~/manager/login/auth?returnUrl={returnUrl}");
            }
            return LocalRedirect("~/manager/login/auth");
        }

        private async Task CleanSignout()
        {
            await _service.SignOut(HttpContext);
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }
}
