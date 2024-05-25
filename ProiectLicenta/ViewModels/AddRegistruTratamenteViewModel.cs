using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRegistruTratamenteViewModel
    {
        [Key]
        public int CodTratamentAplicat { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [ForeignKey("Daunatori")]
        public int CodDaunator { get; set; }
        [Required(ErrorMessage ="Suprafata este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Suprafata { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataAplicare { get; set; }
    }
}
