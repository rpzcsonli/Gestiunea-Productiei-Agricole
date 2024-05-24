using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruFertilizare
    {
        [Key]
        public int CodFertilizare { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [Required]
        public int Suprafata { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataFertilizare { get; set; }
        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
        

        public RegistruFertilizare(int codFertilizare, int codParcela, int suprafata, int codAngajat, DateTime dataFertilizare)
        {
            this.CodFertilizare = codFertilizare;
            this.CodParcela = codParcela;
            this.Suprafata = suprafata;
            this.CodAngajat = codAngajat;
            this.DataFertilizare = dataFertilizare;
        }

    }
}
