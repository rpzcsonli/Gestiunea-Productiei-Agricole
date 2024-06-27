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
        public virtual List<Parcela> Parcele { get; set; }
        public virtual List<RegistruCopilireDate> RegistruCopilire { get; set; }
        public virtual List<RegistruFertilizareDate> RegistruFertilizare { get; set; }
        public virtual List<RegistruIrigareDate> RegistruIrigare { get; set; }
        public virtual List<RegistruPalisareDate> RegistruPalisare { get; set; }
        public virtual List<RegistruRecoltareDate> RegistruRecoltare { get; set; }
        public virtual List<RegistruTratamenteDate> RegistruTratamente { get; set; }
    }
    public class Parcele
    {
        public int CodParcela { get; set; }
        public string Locatie { get; set; }
        public string Tip { get; set; }
        public int Suprafata { get; set; }
        public int CodRasad { get; set; }
        public int NumarPlante { get; set; }
    }
}
