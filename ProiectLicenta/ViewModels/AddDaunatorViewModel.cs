using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddDaunatorViewModel
    {
        [Key]
        public int CodDaunator { get; set; }
        [Required(ErrorMessage ="Denumirea este obligatorie!")]
        [MaxLength(50,ErrorMessage ="Lungimea maxima este de 50 caractere!")]
        public string Denumire { get; set; }
        [ForeignKey("Tratament")]
        public int CodTratament { get; set; }
    }
}
