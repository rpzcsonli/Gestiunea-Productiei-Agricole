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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<RegistruCopilire> registruCopilire = SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).ToList(),sortOrder);
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
            addRegistruCopilireViewModel.DataCopilire = DateTime.Today;
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

            if (propNume == "Parcela")
            {
                keySelector = item => typeof(T).GetProperty("Parcela")?.GetValue(item)?.GetType().GetProperty("CodParcela")?.GetValue(typeof(T).GetProperty("Parcela")?.GetValue(item));
            }
            else
               if (propNume == "Angajat")
            {
                keySelector = item => typeof(T).GetProperty("Angajat")?.GetValue(item)?.GetType().GetProperty("Nume")?.GetValue(typeof(T).GetProperty("Angajat")?.GetValue(item));
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
