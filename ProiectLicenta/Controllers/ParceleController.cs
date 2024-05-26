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
    public class ParceleController : Controller
    {

        private ApplicationDbContext context;
        public ParceleController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Parcela> parcela = context.Parcela.Include(r => r.Rasaduri).ThenInclude(r => r.Plante).ToList();
            return View(parcela);
        }
        public void CheiExterne(AddParcelaViewModel intrare)
        {
            var codRasad = context.Rasad.OrderBy(p => p.Denumire).ToList();
            ViewBag.codRasad = codRasad.Select(p => new SelectListItem
            {
                Value = p.CodRasad.ToString(),
                Text = p.Denumire,
                Selected = p.CodRasad == intrare.CodRasad,
            });
            

        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddParcelaViewModel addParcelaViewModel = new AddParcelaViewModel();
            CheiExterne(addParcelaViewModel);
            return View(addParcelaViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddParcelaViewModel addParcelaViewModel)
        {
            if (ModelState.IsValid)
            {
                Parcela newParcela = new Parcela(
                    addParcelaViewModel.CodParcela,
                    addParcelaViewModel.Locatie,
                    addParcelaViewModel.Tip,
                    addParcelaViewModel.Suprafata,
                    addParcelaViewModel.CodRasad,
                    addParcelaViewModel.NumarPlante
                );

                context.Parcela.Add(newParcela);
                context.SaveChanges();
                return Redirect("/Parcele");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Parcela.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] parcelaId)
        {
            foreach (int Id in parcelaId)
            {
                Parcela parcela = context.Parcela.Find(Id);
                context.Parcela.Remove(parcela);

            }
            context.SaveChanges();
            return Redirect("/Parcele");
        }
    }
}
