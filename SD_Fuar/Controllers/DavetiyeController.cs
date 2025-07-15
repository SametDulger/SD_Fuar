using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;
using System.Text;

namespace SD_Fuar.Controllers
{
    public class DavetiyeController : Controller
    {
        private readonly SD_FuarContext _context;

        public DavetiyeController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Davetiyeler
                .Include(d => d.Ziyaretci)
                .Include(d => d.Fuar)
                .OrderByDescending(d => d.OlusturmaTarihi)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var davetiye = await _context.Davetiyeler
                .Include(d => d.Ziyaretci)
                .Include(d => d.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (davetiye == null)
            {
                return NotFound();
            }

            return View(davetiye);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tip,ZiyaretciId,FuarId,GecerlilikTarihi,Notlar")] Davetiye davetiye)
        {
            if (ModelState.IsValid)
            {
                davetiye.DavetiyeKodu = GenerateDavetiyeKodu();
                davetiye.OlusturmaTarihi = DateTime.Now;
                davetiye.KullanildiMi = false;
                davetiye.Durum = "Aktif";
                davetiye.OlusturanKullanici = "Sistem";
                
                _context.Add(davetiye);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(davetiye);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var davetiye = await _context.Davetiyeler.FindAsync(id);
            if (davetiye == null)
            {
                return NotFound();
            }
            ViewData["Ziyaretciler"] = await _context.Ziyaretciler.Select(z => new { z.Id, Ad = $"{z.Ad} {z.Soyad}" }).ToListAsync();
            ViewData["Fuarlar"] = await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync();
            return View(davetiye);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DavetiyeKodu,Tip,ZiyaretciId,FuarId,OlusturmaTarihi,GecerlilikTarihi,KullanildiMi,KullanilmaTarihi,Durum,OlusturanKullanici,Notlar")] Davetiye davetiye)
        {
            if (id != davetiye.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(davetiye);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DavetiyeExists(davetiye.Id))
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
            return View(davetiye);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var davetiye = await _context.Davetiyeler
                .Include(d => d.Ziyaretci)
                .Include(d => d.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (davetiye == null)
            {
                return NotFound();
            }

            return View(davetiye);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var davetiye = await _context.Davetiyeler.FindAsync(id);
            if (davetiye != null)
            {
                _context.Davetiyeler.Remove(davetiye);
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

            var davetiye = await _context.Davetiyeler.FindAsync(id);
            if (davetiye == null)
            {
                return NotFound();
            }

            davetiye.KullanildiMi = true;
            davetiye.KullanilmaTarihi = DateTime.Now;
            davetiye.Durum = "Kullanıldı";

            _context.Update(davetiye);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnlineDavetiye()
        {
            ViewData["Fuarlar"] = await _context.Fuarlar.Where(f => f.BitisTarihi >= DateTime.Now).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnlineDavetiye([Bind("Ad,Soyad,FirmaAdi,Adres,Tel,Eposta,Faks,Meslek,FuarId")] Ziyaretci ziyaretci)
        {
            if (ModelState.IsValid)
            {
                ziyaretci.ZiyaretTarihi = DateTime.Now;
                _context.Add(ziyaretci);
                await _context.SaveChangesAsync();

                var davetiye = new Davetiye
                {
                    DavetiyeKodu = GenerateDavetiyeKodu(),
                    Tip = "Online",
                    ZiyaretciId = ziyaretci.Id,
                    FuarId = ziyaretci.FuarId,
                    OlusturmaTarihi = DateTime.Now,
                    GecerlilikTarihi = DateTime.Now.AddDays(30),
                    KullanildiMi = false,
                    Durum = "Aktif",
                    OlusturanKullanici = "Online Sistem"
                };

                _context.Add(davetiye);
                await _context.SaveChangesAsync();

                TempData["DavetiyeKodu"] = davetiye.DavetiyeKodu;
                return RedirectToAction(nameof(DavetiyeBasarili));
            }

            ViewData["Fuarlar"] = await _context.Fuarlar.Where(f => f.BitisTarihi >= DateTime.Now).ToListAsync();
            return View(ziyaretci);
        }

        public IActionResult DavetiyeBasarili()
        {
            return View();
        }

        private bool DavetiyeExists(int id)
        {
            return _context.Davetiyeler.Any(e => e.Id == id);
        }

        private string GenerateDavetiyeKodu()
        {
            var random = new Random();
            var sb = new StringBuilder();
            sb.Append("DVT");
            for (int i = 0; i < 8; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            return sb.ToString();
        }
    }
} 