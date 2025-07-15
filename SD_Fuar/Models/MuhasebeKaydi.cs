using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class MuhasebeKaydi
    {
        public int Id { get; set; }
        
        public int? SozlesmeId { get; set; }
        public Sozlesme? Sozlesme { get; set; }
        
        [Required(ErrorMessage = "Tarih zorunludur")]
        public DateTime Tarih { get; set; }
        
        [Required(ErrorMessage = "Açıklama zorunludur")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Aciklama { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tutar zorunludur")]
        [Range(0, double.MaxValue, ErrorMessage = "Tutar 0'dan büyük olmalıdır")]
        public decimal Tutar { get; set; }
        
        [Required(ErrorMessage = "Döviz türü zorunludur")]
        [StringLength(10, ErrorMessage = "Döviz türü en fazla 10 karakter olabilir")]
        public string Doviz { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Ödeme tarihi zorunludur")]
        public DateTime OdemeTarihi { get; set; }
        
        [StringLength(50, ErrorMessage = "Ödeme tipi en fazla 50 karakter olabilir")]
        public string OdemeTipi { get; set; } = string.Empty;
        
        [Range(0, double.MaxValue, ErrorMessage = "Kalan bakiye 0'dan büyük olmalıdır")]
        public decimal KalanBakiye { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Komisyon 0'dan büyük olmalıdır")]
        public decimal Komisyon { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "KDV 0'dan büyük olmalıdır")]
        public decimal KDV { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Kurumlar vergisi 0'dan büyük olmalıdır")]
        public decimal KurumlarVergisi { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Yedek akçe 0'dan büyük olmalıdır")]
        public decimal YedekAkce { get; set; }
    }
} 