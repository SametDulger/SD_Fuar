using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Stand
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Stand kodu zorunludur")]
        [StringLength(50, ErrorMessage = "Stand kodu en fazla 50 karakter olabilir")]
        public string Kod { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "En değeri zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "En değeri 0'dan büyük olmalıdır")]
        public double En { get; set; }
        
        [Required(ErrorMessage = "Boy değeri zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Boy değeri 0'dan büyük olmalıdır")]
        public double Boy { get; set; }
        
        [Required(ErrorMessage = "Metrekare değeri zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Metrekare değeri 0'dan büyük olmalıdır")]
        public double Metrekare { get; set; }
        
        [Required(ErrorMessage = "Birim fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyat 0'dan büyük olmalıdır")]
        public decimal BirimFiyat { get; set; }
        
        public bool MusaitMi { get; set; }
        
        [Required(ErrorMessage = "Salon seçimi zorunludur")]
        public int SalonId { get; set; }
        
        public Salon Salon { get; set; } = null!;
        public ICollection<Teklif> Teklifler { get; set; } = new List<Teklif>();
        public ICollection<Sozlesme> Sozlesmeler { get; set; } = new List<Sozlesme>();
    }
} 