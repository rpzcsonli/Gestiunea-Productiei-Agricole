using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AngajatiController : Controller
    {
        private ApplicationDbContext context;
        public AngajatiController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString)) { 
            List<Angajat> angajati = context.Angajat.Where(a => a.Nume.Contains(searchString) ||
                                               a.Prenume.Contains(searchString) ||
                                               a.Functie.Contains(searchString) ||
                                               a.Telefon.Contains(searchString) ||
                                               a.Email.Contains(searchString)).ToList(); 
            angajati = SortData(angajati.ToList(), sortOrder);
                return View(angajati);
            }
                List<Angajat> angajat = SortData(context.Angajat.ToList(), sortOrder);
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
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                List<Angajat> angajati = context.Angajat.Where(a => a.Nume.Contains(searchString) ||
                                                   a.Prenume.Contains(searchString) ||
                                                   a.Functie.Contains(searchString) ||
                                                   a.Telefon.Contains(searchString) ||
                                                   a.Email.Contains(searchString)).ToList();

                return View(angajati);
            }
            return View(context.Angajat.ToList());
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var angajat = context.Angajat.Find(id);
            if (angajat == null)
            {
                return NotFound();
            }

            var editAngajatViewModel = new AddAngajatViewModel
            {
                CodAngajat = angajat.CodAngajat,
                Nume = angajat.Nume,
                Prenume = angajat.Prenume,
                Functie = angajat.Functie,
                Telefon = angajat.Telefon,
                Email = angajat.Email
            };

            return View(editAngajatViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddAngajatViewModel editAngajatViewModel)
        {
            if (ModelState.IsValid)
            {
                var angajat = context.Angajat.Find(editAngajatViewModel.CodAngajat);
                if (angajat == null)
                {
                    return NotFound();
                }

                angajat.Nume = editAngajatViewModel.Nume;
                angajat.Prenume = editAngajatViewModel.Prenume;
                angajat.Functie = editAngajatViewModel.Functie;
                angajat.Telefon = editAngajatViewModel.Telefon;
                angajat.Email = editAngajatViewModel.Email;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
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
