using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountLedger.Web.Models;
using AccountLedger.Services;

namespace AccountLedger.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _service;

        public DetailsModel(IAccountService service)
        {
            _service = service;
        }

        public AccountModel AccountModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounmodel = await _service.Get(id.Value);

            if (accounmodel == null)
            {
                return NotFound();
            }
            else 
            {
                AccountModel = accounmodel;
            }
            return Page();
        }
    }
}
