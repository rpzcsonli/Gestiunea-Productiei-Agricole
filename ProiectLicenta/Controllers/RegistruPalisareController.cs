using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruPalisareController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruPalisareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        public void CheiExterne(AddRegistruPalisareViewModel intrare)
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
        public ActionResult Index()
        {
            List<RegistruPalisare> registruPalisare = context.RegistruPalisare.ToList();
            return View(registruPalisare);
        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRegistruPalisareViewModel addRegistruPalisareViewModel = new AddRegistruPalisareViewModel();
            CheiExterne(addRegistruPalisareViewModel);
            addRegistruPalisareViewModel.DataPalisare =DateTime.Now;
            return View(addRegistruPalisareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruPalisareViewModel addRegistruPalisareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruPalisare newRegistruPalisare = new RegistruPalisare(
                    addRegistruPalisareViewModel.CodPalisare,
                    addRegistruPalisareViewModel.CodParcela,
                    addRegistruPalisareViewModel.NumarPlante,
                    addRegistruPalisareViewModel.CodAngajat,
                    addRegistruPalisareViewModel.DataPalisare
                );

                context.RegistruPalisare.Add(newRegistruPalisare);
                context.SaveChanges();
                return Redirect("/RegistruPalisare");
            }
            return View(addRegistruPalisareViewModel);
        }
        public IActionResult Stergere()
        {
            ViewBag.events = context.RegistruPalisare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruPalisareId)
        {
            foreach (int Id in registruPalisareId)
            {
                RegistruPalisare registruPalisare = context.RegistruPalisare.Find(Id);
                context.RegistruPalisare.Remove(registruPalisare);

            }
            context.SaveChanges();
            return Redirect("/RegistruPalisare");
        }
    }
}
