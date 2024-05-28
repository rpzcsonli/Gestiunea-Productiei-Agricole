using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.Models
{
    public class Plante
    {
        [Key]
        public int CodPlanta { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nume { get; set; }
        public ICollection<Rasaduri> Rasaduri { get; set; }
        public ICollection<Daunatori> Daunatori { get; set; }
        public ICollection<RegistruRecoltare> RegistruRecoltare { get; set; }
        public Plante(int CodPlanta, string Nume)
        {
            this.CodPlanta = CodPlanta;
            this.Nume = Nume;
            this.Rasaduri= new HashSet<Rasaduri>();
            this.Daunatori = new HashSet<Daunatori>();
            this.RegistruRecoltare = new HashSet<RegistruRecoltare>();
        }
        public override string ToString()
        {
            return Nume;
        }
    }
}
