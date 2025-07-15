using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace SD_Fuar.Models
{
    public class UygulamaKullanicisi
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kullanıcı adı en fazla 100 karakter olabilir")]
        public string KullaniciAdi { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Şifre zorunludur")]
        [StringLength(100, ErrorMessage = "Şifre en fazla 100 karakter olabilir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Sifre { get; set; } = string.Empty;
        
        [StringLength(50, ErrorMessage = "Rol en fazla 50 karakter olabilir")]
        public string Rol { get; set; } = string.Empty;

        public void SifreHashle()
        {
            if (!string.IsNullOrWhiteSpace(Sifre))
            {
                using var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Sifre));
                Sifre = Convert.ToBase64String(hashBytes);
            }
        }

        public bool SifreDogrula(string girilenSifre)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(girilenSifre));
            var girilenHash = Convert.ToBase64String(hashBytes);
            return Sifre == girilenHash;
        }
    }
} 