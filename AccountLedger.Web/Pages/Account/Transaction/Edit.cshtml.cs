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
    public class EditModel : PageModel
    {
        private readonly ITransactionService _service;

        public EditModel(ITransactionService service)
        {
            _service = service;
        }
        public int AccountId { get; set; } = 0;

        [BindProperty]
        public AccountTransaction AccountTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var accounttransaction =  await _context.AccountTransaction.FirstOrDefaultAsync(m => m.Id == id);
            var accounttransaction =  await _service.Get(id.Value);
            //AccountId = AccountTransaction.AccountId;

            if (accounttransaction == null)
            {
                return NotFound();
            }

            AccountTransaction = accounttransaction;
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Update(AccountTransaction, AccountTransaction.Id);

            return RedirectToPage("./Index", new { id = AccountTransaction.AccountId });
        }

        //private bool AccountTransactionExists(int id)
        //{
        //  return _context.AccountTransaction.Any(e => e.Id == id);
        //}
    }
}
