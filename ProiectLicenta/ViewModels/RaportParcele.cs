using ProiectLicenta.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectLicenta.ViewModels
{
    public class RaportParcele
    {

        public int CodParcela { get; set; }
        public int CodRasad { get; set; }
        public string Locatie { get; set; }
        public string Tip { get; set; }
        public int Suprafata { get; set; }
        public int NumarPlante { get; set; }
        public virtual List<RegistruCopilire> RegistruCopilire { get; set; }
        public virtual List<RegistruFertilizare> RegistruFertilizare { get; set; }
        public virtual List<RegistruIrigare> RegistruIrigare { get; set; }
        public virtual List<RegistruPalisare> RegistruPalisare { get; set; }
        public virtual List<RegistruRecoltare> RegistruRecoltare { get; set; }
        public virtual List<RegistruTratamente> RegistruTratamente { get; set; }
    }

}
