using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.Models
{
    public class Angajat
    {
        [Key]
        public int CodAngajat { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nume { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prenume { get; set; }
        [Required]
        [MaxLength(50)]
        public string Functie { get; set; }
        [Required]
        [MaxLength(10)]
        public string Telefon { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<RegistruCopilire> RegistruCopilire { get; set; }
        public virtual ICollection<RegistruFertilizare> RegistruFertilizare { get; set; }
        public virtual ICollection<RegistruIrigare> RegistruIrigare { get; set; }
        public virtual ICollection<RegistruPalisare> RegistruPalisare { get; set; }
        public virtual ICollection<RegistruRecoltare> RegistruRecoltare { get; set; }
        public virtual ICollection<RegistruTratamente> RegistruTratamente { get; set; }
     
        
        public Angajat(int codAngajat, string nume, string prenume, string functie, string telefon, string email)
        {
            this.CodAngajat = codAngajat;
            this.Nume = nume;
            this.Prenume = prenume;
            this.Functie = functie;
            this.Telefon = telefon;
            this.Email = email;
            this.RegistruCopilire = new HashSet<RegistruCopilire>();
            this.RegistruFertilizare = new HashSet<RegistruFertilizare>();
            this.RegistruIrigare = new HashSet<RegistruIrigare>();
            this.RegistruPalisare = new HashSet<RegistruPalisare>();
            this.RegistruRecoltare = new HashSet<RegistruRecoltare>();
            this.RegistruTratamente = new HashSet<RegistruTratamente>();
        }
        public override string ToString()
        {
            return Nume + " " + Prenume;
        }
    }
}
