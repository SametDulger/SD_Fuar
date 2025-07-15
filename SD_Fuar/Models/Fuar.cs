using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Fuar
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Fuar adı zorunludur")]
        [StringLength(200, ErrorMessage = "Fuar adı en fazla 200 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Ülke zorunludur")]
        [StringLength(100, ErrorMessage = "Ülke adı en fazla 100 karakter olabilir")]
        public string Ulke { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Şehir zorunludur")]
        [StringLength(100, ErrorMessage = "Şehir adı en fazla 100 karakter olabilir")]
        public string Sehir { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        public DateTime BaslangicTarihi { get; set; }
        
        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        public DateTime BitisTarihi { get; set; }
        
        public ICollection<Salon> Salonlar { get; set; } = new List<Salon>();
        public ICollection<Teklif> Teklifler { get; set; } = new List<Teklif>();
        public ICollection<Sozlesme> Sozlesmeler { get; set; } = new List<Sozlesme>();
        public ICollection<Gorusme> Gorusmeler { get; set; } = new List<Gorusme>();
        public ICollection<Ziyaretci> Ziyaretciler { get; set; } = new List<Ziyaretci>();
    }
} 