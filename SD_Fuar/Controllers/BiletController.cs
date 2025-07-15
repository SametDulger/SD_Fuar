using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;
using System.Text;

namespace SD_Fuar.Controllers
{
    public class BiletController : Controller
    {
        private readonly SD_FuarContext _context;

        public BiletController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Biletler
                .Include(b => b.Ziyaretci)
                .Include(b => b.Fuar)
                .OrderByDescending(b => b.SatisTarihi)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Biletler
                .Include(b => b.Ziyaretci)
                .Include(b => b.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,Fiyat,Doviz,GecerlilikTarihi,ZiyaretciId,FuarId,SatisPersoneli,OdemeTipi")] Bilet bilet)
        {
            if (ModelState.IsValid)
            {
                bilet.BiletKodu = GenerateBiletKodu();
                bilet.SatisTarihi = DateTime.Now;
                bilet.KullanildiMi = false;
                
                _context.Add(bilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(bilet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Biletler.FindAsync(id);
            if (bilet == null)
            {
                return NotFound();
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(bilet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BiletKodu,Tip,Fiyat,Doviz,SatisTarihi,GecerlilikTarihi,ZiyaretciId,FuarId,SatisPersoneli,KullanildiMi,KullanilmaTarihi,OdemeTipi")] Bilet bilet)
        {
            if (id != bilet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiletExists(bilet.Id))
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
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(bilet);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Biletler
                .Include(b => b.Ziyaretci)
                .Include(b => b.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bilet = await _context.Biletler.FindAsync(id);
            if (bilet != null)
            {
                _context.Biletler.Remove(bilet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Kullan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Biletler.FindAsync(id);
            if (bilet == null)
            {
                return NotFound();
            }

            bilet.KullanildiMi = true;
            bilet.KullanilmaTarihi = DateTime.Now;

            _context.Update(bilet);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SatisRaporu()
        {
            var rapor = await _context.Biletler
                .Include(b => b.Fuar)
                .GroupBy(b => new { b.Fuar.Ad, b.Tip, b.OdemeTipi })
                .Select(g => new
                {
                    FuarAdi = g.Key.Ad,
                    BiletTipi = g.Key.Tip,
                    OdemeTipi = g.Key.OdemeTipi,
                    SatisAdedi = g.Count(),
                    ToplamTutar = g.Sum(b => b.Fiyat)
                })
                .ToListAsync();

            return View(rapor);
        }

        private bool BiletExists(int id)
        {
            return _context.Biletler.Any(e => e.Id == id);
        }

        private string GenerateBiletKodu()
        {
            var random = new Random();
            var sb = new StringBuilder();
            sb.Append("BLT");
            for (int i = 0; i < 8; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            return sb.ToString();
        }
    }
} 