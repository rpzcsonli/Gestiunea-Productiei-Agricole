using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruIrigare
    {
        [Key]
        public int CodIrigare { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [Required]
        public int DurataIrigare { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataIrigare { get; set; }
        public bool Stare { get; set; }

        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
        public RegistruIrigare(int codIrigare, int codParcela, int durataIrigare, int codAngajat, DateTime dataIrigare, bool stare)
        {
            this.CodIrigare = codIrigare;
            this.CodParcela = codParcela;
            this.DurataIrigare = durataIrigare;
            this.CodAngajat = codAngajat;
            this.DataIrigare = dataIrigare;
            this.Stare = stare;

        }
    }
}
