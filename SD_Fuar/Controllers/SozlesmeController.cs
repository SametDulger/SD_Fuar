using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class SozlesmeController : Controller
    {
        private readonly SD_FuarContext _context;

        public SozlesmeController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sozlesmeler = await _context.Sozlesmeler
                .Include(s => s.Firma)
                .Include(s => s.Fuar)
                .Include(s => s.Salon)
                .Include(s => s.Stand)
                .ToListAsync();
            return View(sozlesmeler);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sozlesme = await _context.Sozlesmeler
                .Include(s => s.Firma)
                .Include(s => s.Fuar)
                .Include(s => s.Salon)
                .Include(s => s.Stand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sozlesme == null)
            {
                return NotFound();
            }

            return View(sozlesme);
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
        public async Task<IActionResult> Create([Bind("FirmaId,FuarId,SalonId,StandId,Tarih,SozlesmeTipi,StandTipi,Metrekare,BirimFiyat,Doviz,DovizKuru,IndirimOrani,VadeSayisi,OdemePlani,SonGecerlilikTarihi,ImzaDurumu,UcretsizVerilenMetrekare")] Sozlesme sozlesme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sozlesme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", sozlesme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", sozlesme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", sozlesme.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", sozlesme.StandId);
            return View(sozlesme);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sozlesme = await _context.Sozlesmeler.FindAsync(id);
            if (sozlesme == null)
            {
                return NotFound();
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", sozlesme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", sozlesme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", sozlesme.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", sozlesme.StandId);
            return View(sozlesme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaId,FuarId,SalonId,StandId,Tarih,SozlesmeTipi,StandTipi,Metrekare,BirimFiyat,Doviz,DovizKuru,IndirimOrani,VadeSayisi,OdemePlani,SonGecerlilikTarihi,ImzaDurumu,UcretsizVerilenMetrekare")] Sozlesme sozlesme)
        {
            if (id != sozlesme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sozlesme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SozlesmeExists(sozlesme.Id))
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
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Ad", sozlesme.FirmaId);
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", sozlesme.FuarId);
            ViewData["SalonId"] = new SelectList(_context.Salonlar, "Id", "Ad", sozlesme.SalonId);
            ViewData["StandId"] = new SelectList(_context.Standlar, "Id", "Kod", sozlesme.StandId);
            return View(sozlesme);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sozlesme = await _context.Sozlesmeler
                .Include(s => s.Firma)
                .Include(s => s.Fuar)
                .Include(s => s.Salon)
                .Include(s => s.Stand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sozlesme == null)
            {
                return NotFound();
            }

            return View(sozlesme);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sozlesme = await _context.Sozlesmeler.FindAsync(id);
            if (sozlesme != null)
            {
                _context.Sozlesmeler.Remove(sozlesme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SozlesmeExists(int id)
        {
            return _context.Sozlesmeler.Any(e => e.Id == id);
        }
    }
} 