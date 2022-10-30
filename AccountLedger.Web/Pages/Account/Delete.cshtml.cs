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
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _service;

        public DeleteModel(IAccountService service)
        {
            _service = service;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accounmodel = await _service.Delete(id.Value);
            //var accounmodel = await _context.Account.FindAsync(id);

            if (accounmodel != false)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
