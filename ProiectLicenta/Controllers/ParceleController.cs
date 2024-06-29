using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<Parcela> parcela = SortData(context.Parcela.Include(r => r.Rasaduri).ToList(),sortOrder);
            return View(parcela);
        }
        public void CheiExterne(AddParcelaViewModel intrare)
        {
            var codRasad = context.Rasad.OrderBy(p => p.Denumire).ToList();
            ViewBag.codRasad = codRasad.Select(p => new SelectListItem
            {
                Value = p.CodRasad.ToString(),
                Text = p.Planta + "-" +p.Denumire,
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
