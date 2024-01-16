using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pendu.Data;
using Pendu.Models;

namespace Pendu.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly Pendu.Data.PenduContext _context;

        public DetailsModel(Pendu.Data.PenduContext context)
        {
            _context = context;
        }

        public PenduModel PenduModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendumodel = await _context.PenduModel.FirstOrDefaultAsync(m => m.Id == id);
            if (pendumodel == null)
            {
                return NotFound();
            }
            else
            {
                PenduModel = pendumodel;
            }
            return Page();
        }
    }
}
