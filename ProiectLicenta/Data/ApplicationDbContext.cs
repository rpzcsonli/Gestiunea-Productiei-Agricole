using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
namespace ProiectLicenta.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Angajat> Angajat { get; set; }
        public DbSet<Daunatori> Daunatori { get; set; }
        public DbSet<Parcela> Parcela { get; set; }
        public DbSet<Rasaduri> Rasad { get; set; }
        public DbSet<Tratament> Tratament { get; set; }
        public DbSet<RegistruCopilire> RegistruCopilire { get; set; }
        public DbSet<RegistruFertilizare> RegistruFertilizare { get; set; }
        public DbSet<RegistruIrigare> RegistruIrigare { get; set; }
        public DbSet<RegistruPalisare> RegistruPalisare { get; set; }
        public DbSet<RegistruRecoltare> RegistruRecoltare{ get; set; }
        public DbSet<RegistruTratamente> RegistruTratamente{ get; set; }
        public DbSet<Contact> Contact { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
    }
}
