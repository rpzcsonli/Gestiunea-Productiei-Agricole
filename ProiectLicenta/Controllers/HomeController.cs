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
            var totalParcele = context.Parcela.Count();
            var totalPlante = context.Parcela.Sum(p => p.NumarPlante);
            var totalAngajati = context.Angajat.Count();
            var totalRasaduri = context.Rasad.Sum(p => p.Cantitate);
            var totalTratamente = context.Tratament.Count();
            var totalIrigari = context.RegistruIrigare.Count();
            var totalFertilizari = context.RegistruFertilizare.Count();
            var totalPalisari = context.RegistruPalisare.Count();
            var totalRecoltari = context.RegistruRecoltare.Sum(p => p.CantitateRecoltata);

            var statisticiViewModel = new StatisticiViewModel
            {
                TotalParcele = totalParcele,
                TotalPlante = totalPlante,
                TotalAngajati = totalAngajati,
                TotalRasaduri = totalRasaduri,
                TotalTratamente = totalTratamente,
                TotalIrigari = totalIrigari,
                TotalFertilizari = totalFertilizari,
                TotalPalisari = totalPalisari,
                TotalRecoltari = totalRecoltari
            };

            return View(statisticiViewModel);
        }


        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
