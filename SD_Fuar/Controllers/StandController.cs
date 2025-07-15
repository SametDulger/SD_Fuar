using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class StandController : Controller
    {
        private readonly SD_FuarContext _context;

        public StandController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var standlar = await _context.Standlar
                .Include(s => s.Salon)
                .ThenInclude(s => s.Fuar)
                .ToListAsync();
            return View(standlar);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stand = await _context.Standlar
                .Include(s => s.Salon)
                .ThenInclude(s => s.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stand == null)
            {
                return NotFound();
            }

            return View(stand);
        }

        public IActionResult Create()
        {
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kod,En,Boy,Metrekare,SalonId")] Stand stand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", stand.SalonId);
            return View(stand);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stand = await _context.Standlar.FindAsync(id);
            if (stand == null)
            {
                return NotFound();
            }
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", stand.SalonId);
            return View(stand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kod,En,Boy,Metrekare,SalonId")] Stand stand)
        {
            if (id != stand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandExists(stand.Id))
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
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", stand.SalonId);
            return View(stand);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stand = await _context.Standlar
                .Include(s => s.Salon)
                .ThenInclude(s => s.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stand == null)
            {
                return NotFound();
            }

            return View(stand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stand = await _context.Standlar.FindAsync(id);
            if (stand != null)
            {
                _context.Standlar.Remove(stand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandExists(int id)
        {
            return _context.Standlar.Any(e => e.Id == id);
        }
    }
} 