using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class SalonController : Controller
    {
        private readonly SD_FuarContext _context;

        public SalonController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salonlar = await _context.Salonlar
                .Include(s => s.Fuar)
                .ToListAsync();
            return View(salonlar);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salonlar
                .Include(s => s.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        public IActionResult Create()
        {
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,FuarId")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", salon.FuarId);
            return View(salon);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salonlar.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", salon.FuarId);
            return View(salon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,FuarId")] Salon salon)
        {
            if (id != salon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonExists(salon.Id))
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
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", salon.FuarId);
            return View(salon);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salonlar
                .Include(s => s.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await _context.Salonlar.FindAsync(id);
            if (salon != null)
            {
                _context.Salonlar.Remove(salon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(int id)
        {
            return _context.Salonlar.Any(e => e.Id == id);
        }
    }
} 