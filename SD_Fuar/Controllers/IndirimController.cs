using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class IndirimController : Controller
    {
        private readonly SD_FuarContext _context;

        public IndirimController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Indirimler
                .Include(i => i.Firma)
                .Include(i => i.Fuar)
                .OrderByDescending(i => i.BaslangicTarihi)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirim = await _context.Indirimler
                .Include(i => i.Firma)
                .Include(i => i.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indirim == null)
            {
                return NotFound();
            }

            return View(indirim);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["FirmaId"] = new SelectList(await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad");
            ViewData["FuarId"] = new SelectList(await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Tip,Oran,SabitTutar,FirmaId,FuarId,BaslangicTarihi,BitisTarihi,AktifMi,Aciklama,UygulamaKosullari,MinimumKatilimSayisi,MinimumTutar")] Indirim indirim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indirim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirmaId"] = new SelectList(await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FirmaId);
            ViewData["FuarId"] = new SelectList(await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FuarId);
            return View(indirim);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirim = await _context.Indirimler.FindAsync(id);
            if (indirim == null)
            {
                return NotFound();
            }
            ViewData["FirmaId"] = new SelectList(await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FirmaId);
            ViewData["FuarId"] = new SelectList(await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FuarId);
            return View(indirim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Tip,Oran,SabitTutar,FirmaId,FuarId,BaslangicTarihi,BitisTarihi,AktifMi,Aciklama,UygulamaKosullari,MinimumKatilimSayisi,MinimumTutar")] Indirim indirim)
        {
            if (id != indirim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indirim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndirimExists(indirim.Id))
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
            ViewData["FirmaId"] = new SelectList(await _context.Firmalar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FirmaId);
            ViewData["FuarId"] = new SelectList(await _context.Fuarlar.Select(f => new { f.Id, f.Ad }).ToListAsync(), "Id", "Ad", indirim.FuarId);
            return View(indirim);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indirim = await _context.Indirimler
                .Include(i => i.Firma)
                .Include(i => i.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indirim == null)
            {
                return NotFound();
            }

            return View(indirim);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indirim = await _context.Indirimler.FindAsync(id);
            if (indirim != null)
            {
                _context.Indirimler.Remove(indirim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> HesaplaIndirim(int firmaId, int fuarId, decimal tutar, double metrekare)
        {
            var indirimler = await _context.Indirimler
                .Where(i => i.AktifMi && 
                           i.BaslangicTarihi <= DateTime.Now && 
                           i.BitisTarihi >= DateTime.Now &&
                           (i.FirmaId == firmaId || i.FirmaId == null) &&
                           (i.FuarId == fuarId || i.FuarId == null))
                .ToListAsync();

            var katilimGecmisi = await _context.KatilimGecmisleri
                .Where(k => k.FirmaId == firmaId && k.FuarId == fuarId)
                .CountAsync();

            decimal toplamIndirim = 0;
            var uygulananIndirimler = new List<object>();

            foreach (var indirim in indirimler)
            {
                if (katilimGecmisi >= indirim.MinimumKatilimSayisi && tutar >= indirim.MinimumTutar)
                {
                    decimal indirimTutari = 0;
                    if (indirim.Tip == "Yüzde")
                    {
                        indirimTutari = tutar * (decimal)(indirim.Oran / 100);
                    }
                    else if (indirim.Tip == "Sabit Tutar")
                    {
                        indirimTutari = indirim.SabitTutar;
                    }

                    toplamIndirim += indirimTutari;
                    uygulananIndirimler.Add(new
                    {
                        Ad = indirim.Ad,
                        Tip = indirim.Tip,
                        Oran = indirim.Oran,
                        SabitTutar = indirim.SabitTutar,
                        IndirimTutari = indirimTutari
                    });
                }
            }

            return Json(new
            {
                ToplamIndirim = toplamIndirim,
                IndirimliTutar = tutar - toplamIndirim,
                UygulananIndirimler = uygulananIndirimler
            });
        }

        private bool IndirimExists(int id)
        {
            return _context.Indirimler.Any(e => e.Id == id);
        }
    }
} 