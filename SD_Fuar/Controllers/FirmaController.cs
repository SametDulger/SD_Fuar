using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class FirmaController : BaseController
    {
        public FirmaController(SD_FuarContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Firmalar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var firma = await GetEntityByIdAsync<Firma>(id);
            if (firma == null)
            {
                return NotFound();
            }

            return View(firma);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Yetkili,Unvan,Tel,Dahili,DirektTel,Eposta,Faks,Adres,Sehir,Ulke,Sektor")] Firma firma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firma);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var firma = await GetEntityByIdAsync<Firma>(id);
            if (firma == null)
            {
                return NotFound();
            }
            return View(firma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Yetkili,Unvan,Tel,Dahili,DirektTel,Eposta,Faks,Adres,Sehir,Ulke,Sektor")] Firma firma)
        {
            if (id != firma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists<Firma>(firma.Id))
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
            return View(firma);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var firma = await GetEntityByIdAsync<Firma>(id);
            if (firma == null)
            {
                return NotFound();
            }

            return View(firma);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firma = await GetEntityByIdAsync<Firma>(id);
            if (firma != null)
            {
                _context.Firmalar.Remove(firma);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 