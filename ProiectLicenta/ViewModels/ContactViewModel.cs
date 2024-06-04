using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.ViewModels
{
    public class ContactViewModel
    {
        [Key]
        public int CodContact { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nume { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subiect { get; set; }
        [Required]
        [MaxLength(300)]
        public string Mesaj { get; set; }
    }
}
