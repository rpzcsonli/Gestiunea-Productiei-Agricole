using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.Models
{
    public class Contact
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
        
        public Contact(int codContact, string nume, string email, string subiect, string mesaj)
        {
            this.CodContact = codContact;
            this.Nume = nume;
            this.Email = email;
            this.Subiect = subiect;
            this.Mesaj = mesaj;
        }
    }
}
