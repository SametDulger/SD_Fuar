using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class MuhasebeController : Controller
    {
        private readonly SD_FuarContext _context;

        public MuhasebeController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var muhasebeKayitlari = await _context.MuhasebeKayitlari
                .Include(m => m.Sozlesme)
                .ToListAsync();
            return View(muhasebeKayitlari);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muhasebeKaydi = await _context.MuhasebeKayitlari
                .Include(m => m.Sozlesme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muhasebeKaydi == null)
            {
                return NotFound();
            }

            return View(muhasebeKaydi);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SozlesmeId"] = new SelectList(await _context.Sozlesmeler.Select(s => new { s.Id, SozlesmeAdi = $"Sözleşme {s.Id} - {s.Firma?.Ad}" }).ToListAsync(), "Id", "SozlesmeAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SozlesmeId,Tarih,Aciklama,Tutar,Doviz,OdemeTarihi,OdemeTipi,KalanBakiye,Komisyon,KDV,KurumlarVergisi,YedekAkce")] MuhasebeKaydi muhasebeKaydi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muhasebeKaydi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SozlesmeId"] = new SelectList(_context.Sozlesmeler, "Id", "Id", muhasebeKaydi.SozlesmeId);
            return View(muhasebeKaydi);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muhasebeKaydi = await _context.MuhasebeKayitlari.FindAsync(id);
            if (muhasebeKaydi == null)
            {
                return NotFound();
            }
            ViewData["SozlesmeId"] = new SelectList(_context.Sozlesmeler, "Id", "Id", muhasebeKaydi.SozlesmeId);
            return View(muhasebeKaydi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SozlesmeId,Tarih,Aciklama,Tutar,Doviz,OdemeTarihi,OdemeTipi,KalanBakiye,Komisyon,KDV,KurumlarVergisi,YedekAkce")] MuhasebeKaydi muhasebeKaydi)
        {
            if (id != muhasebeKaydi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muhasebeKaydi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuhasebeKaydiExists(muhasebeKaydi.Id))
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
            ViewData["SozlesmeId"] = new SelectList(_context.Sozlesmeler, "Id", "Id", muhasebeKaydi.SozlesmeId);
            return View(muhasebeKaydi);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muhasebeKaydi = await _context.MuhasebeKayitlari
                .Include(m => m.Sozlesme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muhasebeKaydi == null)
            {
                return NotFound();
            }

            return View(muhasebeKaydi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muhasebeKaydi = await _context.MuhasebeKayitlari.FindAsync(id);
            if (muhasebeKaydi != null)
            {
                _context.MuhasebeKayitlari.Remove(muhasebeKaydi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuhasebeKaydiExists(int id)
        {
            return _context.MuhasebeKayitlari.Any(e => e.Id == id);
        }
    }
} 