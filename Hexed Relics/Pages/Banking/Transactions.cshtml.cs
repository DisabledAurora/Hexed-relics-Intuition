using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hexed_Relics.Models;
using Hexed_Relics.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hexed_Relics.Pages.Banking
{
    public class TransactionsModel : PageModel
    {
        private readonly HexedRelicsContext _context;
        
        public TransactionsModel(HexedRelicsContext context)
        {
            _context = context;
        }

        public IList<Transactions> UserTransactions { get; set; }

        public List<Transactions> FilteredTransactions { get; set; } = new List<Transactions>();

        [BindProperty,Required]
        public DateTime Date1 { get; set; }

        [BindProperty,Required]
        public DateTime Date2 { get; set; }

        public string Message { get; set; } = "";


        public async Task<IActionResult> OnGet()
        {
            if(TempData["DateList"] != null)
            {
                var tempData = TempData["DateList"].ToString();
                UserTransactions = JsonConvert.DeserializeObject<List<Transactions>>(tempData);
                TempData["DateList"] = null;
                return Page();
            }

            if(TempData["msg"] != null)
            {
                Message = TempData["msg"].ToString();
            }

            if (TempData["modelmsg"] != null)
            {
                Message = TempData["modelmsg"].ToString();
            }

            if (TempData["Notst"] != null)
            {
                Message = TempData["Notst"].ToString();
            }

            var user = (from u in _context.Users
                        where u.UserName == User.Identity.Name
                        select u).FirstOrDefault();
            if (user != null && UserTransactions == null)
            {
                UserTransactions = await (from t in _context.Transactions
                                          where t.AccNumber == user.AccountNumber
                                          select t).ToListAsync();

                return Page();
            }
            return RedirectToPage("/Banking/MainScreen");
        }

        public async Task<IActionResult> OnPost()
        {
            var user = (from u in _context.Users
                        where u.UserName == User.Identity.Name
                        select u).FirstOrDefault();


            UserTransactions = await (from t in _context.Transactions
                                        where t.AccNumber == user.AccountNumber
                                        select t).ToListAsync();

            if (!ModelState.IsValid)
            {
                Message = "First and second date are both required.";
                TempData["modelmsg"] = Message;
                return RedirectToPage();
            }
            if (Date1 > Date2)
            {
                Message = "First date must be earlier than the second date.";
                TempData["msg"] = Message;
                return RedirectToPage();
            }

            foreach (var t in UserTransactions)
            {
                if (t.DtTst >= Date1 && t.DtTst <= Date2)
                {
                    FilteredTransactions.Add(t);
                }
            }

            if (FilteredTransactions.Count == 0)
            {
                Message = "No transactions found in the date range.";
                TempData["Notst"] = Message;
                return RedirectToPage();
            }

            TempData["DateList"] = JsonConvert.SerializeObject(FilteredTransactions);
            return RedirectToPage();
        }
    }
}
