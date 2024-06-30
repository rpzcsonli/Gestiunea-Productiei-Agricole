using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{[Authorize]
    public class RegistruIrigareController : Controller
    {
        
        private ApplicationDbContext context;
        public RegistruIrigareController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<RegistruIrigare> registruirigare = SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).ToList(),sortOrder);
            return View(registruirigare);
        }
        public void CheiExterne(AddRegistruIrigareViewModel intrare)
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
            AddRegistruIrigareViewModel adRegistruIrigareViewModel = new AddRegistruIrigareViewModel();
            CheiExterne(adRegistruIrigareViewModel);
            adRegistruIrigareViewModel.DataIrigare = DateTime.Today;
            return View(adRegistruIrigareViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddRegistruIrigareViewModel adRegistruIrigareViewModel)
        {
            if (ModelState.IsValid)
            {
                RegistruIrigare newregistruIrigare = new RegistruIrigare(
                    adRegistruIrigareViewModel.CodIrigare,
                    adRegistruIrigareViewModel.CodParcela,
                    adRegistruIrigareViewModel.DurataIrigare,
                    adRegistruIrigareViewModel.CodAngajat,
                    adRegistruIrigareViewModel.DataIrigare
                );

                context.RegistruIrigare.Add(newregistruIrigare);
                context.SaveChanges();
                return Redirect("/RegistruIrigare");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.RegistruIrigare.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] registruIrigareId)
        {
            foreach (int Id in registruIrigareId)
            {
                RegistruIrigare registruIrigare = context.RegistruIrigare.Find(Id);
                context.RegistruIrigare.Remove(registruIrigare);

            }
            context.SaveChanges();
            return Redirect("/RegistruIrigare");
        }
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var irigare = context.RegistruIrigare.Find(id);
            if (irigare == null)
            {
                return NotFound();
            }
            var editRegistruIrigareViewModel = new AddRegistruIrigareViewModel
            {
                CodIrigare = irigare.CodIrigare,
                CodParcela = irigare.CodParcela,
                DurataIrigare = irigare.DurataIrigare,
                CodAngajat = irigare.CodAngajat,
                DataIrigare = irigare.DataIrigare
            };
            CheiExterne(editRegistruIrigareViewModel);
            return View(editRegistruIrigareViewModel);
        }
        [HttpPost]
        public IActionResult Editare(AddRegistruIrigareViewModel editRegistruIrigareViewModel)
        {
            if (ModelState.IsValid)
            {
                var irigare = context.RegistruIrigare.Find(editRegistruIrigareViewModel.CodIrigare);
                if (irigare == null)
                {
                    return NotFound();
                }
                irigare.CodParcela = editRegistruIrigareViewModel.CodParcela;
                irigare.DurataIrigare = editRegistruIrigareViewModel.DurataIrigare;
                irigare.CodAngajat = editRegistruIrigareViewModel.CodAngajat;
                irigare.DataIrigare = editRegistruIrigareViewModel.DataIrigare;
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
