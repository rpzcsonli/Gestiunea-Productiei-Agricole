using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.Models
{
    public class RegistruCopilire
    {
        [Key]
        public int CodCopilire { get; set; }
        [ForeignKey("Parcela")]
        public int CodParcela { get; set; }
        [Required]
        public int NumarPlante { get; set; }
        [ForeignKey("Angajat")]
        public int CodAngajat { get; set; }
        [Required]
        public DateTime DataCopilire { get; set; }
        public bool Stare { get; set; }
        public Angajat? Angajat { get; set; }
        public Parcela? Parcela { get; set; }
      
        public RegistruCopilire(int codCopilire, int codParcela, int numarPlante, int codAngajat, DateTime dataCopilire, bool stare)
        {
            this.CodCopilire = codCopilire;
            this.CodParcela = codParcela;
            this.NumarPlante = numarPlante;
            this.CodAngajat = codAngajat;
            this.DataCopilire = dataCopilire;
            this.Stare = stare;

        }

    }
}
