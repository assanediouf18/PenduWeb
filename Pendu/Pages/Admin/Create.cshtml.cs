using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pendu.Data;
using Pendu.Models;

namespace Pendu.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly Pendu.Data.PenduContext _context;

        public CreateModel(Pendu.Data.PenduContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PenduModel PenduModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PenduModel.Add(PenduModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
