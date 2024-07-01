using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruPalisare> datePalisare = SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.NumarPlante == index).ToList(), sortOrder);
                    return View(datePalisare);
                }
                else
                {
                    List<RegistruPalisare> datePalisare = SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString)).ToList(), sortOrder);
                    return View(datePalisare);
                }
            }
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
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<RegistruPalisare> datePalisare = context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString) || a.NumarPlante == index).ToList();
                    return View(datePalisare);
                }
                else
                {
                    List<RegistruPalisare> datePalisare = context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(a => a.Parcela.Locatie.Contains(searchString) ||
                 a.Angajat.Nume.Contains(searchString) || a.Angajat.Prenume.Contains(searchString)).ToList();
                    return View(datePalisare);
                }
            }
            List<RegistruPalisare> registruPalisare = context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).ToList();
            return View(registruPalisare);
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var palisare = context.RegistruPalisare.Find(id);
            if (palisare == null)
            {
                return NotFound();
            }

            var editRegistruPalisareViewModel = new AddRegistruPalisareViewModel
            {
                CodPalisare = palisare.CodPalisare,
                CodParcela = palisare.CodParcela,
                NumarPlante = palisare.NumarPlante,
                CodAngajat = palisare.CodAngajat,
                DataPalisare = palisare.DataPalisare
            };
            CheiExterne(editRegistruPalisareViewModel);

            return View(editRegistruPalisareViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddRegistruPalisareViewModel editRegistruPalisareViewModel)
        {
            if (ModelState.IsValid)
            {
                var palisare = context.RegistruPalisare.Find(editRegistruPalisareViewModel.CodPalisare);
                if (palisare == null)
                {
                    return NotFound();
                }
                palisare.CodParcela = editRegistruPalisareViewModel.CodParcela;
                palisare.NumarPlante = editRegistruPalisareViewModel.NumarPlante;
                palisare.CodAngajat = editRegistruPalisareViewModel.CodAngajat;
                palisare.DataPalisare = editRegistruPalisareViewModel.DataPalisare;
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
