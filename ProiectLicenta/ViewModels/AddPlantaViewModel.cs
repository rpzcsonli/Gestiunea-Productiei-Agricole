using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddPlantaViewModel
    {
        [Key]
        public int CodPlanta { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nume { get; set; }
        [Required]
        [MaxLength(50)]
        public string Descriere { get; set; }
    }
}
