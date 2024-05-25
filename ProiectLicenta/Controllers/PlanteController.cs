using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class PlanteController : Controller
    {
        private ApplicationDbContext context;
        public PlanteController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Plante> planta = context.Plante.ToList();
            return View(planta);
        }

        [HttpGet]
        public IActionResult Adaugare()
        {
            AddPlantaViewModel addPlantaViewModel = new AddPlantaViewModel();
            return View(addPlantaViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddPlantaViewModel addPlantaViewModel)
        {
            if (ModelState.IsValid)
            {
                Plante newPlanta = new Plante(
                    addPlantaViewModel.CodPlanta,
                    addPlantaViewModel.Nume,
                    addPlantaViewModel.Descriere
                );
                context.Plante.Add(newPlanta);
                context.SaveChanges();
                return Redirect("/Plante");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Plante.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] plantaId)
        {
            foreach (int Id in plantaId)
            {
                Plante planta = context.Plante.Find(Id);
                context.Plante.Remove(planta);

            }
            context.SaveChanges();
            return Redirect("/Plante");
        }
    }
}
