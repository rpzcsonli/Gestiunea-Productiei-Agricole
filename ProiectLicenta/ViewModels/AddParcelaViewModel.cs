using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddParcelaViewModel
    {
        [Key]
        public int CodParcela { get; set; }
        [Required(ErrorMessage ="Locatia este obligatorie!")]
        [MaxLength(50,ErrorMessage ="Lungimea maxima este de 50 caractere!")]
        public string Locatie { get; set; }
        [Required(ErrorMessage ="Tipul este obligatoriu!")]
        [MaxLength(50,ErrorMessage ="Lungimea maxima este de 50 caractere!")]
        public string Tip { get; set; }
        [Required(ErrorMessage ="Suprafata este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Suprafata { get; set; }
        [ForeignKey("Rasaduri")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti un Rasad!")]
        public int CodRasad { get; set; }
        [Required(ErrorMessage ="Numarul de plante este obligatoriu!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int NumarPlante { get; set; }
    }
}
