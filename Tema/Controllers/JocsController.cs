using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tema.Data;
using Tema.Models;

namespace Tema.Controllers
{
    public class JocsController : Controller
    {
        private readonly TemaContext _context;

        public JocsController(TemaContext context)
        {
            _context = context;
        }

        // GET: Jocs
        public async Task<IActionResult> Index(string sortOrder, string searchString,string currentFilter, int pageNumber=1)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            

            var jocuri = from s in _context.Joc
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                jocuri = jocuri.Where(s => s.Titlu.Contains(searchString)
                                     );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    jocuri = jocuri.OrderByDescending(s => s.Titlu);
                    break;
                case "Date":
                    jocuri = jocuri.OrderBy(s => s.DataLansarii);
                    break;
                case "date_desc":
                    jocuri = jocuri.OrderByDescending(s => s.DataLansarii);
                    break;
                default:
                    jocuri = jocuri.OrderBy(s => s.Titlu);
                    break;
            }

            int pageSize = 3;
            //returneaza lista paginta pentru paginare
            return View(await PaginatedList<Joc>.CreateAsync(jocuri.AsNoTracking(), pageNumber , pageSize));

            /* return _context.Joc != null ?
                          View(await _context.Joc.ToListAsync()) :
                          Problem("Entity set 'TemaContext.Joc'  is null.");*/
        }

     


        // GET: Jocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Joc == null)
            {
                return NotFound();
            }

            var joc = await _context.Joc
                .FirstOrDefaultAsync(m => m.Id == id);

            if (joc == null)
            {
                return NotFound();
            }
            // aici faci
            var model1Data = joc;// Get data for Model1
          
            var model3Data = new Reviews();
            var model2Data = new Reviews();
            List<Reviews> p=new List<Reviews>(); // Get data for Model2
            
           p= await _context.Reviews
        .Where(r => r.IdJoc == id)
        .ToListAsync();

            //p.Add(model2Data);
            IEnumerable<Reviews> r = p;
            /*returneaza model triplu : Jocuri IEnumerable de reviews pentru afisare lista de reviewuri si
            review pentru adaugare review*/
            var combinedData = (model1Data, r,model3Data);

            return View(combinedData);

        }
        //functie adaugare review
        [HttpPost]
        public async Task<IActionResult> AddReview(int id, string Autor, string Review)
        {
            if (string.IsNullOrEmpty(Autor) || string.IsNullOrEmpty(Review))
            {
                ModelState.AddModelError("", "Please enter both Autor and Review.");
                return RedirectToAction("Details", new { id });
            }

            var review = new Reviews
            {
                IdJoc = id,
                Autor = Autor,
                Review = Review,
                DataCompletarii = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            //redirectioneaza catre details dupa ce adauga review
            return RedirectToAction("Details", new { id });
        }

        // GET: Jocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titlu,DataLansarii,Gen,Pret")] Joc joc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(joc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(joc);
        }

        // GET: Jocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Joc == null)
            {
                return NotFound();
            }

            var joc = await _context.Joc.FindAsync(id);
            if (joc == null)
            {
                return NotFound();
            }
            
            return View(joc);
        }

        // POST: Jocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titlu,DataLansarii,Gen,Pret")] Joc joc)
        {
            if (id != joc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(joc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JocExists(joc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
         
            return View(joc);
        }

        // GET: Jocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Joc == null)
            {
                return NotFound();
            }

            var joc = await _context.Joc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (joc == null)
            {
                return NotFound();
            }

            return View(joc);
        }

        // POST: Jocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Joc == null)
            {
                return Problem("Entity set 'TemaContext.Joc'  is null.");
            }
            var joc = await _context.Joc.FindAsync(id);
            if (joc != null)
            {
                _context.Joc.Remove(joc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JocExists(int id)
        {
          return (_context.Joc?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
