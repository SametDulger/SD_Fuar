using System.ComponentModel.DataAnnotations;

namespace SD_Fuar.Models
{
    public class Salon
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Salon adı zorunludur")]
        [StringLength(200, ErrorMessage = "Salon adı en fazla 200 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Fuar seçimi zorunludur")]
        public int FuarId { get; set; }
        
        public Fuar Fuar { get; set; } = null!;
        public ICollection<Stand> Standlar { get; set; } = new List<Stand>();
        public ICollection<Teklif> Teklifler { get; set; } = new List<Teklif>();
        public ICollection<Sozlesme> Sozlesmeler { get; set; } = new List<Sozlesme>();
        public ICollection<Gorusme> Gorusmeler { get; set; } = new List<Gorusme>();
    }
} 