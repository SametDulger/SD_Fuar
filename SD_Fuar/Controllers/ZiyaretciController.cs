using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class ZiyaretciController : Controller
    {
        private readonly SD_FuarContext _context;

        public ZiyaretciController(SD_FuarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ziyaretciler = await _context.Ziyaretciler
                .Include(z => z.Fuar)
                .ToListAsync();
            return View(ziyaretciler);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ziyaretci = await _context.Ziyaretciler
                .Include(z => z.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ziyaretci == null)
            {
                return NotFound();
            }

            return View(ziyaretci);
        }

        public IActionResult Create()
        {
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Soyad,FirmaAdi,Adres,Tel,Eposta,Faks,Meslek,ZiyaretTarihi,Notlar,FuarId")] Ziyaretci ziyaretci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ziyaretci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", ziyaretci.FuarId);
            return View(ziyaretci);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ziyaretci = await _context.Ziyaretciler.FindAsync(id);
            if (ziyaretci == null)
            {
                return NotFound();
            }
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", ziyaretci.FuarId);
            return View(ziyaretci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,FirmaAdi,Adres,Tel,Eposta,Faks,Meslek,ZiyaretTarihi,Notlar,FuarId")] Ziyaretci ziyaretci)
        {
            if (id != ziyaretci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ziyaretci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZiyaretciExists(ziyaretci.Id))
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
            ViewData["FuarId"] = new SelectList(_context.Fuarlar, "Id", "Ad", ziyaretci.FuarId);
            return View(ziyaretci);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ziyaretci = await _context.Ziyaretciler
                .Include(z => z.Fuar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ziyaretci == null)
            {
                return NotFound();
            }

            return View(ziyaretci);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ziyaretci = await _context.Ziyaretciler.FindAsync(id);
            if (ziyaretci != null)
            {
                _context.Ziyaretciler.Remove(ziyaretci);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZiyaretciExists(int id)
        {
            return _context.Ziyaretciler.Any(e => e.Id == id);
        }
    }
} 