using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruFertilizareController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruFertilizareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<RegistruFertilizare> registruFertilizare = SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).ToList(),sortOrder);
            return View(registruFertilizare);
        }
        public void CheiExterne(AddRegistruFertilizareViewModel intrare)
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
            AddRegistruFertilizareViewModel addRegistruFertilizareViewModel = new AddRegistruFertilizareViewModel();
            CheiExterne(addRegistruFertilizareViewModel);
            addRegistruFertilizareViewModel.DataFertilizare = DateTime.Today;
            return View(addRegistruFertilizareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruFertilizareViewModel addRegistruFertilizareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruFertilizare newRegistruFertilizare = new RegistruFertilizare(
                    addRegistruFertilizareViewModel.CodFertilizare,
                    addRegistruFertilizareViewModel.CodParcela,
                    addRegistruFertilizareViewModel.Suprafata,
                    addRegistruFertilizareViewModel.CodAngajat,
                    addRegistruFertilizareViewModel.DataFertilizare
                );

                context.RegistruFertilizare.Add(newRegistruFertilizare);
                context.SaveChanges();
                return Redirect("/RegistruFertilizare");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruFertilizare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruFertilizareId)
        {
            foreach (int Id in registruFertilizareId)
            {
                RegistruFertilizare registruFertilizare = context.RegistruFertilizare.Find(Id);
                context.RegistruFertilizare.Remove(registruFertilizare);

            }
            context.SaveChanges();
            return Redirect("/RegistruFertilizare");
        }
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var fertilizare = context.RegistruFertilizare.Find(id);
            if (fertilizare == null)
            {
                return NotFound();
            }

            var editRegistruFertilizareViewModel = new AddRegistruFertilizareViewModel
            {
                CodFertilizare = fertilizare.CodFertilizare,
                CodParcela = fertilizare.CodParcela,
                Suprafata = fertilizare.Suprafata,
                CodAngajat = fertilizare.CodAngajat,
                DataFertilizare = fertilizare.DataFertilizare
            };
            CheiExterne(editRegistruFertilizareViewModel);

            return View(editRegistruFertilizareViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddRegistruFertilizareViewModel editRegistruFertilizareViewModel)
        {
            if (ModelState.IsValid)
            {
                var fertilizare = context.RegistruFertilizare.Find(editRegistruFertilizareViewModel.CodFertilizare);
                if (fertilizare == null)
                {
                    return NotFound();
                }
                fertilizare.CodParcela = editRegistruFertilizareViewModel.CodParcela;
                fertilizare.Suprafata = editRegistruFertilizareViewModel.Suprafata;
                fertilizare.CodAngajat = editRegistruFertilizareViewModel.CodAngajat;
                fertilizare.DataFertilizare = editRegistruFertilizareViewModel.DataFertilizare;
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
