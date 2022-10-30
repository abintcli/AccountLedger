using AccountLedger.Models;
using AccountLedger.Services;
using AccountLedger.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountLedger.Pages
{
    public class AccountTransactionsModel : PageModel
    {

        private readonly ITransactionService _service;

        [BindProperty]
        public AccountTransaction NewTransaction { get; set; } = new();
        public List<AccountTransaction> transactions = new();

        public AccountTransactionsModel(ITransactionService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // transactions = AccountTransactionService.GetAll();
            transactions = await _service.GetAll();

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            //_service.Update(id);
            return RedirectToAction("Get");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // AccountTransactionService.Add(NewTransaction);
            await _service.Add(NewTransaction);
            return RedirectToAction("get");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // AccountTransactionService.Delete(id);
            await _service.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
