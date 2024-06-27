using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using System.Linq;
using System.Collections.Generic;
using ProiectLicenta.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class RaportRasadController : Controller
    {
        private readonly ApplicationDbContext context;

        public RaportRasadController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index(int? rasadSelectat, string registruSelectat, string sortOrder)
        {
            ViewBag.RasadSelectata = rasadSelectat;
            ViewBag.RegistruSelectat = registruSelectat;
            List<Rasaduri> rasaduri = context.Rasad.ToList();
            ViewBag.Rasaduri = rasaduri.Select(p => new SelectListItem
            {
                Value = p.CodRasad.ToString(),
                Text = p.Denumire,
                Selected = p.CodRasad == rasadSelectat
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

            if (rasadSelectat.HasValue && rasadSelectat.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var rasad = context.Rasad.First(r => r.CodRasad == rasadSelectat.Value);
                var parcele = context.Parcela
                    .Include(p => p.RegistruCopilire)
                    .Include(p => p.RegistruFertilizare)
                    .Include(p => p.RegistruIrigare)
                    .Include(p => p.RegistruPalisare)
                    .Include(p => p.RegistruRecoltare)
                    .Include(p => p.RegistruTratamente)
                    .Where(p => p.CodRasad == rasadSelectat.Value)
                    .ToList();

                var raportData = new RaportRasad
                {
                    CodRasad = rasadSelectat.Value,
                    Denumire = rasad.Denumire,
                    Planta = rasad.Planta,
                    Cantitate = rasad.Cantitate,
                    Parcele = new List<Parcela>(),
                    RegistruCopilire = new List<RegistruCopilireDate>(),
                    RegistruFertilizare = new List<RegistruFertilizareDate>(),
                    RegistruIrigare = new List<RegistruIrigareDate>(),
                    RegistruPalisare= new List<RegistruPalisareDate>(),
                    RegistruRecoltare = new List<RegistruRecoltareDate>(),
                    RegistruTratamente= new List<RegistruTratamenteDate>()
                };

                foreach (var parcela in parcele)
                {
                    raportData.Parcele.Add( new Parcela(
                        parcela.CodParcela,
                        parcela.Locatie,
                        parcela.Tip,
                        parcela.Suprafata,
                        parcela.CodRasad,
                        parcela.NumarPlante)
                    );

                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            
                                raportData.RegistruCopilire.AddRange(parcela.RegistruCopilire.Select(r => new RegistruCopilireDate
                                {
                                    CodCopilire = r.CodCopilire,
                                    CodParcela = r.CodParcela,
                                    NumarPlante = r.NumarPlante,
                                    DataCopilire = r.DataCopilire
                                }).ToList());
                            break;
                        case "RegistruFertilizare":
                            raportData.RegistruFertilizare.AddRange(parcela.RegistruFertilizare.Select(r => new RegistruFertilizareDate
                            {
                                CodFertilizare = r.CodFertilizare,
                                CodParcela = r.CodParcela,
                                Suprafata = r.Suprafata,
                                DataFertilizare = r.DataFertilizare
                            }).ToList());
                            break;
                        case "RegistruIrigare":
                            raportData.RegistruIrigare.AddRange(parcela.RegistruIrigare.Select(r => new RegistruIrigareDate
                            {
                                CodIrigare = r.CodIrigare,
                                CodParcela = r.CodParcela,
                                DurataIrigare = r.DurataIrigare,
                                DataIrigare = r.DataIrigare
                            }).ToList());
                            break;
                        case "RegistruPalisare":
                            raportData.RegistruPalisare.AddRange(parcela.RegistruPalisare.Select(r => new RegistruPalisareDate
                            {
                                CodPalisare = r.CodPalisare,
                                CodParcela = r.CodParcela,
                                NumarPlante = r.NumarPlante,
                                DataPalisare = r.DataPalisare
                            }).ToList());
                            break;
                        case "RegistruRecoltare":
                            raportData.RegistruRecoltare.AddRange(parcela.RegistruRecoltare.Select(r => new RegistruRecoltareDate
                            {
                                CodRecoltare = r.CodRecoltare,
                                CodParcela = r.CodParcela,
                                CantitateRecoltata = r.CantitateRecoltata,
                                DataRecoltare = r.DataRecoltare
                            }).ToList());
                            break;
                        case "RegistruTratamente":
                            raportData.RegistruTratamente.AddRange(parcela.RegistruTratamente.Select(r => new RegistruTratamenteDate
                            {
                                CodTratamentAplicat = r.CodTratamentAplicat,
                                CodParcela = r.CodParcela,
                                CodDaunator = r.CodDaunator,
                                Suprafata = r.Suprafata,
                                DataAplicare = r.DataAplicare
                            }).ToList());
                            break;
                    }

                }
                switch (registruSelectat)
                {
                    case "RegistruCopilire":
                        raportData.RegistruCopilire = SortData(raportData.RegistruCopilire,sortOrder);
                        break;
                    case "RegistruFertilizare":
                        raportData.RegistruFertilizare = SortData(raportData.RegistruFertilizare, sortOrder);
                        break;
                    case "RegistruIrigare":
                        raportData.RegistruIrigare = SortData(raportData.RegistruIrigare, sortOrder);
                        break;
                    case "RegistruPalisare":
                        raportData.RegistruPalisare = SortData(raportData.RegistruPalisare, sortOrder);
                        break;
                    case "RegistruRecoltare":
                        raportData.RegistruRecoltare = SortData(raportData.RegistruRecoltare, sortOrder);
                        break;
                    case "RegistruTratamente":
                        raportData.RegistruTratamente = SortData(raportData.RegistruTratamente, sortOrder);
                        break;
                }
                return View(new List<RaportRasad> { raportData });
            }

            return View(new List<RaportRasad>());
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
