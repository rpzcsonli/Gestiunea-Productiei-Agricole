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
    public class ParceleController : Controller
    {
        private readonly ApplicationDbContext context;
        public ParceleController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {if (int.TryParse(searchString, out int index))
                {
                    List<Parcela> dateParcela = SortData(context.Parcela.Include(r => r.Rasaduri).Where(a => a.Locatie.Contains(searchString) || a.Tip.Contains(searchString) ||
                    a.Rasaduri.Denumire.Contains(searchString) || a.Rasaduri.Planta.Contains(searchString) || a.NumarPlante == index || a.Suprafata == index).ToList(), sortOrder);
                    return View(dateParcela);
                }
                else
                {
                    List<Parcela> dateParcela = SortData(context.Parcela.Include(r => r.Rasaduri).Where(a => a.Locatie.Contains(searchString) || a.Tip.Contains(searchString) ||
                    a.Rasaduri.Denumire.Contains(searchString) || a.Rasaduri.Planta.Contains(searchString)).ToList(), sortOrder);
                    return View(dateParcela);
                }
            }
            List<Parcela> parcela = SortData(context.Parcela.Include(r => r.Rasaduri).ToList(), sortOrder);
            return View(parcela);
        }
        public void CheiExterne(AddParcelaViewModel intrare)
        {
            var codRasad = context.Rasad.Where(p=>p.Cantitate!=0).OrderBy(p => p.Denumire).ToList();
            ViewBag.codRasad = codRasad.Select(p => new SelectListItem
            {
                Value = p.CodRasad.ToString(),
                Text = p.Planta + "-" +p.Denumire + "-" +p.Cantitate + "Buc",
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
                var selectedRasaduri = context.Rasad.Find(addParcelaViewModel.CodRasad);
                if (selectedRasaduri != null && selectedRasaduri.Cantitate >= addParcelaViewModel.NumarPlante)
                {
                    selectedRasaduri.Cantitate -= addParcelaViewModel.NumarPlante;
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
                else
                {
                   
                    ModelState.AddModelError(string.Empty, "Numarul de plante este prea mare!");
                }
            }
            CheiExterne(addParcelaViewModel);
            return View(addParcelaViewModel);
        }
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<Parcela> dateParcela = context.Parcela.Include(r => r.Rasaduri).Where(a => a.Locatie.Contains(searchString) || a.Tip.Contains(searchString) ||
                    a.Rasaduri.Denumire.Contains(searchString) || a.Rasaduri.Planta.Contains(searchString) || a.NumarPlante == index || a.Suprafata == index).ToList();
                    return View(dateParcela);
                }
                else
                {
                    List<Parcela> dateParcela = context.Parcela.Include(r => r.Rasaduri).Where(a => a.Locatie.Contains(searchString) || a.Tip.Contains(searchString) ||
                    a.Rasaduri.Denumire.Contains(searchString) || a.Rasaduri.Planta.Contains(searchString)).ToList();
                    return View(dateParcela);
                }
            }
            List<Parcela> parcela = context.Parcela.Include(r => r.Rasaduri).ToList();
            return View(parcela);
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var parcela = context.Parcela.Find(id);
            if (parcela == null)
            {
                return NotFound();
            }

            var editParcelaViewModel = new AddParcelaViewModel
            {   
                CodParcela = parcela.CodParcela,
                Locatie = parcela.Locatie,
                Tip = parcela.Tip,
                CodRasad = parcela.CodRasad,
                NumarPlante= parcela.NumarPlante,
                Suprafata = parcela.Suprafata
            };
            CheiExterne(editParcelaViewModel);

            return View(editParcelaViewModel);
        }

        [HttpPost]
        public IActionResult Editare(AddParcelaViewModel editParcelaViewModel)
        {
            CheiExterne(editParcelaViewModel);
            if (ModelState.IsValid)
            {
                var parcela = context.Parcela.Find(editParcelaViewModel.CodParcela);
                var selectedRasaduri = context.Rasad.Find(editParcelaViewModel.CodRasad);
                parcela.Locatie = editParcelaViewModel.Locatie;
                parcela.Tip = editParcelaViewModel.Tip;
                parcela.CodRasad = editParcelaViewModel.CodRasad;
                parcela.Suprafata = editParcelaViewModel.Suprafata;
                if (selectedRasaduri != null && parcela.NumarPlante < editParcelaViewModel.NumarPlante)
                {
                    if (selectedRasaduri.Cantitate >= editParcelaViewModel.NumarPlante- parcela.NumarPlante)
                    {
                        selectedRasaduri.Cantitate -= editParcelaViewModel.NumarPlante - parcela.NumarPlante;
                        parcela.NumarPlante = editParcelaViewModel.NumarPlante;
                        context.SaveChanges();
                        return Redirect("/Parcele");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Numarul de plante este prea mare!");
                    }
                }
                else 
                if (selectedRasaduri != null && parcela.NumarPlante >= editParcelaViewModel.NumarPlante)
                {
                    selectedRasaduri.Cantitate += parcela.NumarPlante - editParcelaViewModel.NumarPlante;
                    parcela.NumarPlante = editParcelaViewModel.NumarPlante;
                    context.SaveChanges();
                    return Redirect("/Parcele");
                }
            }
            return View(editParcelaViewModel);
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

            if (propNume == "Rasaduri")
            {
                keySelector = item => typeof(T).GetProperty("Rasaduri")?.GetValue(item)?.GetType().GetProperty("Planta")?.GetValue(typeof(T).GetProperty("Rasaduri")?.GetValue(item));
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
