using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddTratamentViewModel
    {
        [Key]
        public int CodTratament { get; set; }
        [Required(ErrorMessage ="Denumirea este obligatorie!")]
        [MaxLength(50,ErrorMessage = "Lungimea maxima este de 50 caractere!")]
        public string Denumire { get; set; }
        [Required(ErrorMessage ="Cantitatea este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Cantitate { get; set; }
        [Required(ErrorMessage ="Perioada este obligatorie!")]
        [Range(1, int.MaxValue, ErrorMessage = "Introduceti o valoare valida!")]
        public int Perioada { get; set; }
    }
}
