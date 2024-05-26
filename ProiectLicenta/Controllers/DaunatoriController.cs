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
    public class DaunatoriController : Controller
    {

        private ApplicationDbContext context;
        public DaunatoriController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Daunatori> daunator = context.Daunatori.Include(r => r.Tratament).ToList();
            return View(daunator);
        }
        public void CheiExterne(AddDaunatorViewModel intrare)
        {
            var codTratament = context.Tratament.OrderBy(p => p.Denumire).ToList();
            ViewBag.codTratament = codTratament.Select(p => new SelectListItem
            {
                Value = p.CodTratament.ToString(),
                Text = p.Denumire,
                Selected = p.CodTratament == intrare.CodTratament,
            });
        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddDaunatorViewModel addDaunatorViewModel = new AddDaunatorViewModel();
            CheiExterne(addDaunatorViewModel);
            return View(addDaunatorViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddDaunatorViewModel addDaunatorViewModel)
        {
            if (ModelState.IsValid)
            {
                Daunatori newDaunator = new Daunatori(
                    addDaunatorViewModel.CodDaunator,
                    addDaunatorViewModel.Denumire,
                    addDaunatorViewModel.CodTratament
                );

                context.Daunatori.Add(newDaunator);
                context.SaveChanges();
                return Redirect("/Daunatori");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Daunatori.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] daunatorId)
        {
            foreach (int Id in daunatorId)
            {
                Daunatori daunator = context.Daunatori.Find(Id);
                context.Daunatori.Remove(daunator);

            }
            context.SaveChanges();
            return Redirect("/Daunatori");
        }
    }
}
