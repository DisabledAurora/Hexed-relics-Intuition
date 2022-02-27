using Microsoft.AspNetCore.Mvc;
using Hexed_Relics.Data;
using Hexed_Relics.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Hexed_Relics.Pages.Banking
{
    public class DepositsModel : PageModel
    {
        private readonly HexedRelicsContext _context;

        public DepositsModel(HexedRelicsContext context)
        {
            _context = context;
        }

        [BindProperty,Required(ErrorMessage ="Deposit amount must be between $100 to $10000"),Range(100,10000)]
        public float DepositAmt { get; set; }

        [BindProperty,Required(ErrorMessage ="Password cannot be empty")]
        public string Password { get; set; }

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = (from u in _context.Users
                            where u.UserName == User.Identity.Name
                            select u).FirstOrDefault();
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        //DateTime tstDt = DateTime.Now.ToUniversalTime();
                        DateTime tstDt = new DateTime(2022,03,01).ToUniversalTime();
                        Transactions tst = new Transactions(DepositAmt, user.AccountNumber, tstDt, true);
                        user.TotalBalance += DepositAmt;
                        _context.Users.Update(user);
                        _context.Transactions.Add(tst);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("/Banking/MainScreen", new { Message = "Deposit successfully placed." });
                    }
                    else
                    {
                        Message = "Password is incorrect.";
                        return Page();
                    }
                }
                else
                {
                    Message = "User cannot be found.";
                    return Page();
                }
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
