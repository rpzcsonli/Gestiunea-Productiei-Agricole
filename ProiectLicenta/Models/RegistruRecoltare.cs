using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruRecoltare
    {
        [Key]
        public int CodRecoltare { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public int CantitateRecoltata { get; set; }
        [Required]
        public DateTime DataRecoltare { get; set; }
        public bool Stare { get; set; }

        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
        
        public RegistruRecoltare(int codRecoltare, int codParcela, int codAngajat, int cantitateRecoltata, DateTime dataRecoltare, bool stare)
        {
            this.CodRecoltare = codRecoltare;
            this.CodParcela = codParcela;
            this.CodAngajat = codAngajat;
            this.CantitateRecoltata = cantitateRecoltata;
            this.DataRecoltare = dataRecoltare;
            this.Stare = stare;

        }
    }
}
