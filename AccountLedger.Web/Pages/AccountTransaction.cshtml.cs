using AccountLedger.Models;
using AccountLedger.Services;
using AccountLedger.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountLedger.Pages
{
    public class AccountTransactionModel : PageModel
    {

        private readonly IAccountTransactionService _service;

        [BindProperty]
        public AccountTransaction NewTransaction { get; set; } = new();
        public List<AccountTransaction> transactions = new();

        public AccountTransactionModel(IAccountTransactionService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // transactions = AccountTransactionService.GetAll();
            transactions = await _service.GetAll();

            return Page();
        }

        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }
        // }
        public IActionResult OnPostEdit(int id)
        {
            //_service.Update(id);
            return RedirectToAction("Get");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // AccountTransactionService.Add(NewTransaction);
            _service.Add(NewTransaction);
            return RedirectToAction("get");
        }

        public IActionResult OnPostDelete(int id)
        {
            // AccountTransactionService.Delete(id);
            _service.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
