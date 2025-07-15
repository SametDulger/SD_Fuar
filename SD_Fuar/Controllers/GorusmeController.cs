using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class GorusmeController : Controller
    {
        private readonly SD_FuarContext _context;

        public GorusmeController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gorusmeler = await _context.Gorusmeler
                .Include(g => g.Firma)
                .Include(g => g.Fuar)
                .Include(g => g.Salon)
                .ToListAsync();
            return View(gorusmeler);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gorusme = await _context.Gorusmeler
                .Include(g => g.Firma)
                .Include(g => g.Fuar)
                .Include(g => g.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gorusme == null)
            {
                return NotFound();
            }

            return View(gorusme);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["FirmaId"] = new SelectList(await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad");
            ViewData["FuarId"] = new SelectList(await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad");
            ViewData["SalonId"] = new SelectList(await _context.Salonlar.Select(s => new { s.Id, s.Ad }).ToListAsync(), "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmaId,FuarId,SalonId,GorusenKisi,Tarih,Notlar")] Gorusme gorusme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gorusme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", gorusme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", gorusme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", gorusme.SalonId);
            return View(gorusme);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gorusme = await _context.Gorusmeler.FindAsync(id);
            if (gorusme == null)
            {
                return NotFound();
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", gorusme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", gorusme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", gorusme.SalonId);
            return View(gorusme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaId,FuarId,SalonId,GorusenKisi,Tarih,Notlar")] Gorusme gorusme)
        {
            if (id != gorusme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gorusme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GorusmeExists(gorusme.Id))
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
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", gorusme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", gorusme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", gorusme.SalonId);
            return View(gorusme);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gorusme = await _context.Gorusmeler
                .Include(g => g.Firma)
                .Include(g => g.Fuar)
                .Include(g => g.Salon)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gorusme == null)
            {
                return NotFound();
            }

            return View(gorusme);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gorusme = await _context.Gorusmeler.FindAsync(id);
            if (gorusme != null)
            {
                _context.Gorusmeler.Remove(gorusme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GorusmeExists(int id)
        {
            return _context.Gorusmeler.Any(e => e.Id == id);
        }
    }
} 