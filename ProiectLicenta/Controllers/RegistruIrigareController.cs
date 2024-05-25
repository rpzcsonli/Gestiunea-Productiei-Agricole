using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruIrigareController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruIrigareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<RegistruIrigare> registruirigare = context.RegistruIrigare.ToList();
            return View(registruirigare);
        }
        public void CheiExterne(AddRegistruIrigareViewModel intrare)
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
            AddRegistruIrigareViewModel adRegistruIrigareViewModel = new AddRegistruIrigareViewModel();
            CheiExterne(adRegistruIrigareViewModel);
            adRegistruIrigareViewModel.DataIrigare =DateTime.Now;
            return View(adRegistruIrigareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruIrigareViewModel adRegistruIrigareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruIrigare newregistruIrigare = new RegistruIrigare(
                    adRegistruIrigareViewModel.CodIrigare,
                    adRegistruIrigareViewModel.CodParcela,
                    adRegistruIrigareViewModel.DurataIrigare,
                    adRegistruIrigareViewModel.CodAngajat,
                    adRegistruIrigareViewModel.DataIrigare
                );

                context.RegistruIrigare.Add(newregistruIrigare);
                context.SaveChanges();
                return Redirect("/RegistruIrigare");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruIrigare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruIrigareId)
        {
            foreach (int Id in registruIrigareId)
            {
                RegistruIrigare registruIrigare = context.RegistruIrigare.Find(Id);
                context.RegistruIrigare.Remove(registruIrigare);

            }
            context.SaveChanges();
            return Redirect("/RegistruIrigare");
        }
    }
}
