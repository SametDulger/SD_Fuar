using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class FuarController : BaseController
    {
        public FuarController(SD_FuarContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Fuarlar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var fuar = await GetEntityByIdAsync<Fuar>(id);
            if (fuar == null)
            {
                return NotFound();
            }

            return View(fuar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ad,Ulke,Sehir,BaslangicTarihi,BitisTarihi")] Fuar fuar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuar);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var fuar = await GetEntityByIdAsync<Fuar>(id);
            if (fuar == null)
            {
                return NotFound();
            }
            return View(fuar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Ulke,Sehir,BaslangicTarihi,BitisTarihi")] Fuar fuar)
        {
            if (id != fuar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists<Fuar>(fuar.Id))
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
            return View(fuar);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var fuar = await GetEntityByIdAsync<Fuar>(id);
            if (fuar == null)
            {
                return NotFound();
            }

            return View(fuar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuar = await GetEntityByIdAsync<Fuar>(id);
            if (fuar != null)
            {
                _context.Fuarlar.Remove(fuar);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
} 