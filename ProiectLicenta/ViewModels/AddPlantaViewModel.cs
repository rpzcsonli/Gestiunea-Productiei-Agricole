using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddPlantaViewModel
    {
        [Key]
        public int CodPlanta { get; set; }
        [Required(ErrorMessage ="Numele este obligatoriu!")]
        [MaxLength(50,ErrorMessage = "Lungimea maxima este de 50 caractere!")]
        public string Nume { get; set; }
        [Required(ErrorMessage = "Descrierea este obligatorie!")]
        [MaxLength(50, ErrorMessage = "Lungimea maxima este de 50 caractere!")]
        public string Descriere { get; set; }
    }
}
