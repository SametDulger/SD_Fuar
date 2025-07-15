using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Indirim
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "İndirim adı zorunludur")]
        [StringLength(200, ErrorMessage = "İndirim adı en fazla 200 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "İndirim tipi zorunludur")]
        [StringLength(50, ErrorMessage = "İndirim tipi en fazla 50 karakter olabilir")]
        public string Tip { get; set; } = string.Empty;
        
        [Range(0, 100, ErrorMessage = "Oran 0-100 arasında olmalıdır")]
        public double Oran { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Sabit tutar 0'dan büyük olmalıdır")]
        public decimal SabitTutar { get; set; }
        
        public int? FirmaId { get; set; }
        public Firma? Firma { get; set; }
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
        
        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        public DateTime BaslangicTarihi { get; set; }
        
        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        public DateTime BitisTarihi { get; set; }
        
        public bool AktifMi { get; set; }
        
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir")]
        public string Aciklama { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Uygulama koşulları en fazla 500 karakter olabilir")]
        public string UygulamaKosullari { get; set; } = string.Empty;
        
        [Range(0, int.MaxValue, ErrorMessage = "Minimum katılım sayısı 0'dan büyük olmalıdır")]
        public int MinimumKatilimSayisi { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Minimum tutar 0'dan büyük olmalıdır")]
        public decimal MinimumTutar { get; set; }
    }
} 