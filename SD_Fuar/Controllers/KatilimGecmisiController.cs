using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class KatilimGecmisiController : Controller
    {
        private readonly SD_FuarContext _context;

        public KatilimGecmisiController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.KatilimGecmisleri
                .Include(k => k.Firma)
                .Include(k => k.Fuar)
                .OrderByDescending(k => k.Yil)
                .ThenBy(k => k.Firma.Ad)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katilimGecmisi = await _context.KatilimGecmisleri
                .Include(k => k.Firma)
                .Include(k => k.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (katilimGecmisi == null)
            {
                return NotFound();
            }

            return View(katilimGecmisi);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Firmalar"] = await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirmaId,FuarId,Yil,KullanilanMetrekare,ToplamTutar,Doviz,StandTipi,SalonAdi,StandKodu,OdendiMi,OdemeTarihi,Notlar")] KatilimGecmisi katilimGecmisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(katilimGecmisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Firmalar"] = await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(katilimGecmisi);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katilimGecmisi = await _context.KatilimGecmisleri.FindAsync(id);
            if (katilimGecmisi == null)
            {
                return NotFound();
            }
            ViewData["Firmalar"] = await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(katilimGecmisi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaId,FuarId,Yil,KullanilanMetrekare,ToplamTutar,Doviz,StandTipi,SalonAdi,StandKodu,OdendiMi,OdemeTarihi,Notlar")] KatilimGecmisi katilimGecmisi)
        {
            if (id != katilimGecmisi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(katilimGecmisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KatilimGecmisiExists(katilimGecmisi.Id))
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
            ViewData["Firmalar"] = await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(katilimGecmisi);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katilimGecmisi = await _context.KatilimGecmisleri
                .Include(k => k.Firma)
                .Include(k => k.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (katilimGecmisi == null)
            {
                return NotFound();
            }

            return View(katilimGecmisi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var katilimGecmisi = await _context.KatilimGecmisleri.FindAsync(id);
            if (katilimGecmisi != null)
            {
                _context.KatilimGecmisleri.Remove(katilimGecmisi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FirmaGecmisi(int firmaId)
        {
            var gecmis = await _context.KatilimGecmisleri
                .Include(k => k.Fuar)
                .Where(k => k.FirmaId == firmaId)
                .OrderByDescending(k => k.Yil)
                .ThenBy(k => k.Fuar.Ad)
                .ToListAsync();

            var firma = await _context.Firmalar.FindAsync(firmaId);
            ViewData["Firma"] = firma;

            return View(gecmis);
        }

        public async Task<IActionResult> FuarGecmisi(int fuarId)
        {
            var gecmis = await _context.KatilimGecmisleri
                .Include(k => k.Firma)
                .Where(k => k.FuarId == fuarId)
                .OrderBy(k => k.Firma.Ad)
                .ThenByDescending(k => k.Yil)
                .ToListAsync();

            var fuar = await _context.Fuarlar.FindAsync(fuarId);
            ViewData["Fuar"] = fuar;

            return View(gecmis);
        }

        private bool KatilimGecmisiExists(int id)
        {
            return _context.KatilimGecmisleri.Any(e => e.Id == id);
        }
    }
} 