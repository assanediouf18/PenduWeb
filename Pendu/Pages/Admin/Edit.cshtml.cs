using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pendu.Data;
using Pendu.Models;

namespace Pendu.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly Pendu.Data.PenduContext _context;

        public EditModel(Pendu.Data.PenduContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PenduModel PenduModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendumodel =  await _context.PenduModel.FirstOrDefaultAsync(m => m.Id == id);
            if (pendumodel == null)
            {
                return NotFound();
            }
            PenduModel = pendumodel;
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

            _context.Attach(PenduModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenduModelExists(PenduModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PenduModelExists(int id)
        {
            return _context.PenduModel.Any(e => e.Id == id);
        }
    }
}
