using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index()
        {
            List<Rasaduri> rasad = context.Rasad.ToList();
            return View(rasad);
        }

        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRasadViewModel addRasadViewModel = new AddRasadViewModel();
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
                    addRasadViewModel.CodPlanta,
                    addRasadViewModel.DataSemanat,
                    addRasadViewModel.DataMaturitate,
                    addRasadViewModel.Cantitate
                );
                context.Rasad.Add(newRasaduri);
                context.SaveChanges();
                return Redirect("/Rasaduri");
            }
            return View(addRasadViewModel);
        }
        public IActionResult Stergere()
        {
            ViewBag.events = context.Rasad.ToList();
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
    }
}
