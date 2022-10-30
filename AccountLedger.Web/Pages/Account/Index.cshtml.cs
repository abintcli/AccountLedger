using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountLedger.Web.Models;
using AccountLedger.Web.Services;
using AccountLedger.Services;

namespace AccountLedger.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _service;

        public IndexModel(IAccountService service)
        {
            _service = service;
        }

        public IList<AccountModel> Accounts { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Accounts = await _service.GetAll();
        }
    }
}
