using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Gorusme
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Firma seçimi zorunludur")]
        public int FirmaId { get; set; }
        
        public Firma Firma { get; set; } = null!;
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
        
        public int? SalonId { get; set; }
        public Salon? Salon { get; set; }
        
        [Required(ErrorMessage = "Tarih zorunludur")]
        public DateTime Tarih { get; set; }
        
        [Required(ErrorMessage = "Görüşen kişi zorunludur")]
        [StringLength(200, ErrorMessage = "Görüşen kişi adı en fazla 200 karakter olabilir")]
        public string GorusenKisi { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "Notlar en fazla 1000 karakter olabilir")]
        public string Notlar { get; set; } = string.Empty;
    }
} 