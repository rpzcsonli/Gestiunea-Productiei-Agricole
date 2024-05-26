﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ProiectLicenta.Models
{
    public class Rasaduri
    {
        [Key]
        public int CodRasad { get; set; }
        [Required]
        [MaxLength(50)]
        public string Denumire { get; set; }
        [ForeignKey("Plante")]
        public int CodPlanta { get; set; }
        [Required]
        public DateTime DataSemanat { get; set; }
        [Required]
        public DateTime DataMaturitate { get; set; }
        [Required]
        public int Cantitate { get; set; }
        public Plante Plante { get; set; }
        public ICollection<Parcela> Parcela { get; set; }
        public Rasaduri(int CodRasad, string Denumire, int CodPlanta, DateTime DataSemanat, DateTime DataMaturitate, int Cantitate)
        {
            this.CodRasad = CodRasad;
            this.Denumire = Denumire;
            this.CodPlanta = CodPlanta;
            this.DataSemanat = DataSemanat;
            this.DataMaturitate = DataMaturitate;
            this.Cantitate = Cantitate;
            this.Parcela = new HashSet<Parcela>();
        }
        public override string ToString()
        {
            return Denumire;
        }
    }
}
