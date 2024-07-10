using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRegistruTratamenteViewModel
    {
        [Key]
        public int CodTratamentAplicat { get; set; }
        [ForeignKey("Parcela")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti o Parcela!")]
        public int CodParcela { get; set; }
        [ForeignKey("Daunatori")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti un Daunator!")]
        public int CodDaunator { get; set; }
        [Required(ErrorMessage ="Suprafata este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Suprafata { get; set; }
        [ForeignKey("Angajat")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti un Angajat!")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataAplicare { get; set; }
        [Required]

        public bool Stare { get; set; }

    }
}
