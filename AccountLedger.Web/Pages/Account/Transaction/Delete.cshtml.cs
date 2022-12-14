using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountLedger.Models;
using AccountLedger.Web.Services;

namespace AccountLedger.Pages.Account.Transaction
{
    public class DeleteModel : PageModel
    {
        private readonly ITransactionService _service;

        public DeleteModel(ITransactionService service)
        {
            _service = service;
        }

        public int AccountId { get; set; } = 0;

        [BindProperty]
        public AccountTransaction AccountTransaction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounttransaction = await _service.Get(id.Value);

            if (accounttransaction == null)
            {
                return NotFound();
            }
            else
            {
                AccountTransaction = accounttransaction;
                AccountId = accounttransaction.AccountId;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accounttransaction = await _service.Get(id.Value); ;

            if (accounttransaction != null)
            {
                AccountTransaction = accounttransaction;
                await _service.Delete(id.Value);
            }

            return RedirectToPage("./Index", new { id = AccountTransaction.AccountId });
        }
    }
}
