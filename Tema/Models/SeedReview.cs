using Microsoft.EntityFrameworkCore;
using Tema.Data;

namespace Tema.Models
{
    public class SeedReview
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TemaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TemaContext>>()))
            {
                // Look for any movies.
                if (context.Reviews.Any())
                {
                    return;   // DB has been seeded
                }

                context.Reviews.AddRange(
                    new Reviews
                    {
                        Autor = "Gigi",
                        DataCompletarii = DateTime.Parse("1997-2-12"),
                        IdJoc = 1,
                        Review="Bun"
                    },

                  new Reviews
                  {
                      Autor = "Coman",
                      DataCompletarii = DateTime.Parse("1993-2-12"),
                      IdJoc = 1,
                      Review = "Tzais"
                  }
                );
                context.SaveChanges();
            }
        }
    }
}
