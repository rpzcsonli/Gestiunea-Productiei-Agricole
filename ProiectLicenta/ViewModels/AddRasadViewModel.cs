using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRasadViewModel
    {
        [Key]
        public int CodRasad { get; set; }
        [Required(ErrorMessage = "Denumirea este obligatorie!")]
        [MaxLength(50, ErrorMessage = "Lungimea maxima este de 50 caractere!")]
        public string Denumire { get; set; }
        [ForeignKey("Plante")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti o Planta!")]
        public int CodPlanta { get; set; }
        [Required]
        public DateTime DataSemanat { get; set; }
        [Required]
        public DateTime DataMaturitate { get; set; }
        [Required(ErrorMessage = "Cantitatea este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Cantitate { get; set; }
    }
}
