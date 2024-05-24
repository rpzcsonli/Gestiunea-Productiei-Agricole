using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProiectLicenta.Models

{
    public class Parcela
    {
        [Key]
        public int CodParcela { get; set; }
        [Required]
        [MaxLength(50)]
        public string Locatie { get; set; }
        [Required]
        [MaxLength(50)]
        public string Tip { get; set; }
        [Required]
        public int Suprafata { get; set; }
        [ForeignKey("Rasaduri")]
        public int CodRasad { get; set; }
        [Required]
        public int NumarPlante { get; set; }
        public ICollection<RegistruCopilire> RegistruCopilire { get; set; }
        public ICollection<RegistruFertilizare> RegistruFertilizare { get; set; }
        public ICollection<RegistruIrigare> RegistruIrigare { get; set; }
        public ICollection<RegistruPalisare> RegistruPalisare { get; set; }
        public ICollection<RegistruRecoltare> RegistruRecoltare { get; set; }
        public ICollection<RegistruTratamente> RegistruTratamente { get; set; }
        public Rasaduri? Rasaduri { get; set; }

        public Parcela(int codParcela, string locatie, string tip, int suprafata, int codRasad, int numarPlante)
        {
            this.CodParcela = codParcela;
            this.Locatie = locatie;
            this.Tip = tip;
            this.Suprafata = suprafata;
            this.CodRasad = codRasad;
            this.NumarPlante = numarPlante;
            this.RegistruCopilire = new HashSet<RegistruCopilire>();
            this.RegistruFertilizare = new HashSet<RegistruFertilizare>();
            this.RegistruIrigare = new HashSet<RegistruIrigare>();
            this.RegistruPalisare = new HashSet<RegistruPalisare>();
            this.RegistruRecoltare = new HashSet<RegistruRecoltare>();
            this.RegistruTratamente = new HashSet<RegistruTratamente>();
        }
        public override string ToString()
        {
            return Locatie; }
    }
}
