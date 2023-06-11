using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tema.Models;

namespace Tema.Data
{
    public class TemaContext : DbContext
    {
        public TemaContext (DbContextOptions<TemaContext> options)
            : base(options)
        {
        }

        public DbSet<Tema.Models.Joc> Joc { get; set; } = default!;

        public DbSet<Tema.Models.Reviews>? Reviews { get; set; }
    }
}
