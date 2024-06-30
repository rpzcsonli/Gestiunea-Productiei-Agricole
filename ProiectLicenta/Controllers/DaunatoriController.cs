using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                List<Daunatori> daunatori = context.Daunatori.Include(r => r.Tratament).Where(a => a.Denumire.Contains(searchString) ||
                a.Tratament.Denumire.Contains(searchString)).ToList();
                daunatori = SortData(daunatori.ToList(), sortOrder);
                return View(daunatori);
            }
            List<Daunatori> daunator = SortData(context.Daunatori.Include(r => r.Tratament).ToList(),sortOrder);
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
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                List<Daunatori> daunatori = context.Daunatori.Include(r => r.Tratament).Where(a => a.Denumire.Contains(searchString) ||
                a.Tratament.Denumire.Contains(searchString)).ToList();
                return View(daunatori);
            }
            return View(context.Daunatori.Include(r => r.Tratament).ToList());
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var daunator = context.Daunatori.Find(id);
            if (daunator == null)
            {
                return NotFound();
            }

            var editDaunatorViewModel = new AddDaunatorViewModel
            {
                CodDaunator = daunator.CodDaunator,
                Denumire = daunator.Denumire,
                CodTratament = daunator.CodTratament
            };
            CheiExterne(editDaunatorViewModel);

            return View(editDaunatorViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddDaunatorViewModel editDaunatorViewModel)
        {
            if (ModelState.IsValid)
            {
                var daunator = context.Daunatori.Find(editDaunatorViewModel.CodDaunator);
                if (daunator == null)
                {
                    return NotFound();
                }
                daunator.Denumire= editDaunatorViewModel.Denumire;
                daunator.CodTratament = editDaunatorViewModel.CodTratament;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private List<T> SortData<T>(List<T> ListaDate, string Ordine)
        {
            if (ListaDate == null || !ListaDate.Any())
            {
                return ListaDate;
            }

            if (string.IsNullOrEmpty(Ordine))
            {
                return ListaDate;
            }

            var descrescator = Ordine.EndsWith("_desc");
            var propNume = descrescator ? Ordine.Substring(0, Ordine.Length - 5) : Ordine;
            Func<T, object> keySelector;

            if (propNume == "Tratament")
            {
                keySelector = item => typeof(T).GetProperty("Tratament")?.GetValue(item)?.GetType().GetProperty("Denumire")?.GetValue(typeof(T).GetProperty("Tratament")?.GetValue(item));
            }
            else
            {
                var propInfo = typeof(T).GetProperty(propNume);
                if (propInfo == null)
                {
                    return ListaDate;
                }
                keySelector = item => propInfo.GetValue(item);
            }

            if (descrescator)
            {
                ListaDate = ListaDate.OrderByDescending(keySelector).ToList();
            }
            else
            {
                ListaDate = ListaDate.OrderBy(keySelector).ToList();
            }
            return ListaDate;
        }
    }
}
