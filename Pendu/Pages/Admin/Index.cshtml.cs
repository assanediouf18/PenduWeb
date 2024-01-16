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
    public class IndexModel : PageModel
    {
        private readonly Pendu.Data.PenduContext _context;

        public IndexModel(Pendu.Data.PenduContext context)
        {
            _context = context;
        }

        public IList<PenduModel> PenduModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PenduModel = await _context.PenduModel.ToListAsync();
        }
    }
}
