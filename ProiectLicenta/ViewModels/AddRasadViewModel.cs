using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRasadViewModel
    {
        [Key]
        public int CodRasad { get; set; }
        [Required]
        [MaxLength(50)]
        public string Denumire { get; set; }
        [ForeignKey("Plante")]
        public int CodPlanta { get; set; }
        [Required]
        public DateTime DataSemanat { get; set; }
        [Required]
        public DateTime DataMaturitate { get; set; }
        [Required]
        public int Cantitate { get; set; }
    }
}
