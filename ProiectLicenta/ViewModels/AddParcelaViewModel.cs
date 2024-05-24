using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddParcelaViewModel
    {
        [Key]
        public int CodParcela { get; set; }
        [Required]
        [MaxLength(50)]
        public string Locatie { get; set; }
        [Required]
        [MaxLength(50)]
        public string Tip { get; set; }
        [Required]
        public int Suprafata { get; set; }
        [ForeignKey("Rasaduri")]
        public int CodRasad { get; set; }
        [Required]
        public int NumarPlante { get; set; }
    }
}
