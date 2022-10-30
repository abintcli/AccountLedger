using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountLedger.Web.Models;
using AccountLedger.Services;

namespace AccountLedger.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _service;

        public EditModel(IAccountService service)
        {
            _service = service;
        }

        [BindProperty]
        public AccountModel AccountModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _service.Get(id.Value);
            if (accountModel == null)
            {
                return NotFound();
            }
            AccountModel = accountModel;
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

            await _service.Update(AccountModel, AccountModel.Id);

            return RedirectToPage("./Index");
        }

        //private bool AccounModelExists(int id)
        //{
        //  return _context.Account.Any(e => e.Id == id);
        //}
    }
}
