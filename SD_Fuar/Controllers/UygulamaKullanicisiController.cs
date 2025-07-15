using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class UygulamaKullanicisiController : Controller
    {
        private readonly SD_FuarContext _context;

        public UygulamaKullanicisiController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.UygulamaKullanicilari.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uygulamaKullanicisi = await _context.UygulamaKullanicilari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uygulamaKullanicisi == null)
            {
                return NotFound();
            }

            return View(uygulamaKullanicisi);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KullaniciAdi,Sifre,Rol")] UygulamaKullanicisi uygulamaKullanicisi)
        {
            if (ModelState.IsValid)
            {
                uygulamaKullanicisi.SifreHashle();
                
                _context.Add(uygulamaKullanicisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uygulamaKullanicisi);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uygulamaKullanicisi = await _context.UygulamaKullanicilari.FindAsync(id);
            if (uygulamaKullanicisi == null)
            {
                return NotFound();
            }
            return View(uygulamaKullanicisi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KullaniciAdi,Sifre,Rol")] UygulamaKullanicisi uygulamaKullanicisi)
        {
            if (id != uygulamaKullanicisi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uygulamaKullanicisi.SifreHashle();
                    
                    _context.Update(uygulamaKullanicisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UygulamaKullanicisiExists(uygulamaKullanicisi.Id))
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
            return View(uygulamaKullanicisi);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uygulamaKullanicisi = await _context.UygulamaKullanicilari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uygulamaKullanicisi == null)
            {
                return NotFound();
            }

            return View(uygulamaKullanicisi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uygulamaKullanicisi = await _context.UygulamaKullanicilari.FindAsync(id);
            if (uygulamaKullanicisi != null)
            {
                _context.UygulamaKullanicilari.Remove(uygulamaKullanicisi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UygulamaKullanicisiExists(int id)
        {
            return _context.UygulamaKullanicilari.Any(e => e.Id == id);
        }
    }
} 