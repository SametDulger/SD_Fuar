using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public class RaporController : Controller
    {
        private readonly SD_FuarContext _context;

        public RaporController(SD_FuarContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SatisRaporu()
        {
            var satisRaporu = await _context.Sozlesmeler
                .Include(s => s.Firma)
                .Include(s => s.Fuar)
                .OrderByDescending(s => s.Tarih)
                .ToListAsync();

            return View(satisRaporu);
        }

        public async Task<IActionResult> FuarRaporu()
        {
            var fuarRaporu = await _context.Fuarlar
                .Include(f => f.Salonlar)
                .ThenInclude(s => s.Standlar)
                .Include(f => f.Sozlesmeler)
                .ToListAsync();

            return View(fuarRaporu);
        }

        public async Task<IActionResult> MuhasebeRaporu()
        {
            var muhasebeRaporu = await _context.MuhasebeKayitlari
                .Include(m => m.Sozlesme)
                .ThenInclude(s => s.Firma)
                .OrderByDescending(m => m.Tarih)
                .ToListAsync();

            return View(muhasebeRaporu);
        }

        public async Task<IActionResult> ZiyaretciRaporu()
        {
            var ziyaretciRaporu = await _context.Ziyaretciler
                .Include(z => z.Fuar)
                .OrderByDescending(z => z.ZiyaretTarihi)
                .ToListAsync();

            return View(ziyaretciRaporu);
        }
    }
} 