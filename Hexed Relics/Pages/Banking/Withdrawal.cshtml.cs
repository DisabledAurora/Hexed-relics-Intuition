using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Hexed_Relics.Data;
using Hexed_Relics.Models;

namespace Hexed_Relics.Pages.Banking
{
    public class WithdrawalModel : PageModel
    {
        private readonly HexedRelicsContext _context;

        public WithdrawalModel(HexedRelicsContext context)
        {
            _context = context;
        }

        [BindProperty,Required(ErrorMessage ="Withdrawal or transfer amount cannot be empty"),
            Range(0,10000)]
        public float Amount { get; set; }

        [BindProperty,Required(ErrorMessage ="Password cannot be empty.")]
        public string Password { get; set; }

        [BindProperty]
        public string? UserToReceive { get; set; } = "";

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = (from u in _context.Users
                            where u.UserName == User.Identity.Name
                            select u).FirstOrDefault();
                if (user.TotalBalance < Amount)
                {
                    Message = "Your bank account balance is insufficient.";
                    return Page();
                }
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        //TRANSFER TO ANOTHER USER
                        if (UserToReceive != "")
                        {
                            var userReceive = (from u in _context.Users
                                               where u.UserName == UserToReceive
                                               select u).FirstOrDefault();
                            if (userReceive != null)
                            {
                                DateTime tstTransfer = DateTime.Now.ToUniversalTime();
                                Transactions tstFrom = new Transactions(Amount, user.AccountNumber, tstTransfer, false);
                                Transactions tstTo = new Transactions(Amount, userReceive.AccountNumber, tstTransfer, true);

                                user.TotalBalance -= Amount;
                                userReceive.TotalBalance += Amount;
                                _context.Users.Update(user);
                                _context.Users.Update(userReceive);
                                _context.Transactions.Add(tstFrom);
                                _context.Transactions.Add(tstTo);

                                await _context.SaveChangesAsync();
                                return RedirectToPage("/Banking/MainScreen", new {Message = "Transfer successful."});
                            }
                            else
                            {
                                Message = "Account number is incorrect.";
                                return Page();
                            }
                        }
                        else //WITHDRAW
                        {
                            DateTime tstDt = DateTime.Now.ToUniversalTime();
                            Transactions tst = new Transactions(Amount, user.AccountNumber, tstDt, false);
                            user.TotalBalance -= Amount;
                            _context.Users.Update(user);
                            _context.Transactions.Add(tst);

                            await _context.SaveChangesAsync();
                            return RedirectToPage("/Banking/MainScreen", new { Message = "Withdraw successful." });
                        }
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
