using Microsoft.EntityFrameworkCore;
using Tema.Data;

namespace Tema.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TemaContext>>()))
            {
                // Look for any movies.
                if (context.Joc.Any())
                {
                    return;   // DB has been seeded
                }

                context.Joc.AddRange(
                    new Joc
                    {
                        Titlu = "Gran Turismo",
                        DataLansarii = DateTime.Parse("1997-2-12"),
                        Gen = "Driving Sim",
                        Pret = 7.99M
                    },

                    new Joc
                    {
                        Titlu = "Fifa 98 ",
                        DataLansarii = DateTime.Parse("1998-3-13"),
                        Gen = "Sport",
                        Pret = 8.99M
                    },

                    new Joc
                    {
                        Titlu = "Star Fox",
                        DataLansarii = DateTime.Parse("1996-2-23"),
                        Gen = "Sim",
                        Pret = 9.99M
                    },

                    new Joc
                    {
                        Titlu = "Mace",
                        DataLansarii = DateTime.Parse("1997-4-15"),
                        Gen = "Fighting",
                        Pret = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
