using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class Daunatori
    {
        [Key]
        public int CodDaunator { get; set; }
        [Required]
        [MaxLength(50)]
        public string Denumire { get; set; }
        [ForeignKey("Tratament")]
        public int CodTratament { get; set; }
        public Tratament? Tratament { get; set; }
        public ICollection<RegistruTratamente> RegistruTratamente { get; set; }
        public Daunatori(int CodDaunator, string Denumire, int CodTratament)
        {
            this.CodDaunator = CodDaunator;
            this.Denumire = Denumire;
            this.CodTratament = CodTratament;
            this.RegistruTratamente = new HashSet<RegistruTratamente>();
        }
        public override string ToString()
        {
            return Denumire;
        }
    }
}
