using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<Rasaduri> rasad = SortData(context.Rasad.ToList(),sortOrder);
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
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Rasad.ToList();
            return View();
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
