using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;
using System.Data;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruCopilireController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruCopilireController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<RegistruCopilire> registruCopilire = context.RegistruCopilire.ToList();
            return View(registruCopilire);
        }
        
        public void CheiExterne(AddRegistruCopilireViewModel intrare)
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
            AddRegistruCopilireViewModel addRegistruCopilireViewModel = new AddRegistruCopilireViewModel();
           CheiExterne(addRegistruCopilireViewModel);
            addRegistruCopilireViewModel.DataCopilire =DateTime.Now;
            return View(addRegistruCopilireViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruCopilireViewModel addRegistruCopilireViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruCopilire newRegistruCopilire = new RegistruCopilire(
                    addRegistruCopilireViewModel.CodCopilire,
                    addRegistruCopilireViewModel.CodParcela,
                    addRegistruCopilireViewModel.NumarPlante,   
                    addRegistruCopilireViewModel.CodAngajat,
                    addRegistruCopilireViewModel.DataCopilire
                );

                context.RegistruCopilire.Add(newRegistruCopilire);
                context.SaveChanges();
                return Redirect("/RegistruCopilire");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruCopilire.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruCopilireId)
        {
            foreach (int Id in registruCopilireId)
            {
                RegistruCopilire registruCopilire = context.RegistruCopilire.Find(Id);
                context.RegistruCopilire.Remove(registruCopilire);

            }
            context.SaveChanges();
            return Redirect("/RegistruCopilire");
        }
    }
}
