using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Teklif
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Firma seçimi zorunludur")]
        public int FirmaId { get; set; }
        
        public Firma Firma { get; set; } = null!;
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
        
        public int? SalonId { get; set; }
        public Salon? Salon { get; set; }
        
        public int? StandId { get; set; }
        public Stand? Stand { get; set; }
        
        [Required(ErrorMessage = "Tarih zorunludur")]
        public DateTime Tarih { get; set; }
        
        [Required(ErrorMessage = "Teklif tarihi zorunludur")]
        public DateTime TeklifTarihi { get; set; }
        
        [Required(ErrorMessage = "Metrekare zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Metrekare 0'dan büyük olmalıdır")]
        public double Metrekare { get; set; }
        
        [Required(ErrorMessage = "Birim fiyat zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Birim fiyat 0'dan büyük olmalıdır")]
        public decimal BirimFiyat { get; set; }
        
        [Required(ErrorMessage = "Döviz türü zorunludur")]
        [StringLength(10, ErrorMessage = "Döviz türü en fazla 10 karakter olabilir")]
        public string Doviz { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Döviz kuru zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Döviz kuru 0'dan büyük olmalıdır")]
        public decimal DovizKuru { get; set; }
        
        [Required(ErrorMessage = "Vade sayısı zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Vade sayısı en az 1 olmalıdır")]
        public int VadeSayisi { get; set; }
        
        [StringLength(500, ErrorMessage = "Ödeme planı en fazla 500 karakter olabilir")]
        public string OdemePlani { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Son geçerlilik tarihi zorunludur")]
        public DateTime SonGecerlilikTarihi { get; set; }
    }
} 