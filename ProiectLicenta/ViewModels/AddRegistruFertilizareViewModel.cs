using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRegistruFertilizareViewModel
    {
        [Key]
        public int CodFertilizare { get; set; }
        [ForeignKey("Parcela")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti o Parcela!")]
        public int CodParcela { get; set; }
        [Required(ErrorMessage = "Suprafata este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Suprafata { get; set; }
        [ForeignKey("Angajat")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti un Angajat!")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataFertilizare { get; set; }
    }
}
