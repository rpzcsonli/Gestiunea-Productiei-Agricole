using ProiectLicenta.Models;
using System.Collections.Generic;

namespace ProiectLicenta.ViewModels
{
    public class DateRecoltareViewModel
    {
        public int parcelaSelectata { get; set; }
        public DateTime dataInceput { get; set; }
        public DateTime dataSfarsit { get; set; }
        public List<RegistruRecoltare> RecoltareData { get; set; }

    }
}
