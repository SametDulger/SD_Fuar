using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Barkod
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Barkod kodu zorunludur")]
        [StringLength(100, ErrorMessage = "Barkod kodu en fazla 100 karakter olabilir")]
        public string BarkodKodu { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Barkod tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Barkod tipi en fazla 50 karakter olabilir")]
        public string Tip { get; set; } = string.Empty;
        
        public int? ZiyaretciId { get; set; }
        public Ziyaretci? Ziyaretci { get; set; }
        
        [Required(ErrorMessage = "Oluşturma tarihi zorunludur")]
        public DateTime OlusturmaTarihi { get; set; }
        
        public DateTime? GecerlilikTarihi { get; set; }
        
        public bool KullanildiMi { get; set; }
        public DateTime? KullanilmaTarihi { get; set; }
        
        [Required(ErrorMessage = "Durum zorunludur")]
        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir")]
        public string Durum { get; set; } = string.Empty;
    }
} 