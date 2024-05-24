using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.Models
{
    public class Tratament
    {
        [Key]
        public int CodTratament { get; set; }
        [Required]
        [MaxLength(50)]
        public string Denumire { get; set; }
        [Required]
        public int Cantitate { get; set; }
        [Required]
        public int Perioada { get; set; }
        public ICollection<Daunatori> Daunatori { get; set; }
        public Tratament(int CodTratament, string Denumire, int Cantitate, int Perioada)
        {
            this.CodTratament = CodTratament;
            this.Denumire = Denumire;
            this.Cantitate = Cantitate;
            this.Perioada = Perioada;
            this.Daunatori = new HashSet<Daunatori>();
        }
        public override string ToString()
        {
            return Denumire;
        }
    }
}
