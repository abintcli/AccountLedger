using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountLedger.Models;
using AccountLedger.Web.Services;

namespace AccountLedger.Pages.Account.Transaction
{
    public class CreateModel : PageModel
    {
        private readonly ITransactionService _service;

        public CreateModel(ITransactionService service)
        {
            _service = service;
        }

        public int AccountId { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            //AccountTransaction.AccountId = id.Value;
            AccountId = id.Value;
            AccountTransaction = new AccountTransaction { AccountId = AccountId };

            return Page();
        }

        [BindProperty]
        public AccountTransaction AccountTransaction { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //AccountTransaction.AccountId = AccountId;
            //need some error handling
            await _service.Add(AccountTransaction);

            return RedirectToPage("./Index", new { id = AccountTransaction.AccountId });
        }
    }
}
