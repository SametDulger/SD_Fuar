using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Bilet
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Bilet kodu zorunludur")]
        [StringLength(50, ErrorMessage = "Bilet kodu en fazla 50 karakter olabilir")]
        public string BiletKodu { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Bilet tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Bilet tipi en fazla 50 karakter olabilir")]
        public string Tip { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Fiyat { get; set; }
        
        [Required(ErrorMessage = "Döviz türü zorunludur")]
        [StringLength(10, ErrorMessage = "Döviz türü en fazla 10 karakter olabilir")]
        public string Doviz { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Satış tarihi zorunludur")]
        public DateTime SatisTarihi { get; set; }
        
        [Required(ErrorMessage = "Geçerlilik tarihi zorunludur")]
        public DateTime GecerlilikTarihi { get; set; }
        
        public int? ZiyaretciId { get; set; }
        public Ziyaretci? Ziyaretci { get; set; }
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
        
        [StringLength(100, ErrorMessage = "Satış personeli adı en fazla 100 karakter olabilir")]
        public string SatisPersoneli { get; set; } = string.Empty;
        
        public bool KullanildiMi { get; set; }
        public DateTime? KullanilmaTarihi { get; set; }
        
        [StringLength(50, ErrorMessage = "Ödeme tipi en fazla 50 karakter olabilir")]
        public string OdemeTipi { get; set; } = string.Empty;
    }
} 