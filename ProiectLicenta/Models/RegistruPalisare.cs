
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruPalisare
    {
        [Key]
        public int CodPalisare { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [Required]
        public int NumarPlante { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataPalisare { get; set; }
        public bool Stare { get; set; }

        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
        public RegistruPalisare(int codPalisare, int codParcela, int numarPlante, int codAngajat, DateTime dataPalisare, bool stare)
        {
            this.CodPalisare = codPalisare;
            this.CodParcela = codParcela;
            this.NumarPlante = numarPlante;
            this.CodAngajat = codAngajat;
            this.DataPalisare = dataPalisare;
            this.Stare = stare;

        }
    }
}
