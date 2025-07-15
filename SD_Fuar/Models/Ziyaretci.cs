using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Ziyaretci
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ad zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Soyad zorunludur")]
        [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir")]
        public string Soyad { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Firma adı en fazla 200 karakter olabilir")]
        public string FirmaAdi { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Adres { get; set; } = string.Empty;
        
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir")]
        public string Tel { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        public string Eposta { get; set; } = string.Empty;
        
        [StringLength(20, ErrorMessage = "Faks numarası en fazla 20 karakter olabilir")]
        public string Faks { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "Meslek en fazla 100 karakter olabilir")]
        public string Meslek { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Ziyaret tarihi zorunludur")]
        public DateTime ZiyaretTarihi { get; set; }
        
        [StringLength(1000, ErrorMessage = "Notlar en fazla 1000 karakter olabilir")]
        public string Notlar { get; set; } = string.Empty;
        
        public int? FuarId { get; set; }
        public Fuar? Fuar { get; set; }
    }
} 