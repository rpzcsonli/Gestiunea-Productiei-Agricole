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
        public ActionResult Index()
        {
            List<Rasaduri> rasad = context.Rasad.Include(r => r.Plante).ToList();
            return View(rasad);
        }
        public void CheiExterne(AddRasadViewModel intrare)
        {
            var codPlanta = context.Plante.OrderBy(p => p.Nume).ToList();
            ViewBag.codPlanta = codPlanta.Select(p => new SelectListItem
            {
                Value = p.CodPlanta.ToString(),
                Text = p.CodPlanta + " - "+p.Nume,
                Selected = p.CodPlanta == intrare.CodPlanta,
            });
        }

        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRasadViewModel addRasadViewModel = new AddRasadViewModel();
            CheiExterne(addRasadViewModel);
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
                    addRasadViewModel.CodPlanta,
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
    }
}
