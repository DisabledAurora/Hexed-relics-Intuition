using Hexed_Relics.Data;
using Hexed_Relics.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Hexed_Relics.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly HexedRelicsContext _context;

        public LoginModel(HexedRelicsContext context)
        {
            _context = context;
        }

        [BindProperty, Required(ErrorMessage = "You must enter your username.")]
        public string? Username { get;set; }
        
        [BindProperty, Required(ErrorMessage = "You must enter your Password."), DataType(DataType.Password)]
        public string? Password { get;set; }

        [BindProperty]
        public bool RememberMe { get; set; } = false;

        public User? LoginUser { get; set; }

        public string Message { get; set; }

        private async Task CreateIdentity(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, "authcookie");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = RememberMe
            };

            await HttpContext.SignInAsync("authcookie", claimsPrincipal, authProperties);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            LoginUser = (from u in _context.Users
                         where Username == u.UserName && Password == u.Password
                         select u).FirstOrDefault();
            if(LoginUser != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,LoginUser.UserName),
                        new Claim("User","true")
                    };
                await CreateIdentity(claims);
                return RedirectToPage("/Banking/MainScreen", new { Message = "Login Success" });
            }
            else
            {
                Message = "Wrong Username or Password.";
                return Page();
            }
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Banking/MainScreen");
            else return Page();
        }
    }
}
