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

        public virtual List<RegistruCopilireDate> RegistruCopilire { get; set; }
        public virtual List<RegistruFertilizareDate> RegistruFertilizare { get; set; }
        public virtual List<RegistruIrigareDate> RegistruIrigare { get; set; }
        public virtual List<RegistruPalisareDate> RegistruPalisare { get; set; }
        public virtual List<RegistruRecoltareDate> RegistruRecoltare { get; set; }
        public virtual List<RegistruTratamenteDate> RegistruTratamente { get; set; }
    }
    public class RegistruCopilireDate
    {
        public int CodCopilire { get; set; }
        public int CodParcela { get; set; }
        public int NumarPlante { get; set; }
        public DateTime DataCopilire { get; set; }
    }
    public class RegistruFertilizareDate
    {
        public int CodFertilizare { get; set; }
        public int CodParcela { get; set; }
        public int Suprafata { get; set; }
        public DateTime DataFertilizare { get; set; }
    }
    public class RegistruIrigareDate
    {
        public int CodIrigare { get; set; }
        public int CodParcela { get; set; }
        public int DurataIrigare { get; set; }
        public DateTime DataIrigare { get; set; }
    }
    public class RegistruPalisareDate
    {
        public int CodPalisare { get; set; }
        public int CodParcela { get; set; }
        public int NumarPlante { get; set; }
        public DateTime DataPalisare { get; set; }
    }
    public class RegistruRecoltareDate
    {
        public int CodRecoltare { get; set; }
        public int CodParcela { get; set; }
        public int CantitateRecoltata { get; set; }
        public DateTime DataRecoltare { get; set; }
    }
    public class RegistruTratamenteDate
    {
        public int CodTratamentAplicat { get; set; }
        public int CodParcela { get; set; }
        public int CodDaunator { get; set; }
        public int Suprafata { get; set; }
        public DateTime DataAplicare { get; set; }
    }

}
