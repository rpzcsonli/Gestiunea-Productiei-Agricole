using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using System.Linq;
using System.Collections.Generic;
using ProiectLicenta.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class RaportParcelaController : Controller
    {
        private readonly ApplicationDbContext context;

        public RaportParcelaController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index(int? parcelaSelectata, string registruSelectat, string sortOrder)
        {
            ViewBag.ParcelaSelectata = parcelaSelectata;
            ViewBag.RegistruSelectat = registruSelectat;
            List<Parcela> parcele = context.Parcela.ToList();
            ViewBag.Parcele = parcele.Select(p => new SelectListItem
            {
                Value = p.CodParcela.ToString(),
                Text = p.Locatie,
                Selected = p.CodParcela == parcelaSelectata
            }).ToList();

            ViewBag.Registre = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "RegistruCopilire", Text = "Registru Copilire" },
                new SelectListItem { Value = "RegistruFertilizare", Text = "Registru Fertilizare" },
                new SelectListItem { Value = "RegistruIrigare", Text = "Registru Irigare" },
                new SelectListItem { Value = "RegistruPalisare", Text = "Registru Palisare" },
                new SelectListItem { Value = "RegistruRecoltare", Text = "Registru Recoltare" },
                new SelectListItem { Value = "RegistruTratamente", Text = "Registru Tratamente" }
            }, "Value", "Text", registruSelectat);

            ViewBag.SortOrder = sortOrder;

            if (parcelaSelectata.HasValue && parcelaSelectata.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var parcela = context.Parcela
                    .Include(p => p.RegistruCopilire)
                    .Include(p => p.RegistruFertilizare)
                    .Include(p => p.RegistruIrigare)
                    .Include(p => p.RegistruPalisare)
                    .Include(p => p.RegistruRecoltare)
                    .Include(p => p.RegistruTratamente)
                    .FirstOrDefault(p => p.CodParcela == parcelaSelectata.Value);

                var raportParcele = new RaportParcele
                {
                    CodParcela = parcela.CodParcela,
                    Locatie = parcela.Locatie,
                    Tip = parcela.Tip,
                    Suprafata = parcela.Suprafata,
                    NumarPlante = parcela.NumarPlante
                };

                switch (registruSelectat)
                {
                    case "RegistruCopilire":
                        raportParcele.RegistruCopilire = SortData(parcela.RegistruCopilire.Select(r => new RegistruCopilireDate
                        {
                            CodCopilire = r.CodCopilire,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataCopilire = r.DataCopilire
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruFertilizare":
                        raportParcele.RegistruFertilizare = SortData(parcela.RegistruFertilizare.Select(r => new RegistruFertilizareDate
                        {
                            CodFertilizare = r.CodFertilizare,
                            CodParcela = r.CodParcela,
                            Suprafata = r.Suprafata,
                            DataFertilizare = r.DataFertilizare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruIrigare":
                        raportParcele.RegistruIrigare = SortData(parcela.RegistruIrigare.Select(r => new RegistruIrigareDate
                        {
                            CodIrigare = r.CodIrigare,
                            CodParcela = r.CodParcela,
                            DurataIrigare = r.DurataIrigare,
                            DataIrigare = r.DataIrigare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruPalisare":
                        raportParcele.RegistruPalisare = SortData(parcela.RegistruPalisare.Select(r => new RegistruPalisareDate
                        {
                            CodPalisare = r.CodPalisare,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataPalisare = r.DataPalisare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruRecoltare":
                        raportParcele.RegistruRecoltare = SortData(parcela.RegistruRecoltare.Select(r => new RegistruRecoltareDate
                        {
                            CodRecoltare = r.CodRecoltare,
                            CodParcela = r.CodParcela,
                            CantitateRecoltata = r.CantitateRecoltata,
                            DataRecoltare = r.DataRecoltare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruTratamente":
                        raportParcele.RegistruTratamente = SortData(parcela.RegistruTratamente.Select(r => new RegistruTratamenteDate
                        {
                            CodTratamentAplicat = r.CodTratamentAplicat,
                            CodParcela = r.CodParcela,
                            CodDaunator = r.CodDaunator,
                            Suprafata = r.Suprafata,
                            DataAplicare = r.DataAplicare
                        }).ToList(), sortOrder);
                        break;
                }

                return View(new List<RaportParcele> { raportParcele });
            }

            return View(new List<RaportParcele>());
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
            var propInfo = typeof(T).GetProperty(propNume);

            if (propInfo == null)
            {
                return ListaDate;
            }

            if (descrescator)
            {
                ListaDate = ListaDate.OrderByDescending(x => propInfo.GetValue(x, null)).ToList();
            }
            else
            {
                ListaDate = ListaDate.OrderBy(x => propInfo.GetValue(x, null)).ToList();
            }
            return ListaDate;
        }
    }
}
