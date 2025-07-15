using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Firma
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firma adı zorunludur")]
        [StringLength(100, ErrorMessage = "Firma adı en fazla 100 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Yetkili adı en fazla 50 karakter olabilir")]
        public string Yetkili { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Unvan en fazla 50 karakter olabilir")]
        public string Unvan { get; set; } = string.Empty;
        
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string Tel { get; set; } = string.Empty;
        
        [StringLength(10, ErrorMessage = "Dahili numarası en fazla 10 karakter olabilir")]
        public string Dahili { get; set; } = string.Empty;
        
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string DirektTel { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Eposta { get; set; } = string.Empty;
        
        [Phone(ErrorMessage = "Geçerli bir faks numarası giriniz")]
        public string Faks { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir")]
        public string Adres { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Şehir adı en fazla 50 karakter olabilir")]
        public string Sehir { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Ülke adı en fazla 50 karakter olabilir")]
        public string Ulke { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Sektör adı en fazla 50 karakter olabilir")]
        public string Sektor { get; set; } = string.Empty;
    }
} 