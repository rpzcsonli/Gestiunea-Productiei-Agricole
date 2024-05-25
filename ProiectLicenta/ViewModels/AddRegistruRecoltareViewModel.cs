using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddRegistruRecoltareViewModel
    {
        [Key]
        public int CodRecoltare { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required(ErrorMessage = "Cantitatea recoltata este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int CantitateRecoltata { get; set; }
        [Required]
        public DateTime DataRecoltare { get; set; }
    }
}
