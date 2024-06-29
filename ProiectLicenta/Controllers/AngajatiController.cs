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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<Angajat> angajat = SortData(context.Angajat.ToList(),sortOrder);
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
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Angajat.ToList();
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
        private List<T> SortData<T>(List<T> ListaDate, string Ordine)
        {
            if (ListaDate == null || !ListaDate.Any())
            {
                return ListaDate;
            }

            if (string.IsNullOrEmpty(Ordine))
            {
                return ListaDate;
            }

            var descrescator = Ordine.EndsWith("_desc");
            var propNume = descrescator ? Ordine.Substring(0, Ordine.Length - 5) : Ordine;
            var propInfo = typeof(T).GetProperty(propNume);

            if (propInfo == null)
            {
                return ListaDate;
            }

            if (descrescator)
            {
                ListaDate = ListaDate.OrderByDescending(x => propInfo.GetValue(x, null)).ToList();
            }
            else
            {
                ListaDate = ListaDate.OrderBy(x => propInfo.GetValue(x, null)).ToList();
            }
            return ListaDate;
        }

    }
}
