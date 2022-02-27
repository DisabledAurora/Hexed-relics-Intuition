using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hexed_Relics.Data;
using Hexed_Relics.Models;

namespace Hexed_Relics.Pages
{
    public class MainScreenModel : PageModel
    {
        private readonly HexedRelicsContext _context;

        public MainScreenModel(HexedRelicsContext context)
        {
            _context = context;
        }

        public User? LoggedInUser { get; set; }

        public void OnGet()
        {
            LoggedInUser = (from u in _context.Users
                            where u.UserName == User.Identity.Name
                            select u).FirstOrDefault();
        }
    }
}
