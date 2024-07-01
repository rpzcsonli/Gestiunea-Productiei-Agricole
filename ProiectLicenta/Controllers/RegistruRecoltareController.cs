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
    public class RegistruRecoltareController : Controller
    {

        private ApplicationDbContext context;
        public RegistruRecoltareController(ApplicationDbContext dbcontext)
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
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruRecoltare> dateRecoltare = SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.CantitateRecoltata == index).ToList(), sortOrder);
                    return View(dateRecoltare);
                }
                else
                {
                    List<RegistruRecoltare> dateRecoltare = SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString)).ToList(), sortOrder);
                    return View(dateRecoltare);
                }
            }
            List<RegistruRecoltare> registruRecoltare = SortData(context.RegistruRecoltare.Include(r => r.Parcela).ThenInclude(r => r.Rasaduri).Include(r => r.Angajat).ToList(),sortOrder);
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
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruRecoltare> dateRecoltare = context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.CantitateRecoltata == index).ToList();
                    return View(dateRecoltare);
                }
                else
                {
                    List<RegistruRecoltare> dateRecoltare = context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString)).ToList();
                    return View(dateRecoltare);
                }
            }
            List<RegistruRecoltare> registruRecoltare = context.RegistruRecoltare.Include(r => r.Parcela).ThenInclude(r => r.Rasaduri).Include(r => r.Angajat).ToList();
            return View(registruRecoltare);
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var recoltare = context.RegistruRecoltare.Find(id);
            if (recoltare == null)
            {
                return NotFound();
            }

            var editRegistruRecoltareViewModel = new AddRegistruRecoltareViewModel
            {
                CodRecoltare = recoltare.CodRecoltare,
                CodParcela = recoltare.CodParcela,
                CodAngajat = recoltare.CodAngajat,
                CantitateRecoltata = recoltare.CantitateRecoltata,
                DataRecoltare = recoltare.DataRecoltare
            };
            CheiExterne(editRegistruRecoltareViewModel);

            return View(editRegistruRecoltareViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddRegistruRecoltareViewModel editRegistruRecoltareViewModel)
        {
            if (ModelState.IsValid)
            {
                var recoltare = context.RegistruRecoltare.Find(editRegistruRecoltareViewModel.CodRecoltare);
                if (recoltare == null)
                {
                    return NotFound();
                }
                recoltare.CodParcela = editRegistruRecoltareViewModel.CodParcela;
                recoltare.CantitateRecoltata = editRegistruRecoltareViewModel.CantitateRecoltata;
                recoltare.CodAngajat = editRegistruRecoltareViewModel.CodAngajat;
                recoltare.DataRecoltare = editRegistruRecoltareViewModel.DataRecoltare;
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
