using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;
using System.Diagnostics;

namespace ProiectLicenta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            context = dbcontext;
        }
        public IActionResult Index()
        {
            var numarParcele = context.Parcela.Count();
            var suprafataPlante = context.Parcela.Sum(p => p.Suprafata);
            var totalPlante = context.Parcela.Sum(p => p.NumarPlante);
            var totalAngajati = context.Angajat.Count();
            var totalRasaduri = context.Rasad.Sum(p => p.Cantitate);
            var totalTratamente = context.RegistruTratamente.Where(a=>a.Stare==true).Count();
            var suprafataTratamente = context.RegistruTratamente.Where(a => a.Stare == true).Sum(p => p.Suprafata);
            var totalIrigari = context.RegistruIrigare.Where(a => a.Stare == true).Count();
            var durataIrigari = context.RegistruIrigare.Where(a => a.Stare == true).Sum(p => p.DurataIrigare);
            var totalFertilizari = context.RegistruFertilizare.Where(a => a.Stare == true).Count();
            var suprafataFertilizari = context.RegistruFertilizare.Where(a => a.Stare == true).Sum(p => p.Suprafata);
            var totalPalisari = context.RegistruPalisare.Where(a => a.Stare == true).Count();
            var plantePalisari = context.RegistruPalisare.Where(a => a.Stare == true).Sum(p => p.NumarPlante);
            var totalRecoltari = context.RegistruRecoltare.Where(a => a.Stare == true).Count();
            var cantitateRecoltari = context.RegistruRecoltare.Where(a => a.Stare == true).Sum(p => p.CantitateRecoltata);
            var totalCopiliri = context.RegistruCopilire.Where(a => a.Stare == true).Count();
            var numarCopiliri = context.RegistruCopilire.Where(a => a.Stare == true).Sum(p => p.NumarPlante);

            var statisticiViewModel = new StatisticiViewModel
            {
                NumarParcele = numarParcele,
                SuprafataParcele= suprafataPlante,
                TotalPlante = totalPlante,
                TotalAngajati = totalAngajati,
                TotalRasaduri =totalRasaduri,
                TotalTratamente = totalTratamente,
                SuprafataTratamente = suprafataTratamente,
                TotalIrigari= totalIrigari,
                DurataIrigari= durataIrigari,
                TotalFertilizari = totalFertilizari,
                SuprafataFertilizari = suprafataFertilizari,
                TotalPalisari = totalPalisari,
                PlantePalisari = plantePalisari,
                TotalRecoltari= totalRecoltari,
                CantitateRecoltari = cantitateRecoltari,
                TotalCopiliri = totalCopiliri,
                PlanteCopilite = numarCopiliri
            };

            return View(statisticiViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contact()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            return View(contactViewModel);
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                Contact newContact = new Contact(
                    contactViewModel.CodContact,
                    contactViewModel.Nume,
                    contactViewModel.Email,
                    contactViewModel.Subiect,
                    contactViewModel.Mesaj
                );
                context.Contact.Add(newContact);
                context.SaveChanges();
                return Redirect("/Home/Contact");
            }
            return Contact();
        }
    }
}
