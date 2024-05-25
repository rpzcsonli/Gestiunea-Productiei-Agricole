using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class RegistruTratamenteController : Controller
    {

        private ApplicationDbContext context;
        public RegistruTratamenteController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<RegistruTratamente> registruTratamente = context.RegistruTratamente.ToList();
            return View(registruTratamente);
        }
        public void CheiExterne(AddRegistruTratamenteViewModel intrare)
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
            var codDaunator = context.Daunatori.OrderBy(p => p.Denumire).ToList();
            ViewBag.codDaunator = codDaunator.Select(p => new SelectListItem
            {
                Value = p.CodDaunator.ToString(),
                Text = p.Denumire,
                Selected = p.CodDaunator == intrare.CodDaunator,
            });

        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRegistruTratamenteViewModel addRegistruTratamenteViewModel = new AddRegistruTratamenteViewModel();
            CheiExterne(addRegistruTratamenteViewModel);
            addRegistruTratamenteViewModel.DataAplicare = DateTime.Now;
            return View(addRegistruTratamenteViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruTratamenteViewModel addRegistruTratamenteViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruTratamente newRegistruTratamente = new RegistruTratamente(
                    addRegistruTratamenteViewModel.CodTratamentAplicat,
                    addRegistruTratamenteViewModel.CodParcela,
                    addRegistruTratamenteViewModel.CodDaunator,
                    addRegistruTratamenteViewModel.Suprafata,
                    addRegistruTratamenteViewModel.CodAngajat,
                    addRegistruTratamenteViewModel.DataAplicare
                );

                context.RegistruTratamente.Add(newRegistruTratamente);
                context.SaveChanges();
                return Redirect("/RegistruTratamente");
            }
            return View(addRegistruTratamenteViewModel);
        }
        public IActionResult Stergere()
        {
            ViewBag.events = context.RegistruTratamente.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruTratamenteId)
        {
            foreach (int Id in registruTratamenteId)
            {
                RegistruTratamente tratament = context.RegistruTratamente.Find(Id);
                context.RegistruTratamente.Remove(tratament);

            }
            context.SaveChanges();
            return Redirect("/RegistruTratamente");
        }
    }
}
