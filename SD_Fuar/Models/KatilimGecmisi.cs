using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class KatilimGecmisi
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Firma seçimi zorunludur")]
        public int FirmaId { get; set; }
        
        public Firma Firma { get; set; } = null!;
        
        [Required(ErrorMessage = "Fuar seçimi zorunludur")]
        public int FuarId { get; set; }
        
        public Fuar Fuar { get; set; } = null!;
        
        [Required(ErrorMessage = "Yıl zorunludur")]
        [Range(1900, 2100, ErrorMessage = "Yıl 1900-2100 arasında olmalıdır")]
        public int Yil { get; set; }
        
        [Required(ErrorMessage = "Kullanılan metrekare zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Kullanılan metrekare 0'dan büyük olmalıdır")]
        public double KullanilanMetrekare { get; set; }
        
        [Required(ErrorMessage = "Toplam tutar zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Toplam tutar 0'dan büyük olmalıdır")]
        public decimal ToplamTutar { get; set; }
        
        [Required(ErrorMessage = "Döviz türü zorunludur")]
        [StringLength(10, ErrorMessage = "Döviz türü en fazla 10 karakter olabilir")]
        public string Doviz { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Stand tipi en fazla 100 karakter olabilir")]
        public string StandTipi { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Salon adı en fazla 200 karakter olabilir")]
        public string SalonAdi { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Stand kodu en fazla 50 karakter olabilir")]
        public string StandKodu { get; set; } = string.Empty;
        
        public bool OdendiMi { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        
        [StringLength(1000, ErrorMessage = "Notlar en fazla 1000 karakter olabilir")]
        public string Notlar { get; set; } = string.Empty;
    }
} 