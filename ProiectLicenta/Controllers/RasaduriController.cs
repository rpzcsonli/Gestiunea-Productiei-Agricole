using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class RasaduriController : Controller
    {
        private ApplicationDbContext context;
        public RasaduriController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<Rasaduri> dateRasaduri = SortData(context.Rasad.Where(a => a.Denumire.Contains(searchString) || a.Planta.Contains(searchString) || a.Cantitate == index).ToList(), sortOrder);
                    return View(dateRasaduri);
                }
                else
                {
                    List<Rasaduri> dateRasaduri = SortData(context.Rasad.Where(a => a.Denumire.Contains(searchString) || a.Planta.Contains(searchString)).ToList(), sortOrder);
                    return View(dateRasaduri);
                }
            }
            List<Rasaduri> rasad = SortData(context.Rasad.ToList(), sortOrder);
            return View(rasad);
        }
       
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRasadViewModel addRasadViewModel = new AddRasadViewModel();
            addRasadViewModel.DataSemanat = DateTime.Today;
            addRasadViewModel.DataMaturitate = DateTime.Today;
            return View(addRasadViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRasadViewModel addRasadViewModel)
        {
            if (ModelState.IsValid)
            {
                Rasaduri newRasaduri = new Rasaduri(
                    addRasadViewModel.CodRasad,
                    addRasadViewModel.Denumire, 
                    addRasadViewModel.Planta,
                    addRasadViewModel.DataSemanat,
                    addRasadViewModel.DataMaturitate,
                    addRasadViewModel.Cantitate
                );
                context.Rasad.Add(newRasaduri);
                context.SaveChanges();
                return Redirect("/Rasaduri");
            }
            return Adaugare();
        }
        public IActionResult Stergere(string searchString )
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<Rasaduri> dateRasaduri = context.Rasad.Where(a => a.Denumire.Contains(searchString) || a.Planta.Contains(searchString) || a.Cantitate == index).ToList();
                    return View(dateRasaduri);
                }
                else
                {
                    List<Rasaduri> dateRasaduri = context.Rasad.Where(a => a.Denumire.Contains(searchString) || a.Planta.Contains(searchString)).ToList();
                    return View(dateRasaduri);
                }
            }
            List<Rasaduri> Rasaduri = context.Rasad.ToList();
            return View(Rasaduri);
        }
        [HttpPost]
        public IActionResult Stergere(int[] rasadId)
        {
            foreach (int Id in rasadId)
            {
                Rasaduri rasad = context.Rasad.Find(Id);
                context.Rasad.Remove(rasad);

            }
            context.SaveChanges();
            return Redirect("/Rasaduri");
        }
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var rasad = context.Rasad.Find(id);
            if (rasad == null)
            {
                return NotFound();
            }

            var editRasadViewModel = new AddRasadViewModel
            {   CodRasad = rasad.CodRasad,   
                Denumire = rasad.Denumire,
                   Planta= rasad.Planta,
                   DataSemanat = rasad.DataSemanat,
                   DataMaturitate= rasad.DataMaturitate,
                   Cantitate= rasad.Cantitate
            };

            return View(editRasadViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddRasadViewModel editRasadViewModel)
        {
            if (ModelState.IsValid)
            {
                var rasad = context.Rasad.Find(editRasadViewModel.CodRasad);
                if (rasad == null)
                {
                    return NotFound();
                }

                rasad.Denumire = editRasadViewModel.Denumire;
                rasad.Planta = editRasadViewModel.Planta;
                rasad.DataSemanat = editRasadViewModel.DataSemanat;
                rasad.DataMaturitate = editRasadViewModel.DataMaturitate;
                rasad.Cantitate = editRasadViewModel.Cantitate;
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
