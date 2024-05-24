using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class AngajatiController : Controller
    {
        private ApplicationDbContext context;
        public AngajatiController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        { 
                List<Angajat> angajat = context.Angajat.ToList();
                return View(angajat);
        }

        [HttpGet]
        public IActionResult Adaugare()
        {
            AddAngajatViewModel addAngajatViewModel = new AddAngajatViewModel();
            return View(addAngajatViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddAngajatViewModel addAngajatViewModel)
        {
            if (ModelState.IsValid)
            {
                Angajat newAngajat = new Angajat(
                    addAngajatViewModel.CodAngajat,
                    addAngajatViewModel.Nume,
                    addAngajatViewModel.Prenume,
                    addAngajatViewModel.Functie,
                    addAngajatViewModel.Telefon,
                    addAngajatViewModel.Email
                );

                context.Angajat.Add(newAngajat);
                context.SaveChanges();
                return Redirect("/Angajati");
            }
            return View(addAngajatViewModel);
        }
        public IActionResult Stergere()
        {
            ViewBag.events = context.Angajat.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] angajatId)
        {
            foreach (int Id in angajatId)
            {
               Angajat angajat = context.Angajat.Find(Id);
                context.Angajat.Remove(angajat);
                
            }
            context.SaveChanges();
            return Redirect("/Angajati");
        }

    }
}
