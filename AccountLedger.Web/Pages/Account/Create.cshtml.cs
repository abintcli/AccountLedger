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
    public class CreateModel : PageModel
    {
        private readonly IAccountService _service;

        public CreateModel(IAccountService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountModel AccountModel { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Add(AccountModel);

            return RedirectToPage("./Index");
        }
    }
}
