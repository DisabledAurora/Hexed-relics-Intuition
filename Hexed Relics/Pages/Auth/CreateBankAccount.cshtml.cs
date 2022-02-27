using Hexed_Relics.Data;
using Hexed_Relics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hexed_Relics.Pages
{
    public class CreateBankAccountModel : PageModel
    {
        private readonly HexedRelicsContext _context;

        public CreateBankAccountModel(HexedRelicsContext context)
        {
            _context = context;
        }

        [BindProperty,Range(500,30000),Required(ErrorMessage ="Deposit must be within $500 and $30,000")]
        public float Deposit { get; set; }

        [BindProperty,Required(ErrorMessage ="All fields cannot be empty.")]
        public User? NewUser { get; set; }

        public string Message { get; set; } = "";
        
        private int GetAccountNo()
        {
            Random random = new Random();
            User? acc = null;
            int rn;
            do
            {
                rn = random.Next(100000, 999999);
                acc = (from n in _context.Users
                              where n.AccountNumber == rn
                              select n).SingleOrDefault();
            } while (acc != null);
            return rn;
        }

        private bool UsernameIsTaken()
        {
            var un = (from u in _context.Users
                      where u.UserName == NewUser.FullName
                      select u).SingleOrDefault();
            if (un != null)
                return true;
            return false;
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            else if (UsernameIsTaken())
            {
                Message = "Username is taken.";
                return Page();
            }
            else
            {
                NewUser.DateCreated = DateTime.Now.ToUniversalTime();
                NewUser.AccountNumber = GetAccountNo();
                Transactions tst = new Transactions(Deposit,NewUser.AccountNumber,NewUser.DateCreated,true);


                _context.Users.Add(NewUser);
                _context.Transactions.Add(tst);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Banking/MainScreen");
            }
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPage("/Banking/MainScreen");
            else
                return Page();
        }
    }
}
