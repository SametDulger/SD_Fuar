using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class TeklifController : Controller
    {
        private readonly SD_FuarContext _context;

        public TeklifController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teklifler = await _context.Teklifler
                .Include(t => t.Firma)
                .Include(t => t.Fuar)
                .Include(t => t.Salon)
                .Include(t => t.Stand)
                .ToListAsync();
            return View(teklifler);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teklif = await _context.Teklifler
                .Include(t => t.Firma)
                .Include(t => t.Fuar)
                .Include(t => t.Salon)
                .Include(t => t.Stand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teklif == null)
            {
                return NotFound();
            }

            return View(teklif);
        }

        public IActionResult Create()
        {
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad");
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad");
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad");
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmaId,FuarId,SalonId,StandId,TeklifTarihi,Metrekare,BirimFiyat,Doviz,VadeSayisi,OdemePlani,SonGecerlilikTarihi")] Teklif teklif)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teklif);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", teklif.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", teklif.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", teklif.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", teklif.StandId);
            return View(teklif);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teklif = await _context.Teklifler.FindAsync(id);
            if (teklif == null)
            {
                return NotFound();
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", teklif.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", teklif.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", teklif.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", teklif.StandId);
            return View(teklif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaId,FuarId,SalonId,StandId,TeklifTarihi,Metrekare,BirimFiyat,Doviz,VadeSayisi,OdemePlani,SonGecerlilikTarihi")] Teklif teklif)
        {
            if (id != teklif.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teklif);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeklifExists(teklif.Id))
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
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", teklif.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", teklif.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", teklif.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", teklif.StandId);
            return View(teklif);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teklif = await _context.Teklifler
                .Include(t => t.Firma)
                .Include(t => t.Fuar)
                .Include(t => t.Salon)
                .Include(t => t.Stand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teklif == null)
            {
                return NotFound();
            }

            return View(teklif);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teklif = await _context.Teklifler.FindAsync(id);
            if (teklif != null)
            {
                _context.Teklifler.Remove(teklif);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeklifExists(int id)
        {
            return _context.Teklifler.Any(e => e.Id == id);
        }
    }
} 