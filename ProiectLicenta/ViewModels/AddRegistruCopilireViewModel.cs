
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRegistruCopilireViewModel
    {
        [Key]
        public int CodCopilire { get; set; }
        [ForeignKey("Parcela")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti o Parcela!")]
        public int CodParcela { get; set; }
        [Required(ErrorMessage = "Numarul de plante este obligatoriu!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int NumarPlante { get; set; }
        [ForeignKey("Angajat")]
        [Range(1, int.MaxValue, ErrorMessage = "Alegeti un Angajat!")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataCopilire { get; set; }
        [Required]
        public bool Stare { get; set; }

    }
}
