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
    public class RegistruTratamenteController : Controller
    {

        private ApplicationDbContext context;
        public RegistruTratamenteController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruTratamente> datetratamente = SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(a => a.Parcela.Locatie.Contains(searchString) && a.Stare == true ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.Daunatori.Denumire.Contains(searchString) || a.Daunatori.Tratament.Denumire.Contains(searchString) || a.Suprafata == index).ToList(), sortOrder);
                    return View(datetratamente);
                }
                else
                {
                    List<RegistruTratamente> datetratamente = SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(a => a.Parcela.Locatie.Contains(searchString) && a.Stare == true ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.Daunatori.Denumire.Contains(searchString) || a.Daunatori.Tratament.Denumire.Contains(searchString)).ToList(), sortOrder);
                    return View(datetratamente);
                }
            }
            List<RegistruTratamente> registruTratamente = SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(a => a.Stare == true).ToList(), sortOrder);
            return View(registruTratamente);
        }
        public void CheiExterne(AddRegistruTratamenteViewModel intrare)
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
            var codDaunator = context.Daunatori.OrderBy(p => p.Denumire).ToList();
            ViewBag.codDaunator = codDaunator.Select(p => new SelectListItem
            {
                Value = p.CodDaunator.ToString(),
                Text = p.Denumire,
                Selected = p.CodDaunator == intrare.CodDaunator,
            });

        }
        [HttpGet]
        public IActionResult Adaugare()
        {
            AddRegistruTratamenteViewModel addRegistruTratamenteViewModel = new AddRegistruTratamenteViewModel();
            CheiExterne(addRegistruTratamenteViewModel);
            addRegistruTratamenteViewModel.DataAplicare = DateTime.Today;
            return View(addRegistruTratamenteViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruTratamenteViewModel addRegistruTratamenteViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruTratamente newRegistruTratamente = new RegistruTratamente(
                    addRegistruTratamenteViewModel.CodTratamentAplicat,
                    addRegistruTratamenteViewModel.CodParcela,
                    addRegistruTratamenteViewModel.CodDaunator,
                    addRegistruTratamenteViewModel.Suprafata,
                    addRegistruTratamenteViewModel.CodAngajat,
                    addRegistruTratamenteViewModel.DataAplicare,
                    addRegistruTratamenteViewModel.Stare
                );

                context.RegistruTratamente.Add(newRegistruTratamente);
                context.SaveChanges();
                return Redirect("/RegistruTratamente");
            }
            return Adaugare();
        }
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruTratamente> datetratamente = context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.Daunatori.Denumire.Contains(searchString) || a.Daunatori.Tratament.Denumire.Contains(searchString) || a.Suprafata == index).ToList();
                    return View(datetratamente);
                }
                else
                {
                    List<RegistruTratamente> datetratamente = context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.Daunatori.Denumire.Contains(searchString) || a.Daunatori.Tratament.Denumire.Contains(searchString)).ToList();
                    return View(datetratamente);
                }
            }
            List<RegistruTratamente> registruTratamente = context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).ToList();
            return View(registruTratamente);
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruTratamenteId)
        {
            foreach (int Id in registruTratamenteId)
            {
                RegistruTratamente tratament = context.RegistruTratamente.Find(Id);
                context.RegistruTratamente.Remove(tratament);

            }
            context.SaveChanges();
            return Redirect("/RegistruTratamente");
        }
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var tratament = context.RegistruTratamente.Find(id);
            if (tratament == null)
            {
                return NotFound();
            }

            var editRegistruTratamenteViewModel = new AddRegistruTratamenteViewModel
            {
                CodTratamentAplicat = tratament.CodTratamentAplicat,
                CodParcela = tratament.CodParcela,
                CodDaunator = tratament.CodDaunator,
                CodAngajat = tratament.CodAngajat,
                Suprafata = tratament.Suprafata,
                DataAplicare = tratament.DataAplicare
            };
            CheiExterne(editRegistruTratamenteViewModel);

            return View(editRegistruTratamenteViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddRegistruTratamenteViewModel editRegistruTratamenteViewModel)
        {
            if (ModelState.IsValid)
            {
                var tratament = context.RegistruTratamente.Find(editRegistruTratamenteViewModel.CodTratamentAplicat);
                if (tratament == null)
                {
                    return NotFound();
                }
                tratament.CodParcela = editRegistruTratamenteViewModel.CodParcela;
                tratament.CodDaunator = editRegistruTratamenteViewModel.CodDaunator;
                tratament.Suprafata = editRegistruTratamenteViewModel.Suprafata;
                tratament.CodAngajat = editRegistruTratamenteViewModel.CodAngajat;
                tratament.DataAplicare = editRegistruTratamenteViewModel.DataAplicare;
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
               if (propNume == "Daunator")
            {
                keySelector = item => typeof(T).GetProperty("Daunatori")?.GetValue(item)?.GetType().GetProperty("Denumire")?.GetValue(typeof(T).GetProperty("Daunatori")?.GetValue(item));
            }
            else
               if (propNume == "Tratament")
            {
                keySelector = item =>
                {
                    var tratament = typeof(T).GetProperty("Daunatori")?.GetValue(item)?.GetType().GetProperty("Tratament")?.GetValue(typeof(T).GetProperty("Daunatori")?.GetValue(item));
                    return tratament?.GetType().GetProperty("Denumire")?.GetValue(tratament);
                };
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
