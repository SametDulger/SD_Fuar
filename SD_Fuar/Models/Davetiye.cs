using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Davetiye
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Davetiye kodu zorunludur")]
        [StringLength(50, ErrorMessage = "Davetiye kodu en fazla 50 karakter olabilir")]
        public string DavetiyeKodu { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Davetiye tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Davetiye tipi en fazla 50 karakter olabilir")]
        public string Tip { get; set; } = string.Empty;
        
        public int? ZiyaretciId { get; set; }
        public Ziyaretci? Ziyaretci { get; set; }
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
        
        [Required(ErrorMessage = "Oluşturma tarihi zorunludur")]
        public DateTime OlusturmaTarihi { get; set; }
        
        [Required(ErrorMessage = "Geçerlilik tarihi zorunludur")]
        public DateTime GecerlilikTarihi { get; set; }
        
        public bool KullanildiMi { get; set; }
        public DateTime? KullanilmaTarihi { get; set; }
        
        [Required(ErrorMessage = "Durum zorunludur")]
        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir")]
        public string Durum { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Oluşturan kullanıcı zorunludur")]
        [StringLength(100, ErrorMessage = "Oluşturan kullanıcı adı en fazla 100 karakter olabilir")]
        public string OlusturanKullanici { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir")]
        public string? Notlar { get; set; }
    }
} 