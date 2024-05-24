using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class AddAngajatViewModel
    {
        
            [Key]
            public int CodAngajat { get; set; }
            [Required(ErrorMessage = "Numele este obligatoriu!")]
            [MaxLength(50, ErrorMessage = "Lungimea maxima este de 50 caractere!")]
            public string Nume { get; set; }
            [Required(ErrorMessage = "Prenumele este obligatoriu!")]
            [MaxLength(50, ErrorMessage = "Lungimea maxima este de 50 caractere!")]
            public string Prenume { get; set; }
            [Required(ErrorMessage = "Functia este obligatorie!")]
            [MaxLength(50, ErrorMessage = "Lungimea maxima este de 50 caractere!")]
            public string Functie { get; set; }
            [Required(ErrorMessage = "Numarul de telefon este obligatoriu!")]
            [MaxLength(10, ErrorMessage = "Lungimea maxima este de 10 caractere!")]
            public string Telefon { get; set; }
            [Required(ErrorMessage = "Email-ul este obligatoriu!")]
            [EmailAddress]
            public string Email { get; set; }


        
    }
}
