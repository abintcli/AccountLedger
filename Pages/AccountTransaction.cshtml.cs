using AccountLedger.Models;
using AccountLedger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountLedger.Pages
{
    public class AccountTransactionModel : PageModel
    {
        [BindProperty]
        public AccountTransaction NewTransaction { get; set; } = new();
        public List<AccountTransaction> transactions = new();
        public void OnGet()
        {
            transactions = AccountTransactionService.GetAll();
        }

        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }
        // }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AccountTransactionService.Add(NewTransaction);
            return RedirectToAction("get");
        }

        public IActionResult OnPostDelete(int id)
        {
            AccountTransactionService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
