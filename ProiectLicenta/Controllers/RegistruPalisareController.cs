using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<RegistruPalisare> registruPalisare = SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).ToList(),sortOrder);
            return View(registruPalisare);
        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRegistruPalisareViewModel addRegistruPalisareViewModel = new AddRegistruPalisareViewModel();
            CheiExterne(addRegistruPalisareViewModel);
            addRegistruPalisareViewModel.DataPalisare = DateTime.Today;
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
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruPalisare.ToList();
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
