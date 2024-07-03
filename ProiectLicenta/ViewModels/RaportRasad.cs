using ProiectLicenta.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.ViewModels
{
    public class RaportRasad
    {

        public int CodRasad { get; set; }
        public string Denumire { get; set; }
        public string Planta { get; set; }
        public int Cantitate { get; set; }
        public bool filtrareData { get; set; }
        public DateTime DataInceput { get; set; }
        public DateTime DataSfarsit { get; set; }
        public virtual List<Parcela> Parcele { get; set; }
        public virtual List<RegistruCopilire> RegistruCopilire { get; set; }
        public virtual List<RegistruFertilizare> RegistruFertilizare { get; set; }
        public virtual List<RegistruIrigare> RegistruIrigare { get; set; }
        public virtual List<RegistruPalisare> RegistruPalisare { get; set; }
        public virtual List<RegistruRecoltare> RegistruRecoltare { get; set; }
        public virtual List<RegistruTratamente> RegistruTratamente { get; set; }
    }
}
