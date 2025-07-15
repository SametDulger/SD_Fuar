using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;
using System.Diagnostics;

namespace SD_Fuar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SD_FuarContext _context;

        private const int DASHBOARD_ITEM_COUNT = 5;

        public HomeController(ILogger<HomeController> logger, SD_FuarContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardViewModel = new DashboardViewModel
            {
                FirmaSayisi = await _context.Firmalar.CountAsync(),
                FuarSayisi = await _context.Fuarlar.CountAsync(),
                SozlesmeSayisi = await _context.Sozlesmeler.CountAsync(),
                ZiyaretciSayisi = await _context.Ziyaretciler.CountAsync(),
                SonFirmalar = await _context.Firmalar.OrderByDescending(f => f.Id).Take(DASHBOARD_ITEM_COUNT).ToListAsync(),
                AktifFuarlar = await _context.Fuarlar.Where(f => f.BitisTarihi >= DateTime.Today).OrderBy(f => f.BaslangicTarihi).Take(DASHBOARD_ITEM_COUNT).ToListAsync()
            };

            return View(dashboardViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class DashboardViewModel
    {
        public int FirmaSayisi { get; set; }
        public int FuarSayisi { get; set; }
        public int SozlesmeSayisi { get; set; }
        public int ZiyaretciSayisi { get; set; }
        public List<Firma> SonFirmalar { get; set; } = new List<Firma>();
        public List<Fuar> AktifFuarlar { get; set; } = new List<Fuar>();
    }
}
