using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruTratamente
    {
        [Key]
        public int CodTratamentAplicat { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [ForeignKey("Daunatori")]
        public int CodDaunator { get; set; }
        [Required]
        public int Suprafata { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataAplicare { get; set; }
        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
        public Daunatori? Daunatori { get; set; }
        public RegistruTratamente(int codTratamentAplicat, int codParcela, int codDaunator, int suprafata, int codAngajat, DateTime dataAplicare)
        {
            this.CodTratamentAplicat = codTratamentAplicat;
            this.CodParcela = codParcela;
            this.CodDaunator = codDaunator;
            this.Suprafata = suprafata;
            this.CodAngajat = codAngajat;
            this.DataAplicare = dataAplicare;
        }
    }
}
