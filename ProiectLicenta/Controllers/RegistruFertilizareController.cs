using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruFertilizareController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruFertilizareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<RegistruFertilizare> registruFertilizare = context.RegistruFertilizare.ToList();
            return View(registruFertilizare);
        }
        public void CheiExterne(AddRegistruFertilizareViewModel intrare)
        {
            var codAngajat = context.Angajat.OrderBy(p => p.Nume).ToList();
            ViewBag.codAngajat = codAngajat.Select(p => new SelectListItem
            {
                Value = p.CodAngajat.ToString(),
                Text = p.Nume + " " + p.Prenume,
                Selected = p.CodAngajat == intrare.CodAngajat,
            });
            var codParcela = context.Parcela.OrderBy(p => p.Locatie).ToList();
            ViewBag.codParcela = codParcela.Select(p => new SelectListItem
            {
                Value = p.CodParcela.ToString(),
                Text = p.Locatie,
                Selected = p.CodParcela == intrare.CodParcela,
            });

        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRegistruFertilizareViewModel addRegistruFertilizareViewModel = new AddRegistruFertilizareViewModel();
            CheiExterne(addRegistruFertilizareViewModel);
            addRegistruFertilizareViewModel.DataFertilizare = DateTime.Now;
            return View(addRegistruFertilizareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruFertilizareViewModel addRegistruFertilizareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruFertilizare newRegistruFertilizare = new RegistruFertilizare(
                    addRegistruFertilizareViewModel.CodFertilizare,
                    addRegistruFertilizareViewModel.CodParcela,
                    addRegistruFertilizareViewModel.Suprafata,
                    addRegistruFertilizareViewModel.CodAngajat,
                    addRegistruFertilizareViewModel.DataFertilizare
                );

                context.RegistruFertilizare.Add(newRegistruFertilizare);
                context.SaveChanges();
                return Redirect("/RegistruFertilizare");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruFertilizare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruFertilizareId)
        {
            foreach (int Id in registruFertilizareId)
            {
                RegistruFertilizare registruFertilizare = context.RegistruFertilizare.Find(Id);
                context.RegistruFertilizare.Remove(registruFertilizare);

            }
            context.SaveChanges();
            return Redirect("/RegistruFertilizare");
        }
    }
}
