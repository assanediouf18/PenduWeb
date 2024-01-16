using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pendu.Models;

namespace Pendu.Data
{
    public class PenduContext : DbContext
    {
        public PenduContext (DbContextOptions<PenduContext> options)
            : base(options)
        {
        }

        public DbSet<Pendu.Models.PenduModel> PenduModel { get; set; } = default!;
    }
}
