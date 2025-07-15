using Microsoft.EntityFrameworkCore;

namespace SD_Fuar.Models
{
    public class SD_FuarContext : DbContext
    {
        public SD_FuarContext(DbContextOptions<SD_FuarContext> options) : base(options) { }

        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<Fuar> Fuarlar { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Stand> Standlar { get; set; }
        public DbSet<Gorusme> Gorusmeler { get; set; }
        public DbSet<Teklif> Teklifler { get; set; }
        public DbSet<Sozlesme> Sozlesmeler { get; set; }
        public DbSet<Ziyaretci> Ziyaretciler { get; set; }
        public DbSet<UygulamaKullanicisi> UygulamaKullanicilari { get; set; }
        public DbSet<MuhasebeKaydi> MuhasebeKayitlari { get; set; }
        public DbSet<Barkod> Barkodlar { get; set; }
        public DbSet<Bilet> Biletler { get; set; }
        public DbSet<Davetiye> Davetiyeler { get; set; }
        public DbSet<Indirim> Indirimler { get; set; }
        public DbSet<KatilimGecmisi> KatilimGecmisleri { get; set; }
    }
} 