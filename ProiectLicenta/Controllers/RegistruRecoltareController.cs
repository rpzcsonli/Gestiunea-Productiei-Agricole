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
    public class RegistruRecoltareController : Controller
    {

        private ApplicationDbContext context;
        public RegistruRecoltareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<RegistruRecoltare> registruRecoltare = context.RegistruRecoltare.Include(r => r.Parcela).ThenInclude(r => r.Rasaduri).ThenInclude(r => r.Plante).Include(r => r.Angajat).ToList();
            return View(registruRecoltare);
        }
        public void CheiExterne(AddRegistruRecoltareViewModel intrare)
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
            AddRegistruRecoltareViewModel addRegistruRecoltareViewModel = new AddRegistruRecoltareViewModel();
            CheiExterne(addRegistruRecoltareViewModel);
            addRegistruRecoltareViewModel.DataRecoltare = DateTime.Today;
            return View(addRegistruRecoltareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruRecoltareViewModel addRegistruRecoltareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruRecoltare newRecoltare = new RegistruRecoltare(
                    addRegistruRecoltareViewModel.CodRecoltare,
                    addRegistruRecoltareViewModel.CodParcela,
                    addRegistruRecoltareViewModel.CodAngajat,
                    addRegistruRecoltareViewModel.CantitateRecoltata,
                    addRegistruRecoltareViewModel.DataRecoltare
                );

                context.RegistruRecoltare.Add(newRecoltare);
                context.SaveChanges();
                return Redirect("/RegistruRecoltare");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruRecoltare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruRecoltareId)
        {
            foreach (int Id in registruRecoltareId)
            {
                RegistruRecoltare recoltare = context.RegistruRecoltare.Find(Id);
                context.RegistruRecoltare.Remove(recoltare);

            }
            context.SaveChanges();
            return Redirect("/RegistruRecoltare");
        }
    }
}
