using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;
using System.Text;

namespace SD_Fuar.Controllers
{
    public class BarkodController : Controller
    {
        private readonly SD_FuarContext _context;

        public BarkodController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Barkodlar
                .Include(b => b.Ziyaretci)
                .OrderByDescending(b => b.OlusturmaTarihi)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barkod = await _context.Barkodlar
                .Include(b => b.Ziyaretci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barkod == null)
            {
                return NotFound();
            }

            return View(barkod);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,ZiyaretciId,GecerlilikTarihi")] Barkod barkod)
        {
            if (ModelState.IsValid)
            {
                barkod.BarkodKodu = GenerateBarkod();
                barkod.OlusturmaTarihi = DateTime.Now;
                barkod.KullanildiMi = false;
                barkod.Durum = "Aktif";
                
                _context.Add(barkod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            return View(barkod);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barkod = await _context.Barkodlar.FindAsync(id);
            if (barkod == null)
            {
                return NotFound();
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            return View(barkod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BarkodKodu,Tip,ZiyaretciId,OlusturmaTarihi,GecerlilikTarihi,KullanildiMi,KullanilmaTarihi,Durum")] Barkod barkod)
        {
            if (id != barkod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barkod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarkodExists(barkod.Id))
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
            return View(barkod);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barkod = await _context.Barkodlar
                .Include(b => b.Ziyaretci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barkod == null)
            {
                return NotFound();
            }

            return View(barkod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barkod = await _context.Barkodlar.FindAsync(id);
            if (barkod != null)
            {
                _context.Barkodlar.Remove(barkod);
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

            var barkod = await _context.Barkodlar.FindAsync(id);
            if (barkod == null)
            {
                return NotFound();
            }

            barkod.KullanildiMi = true;
            barkod.KullanilmaTarihi = DateTime.Now;
            barkod.Durum = "Kullanıldı";

            _context.Update(barkod);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool BarkodExists(int id)
        {
            return _context.Barkodlar.Any(e => e.Id == id);
        }

        private string GenerateBarkod()
        {
            var random = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            return sb.ToString();
        }
    }
} 