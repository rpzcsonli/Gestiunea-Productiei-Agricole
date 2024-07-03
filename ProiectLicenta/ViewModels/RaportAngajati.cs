using ProiectLicenta.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.ViewModels
{
    public class RaportAngajati
    {
        public int CodAngajat { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Functie { get; set; }
        public bool filtrareData { get; set; }
        public DateTime DataInceput { get; set; }
        public DateTime DataSfarsit { get; set; }
        public virtual List<RegistruCopilire> RegistruCopilire { get; set; }
        public virtual List<RegistruFertilizare> RegistruFertilizare { get; set; }
        public virtual List<RegistruIrigare> RegistruIrigare { get; set; }
        public virtual List<RegistruPalisare> RegistruPalisare { get; set; }
        public virtual List<RegistruRecoltare> RegistruRecoltare { get; set; }
        public virtual List<RegistruTratamente> RegistruTratamente { get; set; }
    }

}
