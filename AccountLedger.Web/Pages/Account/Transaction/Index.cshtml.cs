using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountLedger.Models;
using AccountLedger.Services;
using AccountLedger.Web.Services;

namespace AccountLedger.Pages.Account.Transaction
{
    public class IndexModel : PageModel
    {
        private readonly ITransactionService _service;

        public IndexModel(ITransactionService service)
        {
            _service = service;
        }
        public int AccountId { get; set; } = 0;
        public IList<AccountTransaction> Transactions { get; set; } = default!;

        public decimal Total { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            AccountId = id.Value;
            Transactions = await _service.GetAll(AccountId);
            Total = Transactions.Sum(t=> t.Type == TransactionType.Credit ? t.Amount : t.Amount * -1);

            return Page();
        }
    }
}
