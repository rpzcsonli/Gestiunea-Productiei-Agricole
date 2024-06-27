using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class RaportAngajatiController : Controller
    {
        private readonly ApplicationDbContext context;

        public RaportAngajatiController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index(int? angajatSelectat, string registruSelectat, string sortOrder)
        {
            ViewBag.AngajatSelectata = angajatSelectat;
            ViewBag.RegistruSelectat = registruSelectat;
            List<Angajat> angajati = context.Angajat.ToList();
            ViewBag.Angajati = angajati.Select(p => new SelectListItem
            {
                Value = p.CodAngajat.ToString(),
                Text = p.Nume + " " + p.Prenume,
                Selected = p.CodAngajat == angajatSelectat
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

            if (angajatSelectat.HasValue && angajatSelectat.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var angajat = context.Angajat
                    .Include(p => p.RegistruCopilire)
                    .Include(p => p.RegistruFertilizare)
                    .Include(p => p.RegistruIrigare)
                    .Include(p => p.RegistruPalisare)
                    .Include(p => p.RegistruRecoltare)
                    .Include(p => p.RegistruTratamente)
                    .FirstOrDefault(p => p.CodAngajat == angajatSelectat.Value);

                var raportAngajati = new RaportAngajati
                {
                    CodAngajat = angajat.CodAngajat,
                    Nume = angajat.Nume,
                    Prenume = angajat.Prenume,
                    Functie = angajat.Functie
                };

                switch (registruSelectat)
                {
                    case "RegistruCopilire":
                        raportAngajati.RegistruCopilire = SortData(angajat.RegistruCopilire.Select(r => new RegistruCopilireDate
                        {
                            CodCopilire = r.CodCopilire,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataCopilire = r.DataCopilire
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruFertilizare":
                        raportAngajati.RegistruFertilizare = SortData(angajat.RegistruFertilizare.Select(r => new RegistruFertilizareDate
                        {
                            CodFertilizare = r.CodFertilizare,
                            CodParcela = r.CodParcela,
                            Suprafata = r.Suprafata,
                            DataFertilizare = r.DataFertilizare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruIrigare":
                        raportAngajati.RegistruIrigare = SortData(angajat.RegistruIrigare.Select(r => new RegistruIrigareDate
                        {
                            CodIrigare = r.CodIrigare,
                            CodParcela = r.CodParcela,
                            DurataIrigare = r.DurataIrigare,
                            DataIrigare = r.DataIrigare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruPalisare":
                        raportAngajati.RegistruPalisare = SortData(angajat.RegistruPalisare.Select(r => new RegistruPalisareDate
                        {
                            CodPalisare = r.CodPalisare,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataPalisare = r.DataPalisare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruRecoltare":
                        raportAngajati.RegistruRecoltare = SortData(angajat.RegistruRecoltare.Select(r => new RegistruRecoltareDate
                        {
                            CodRecoltare = r.CodRecoltare,
                            CodParcela = r.CodParcela,
                            CantitateRecoltata = r.CantitateRecoltata,
                            DataRecoltare = r.DataRecoltare
                        }).ToList(), sortOrder);
                        break;
                    case "RegistruTratamente":
                        raportAngajati.RegistruTratamente = SortData(angajat.RegistruTratamente.Select(r => new RegistruTratamenteDate
                        {
                            CodTratamentAplicat = r.CodTratamentAplicat,
                            CodParcela = r.CodParcela,
                            CodDaunator = r.CodDaunator,
                            Suprafata = r.Suprafata,
                            DataAplicare = r.DataAplicare
                        }).ToList(), sortOrder);
                        break;
                }

                return View(new List<RaportAngajati> { raportAngajati });
            }

            return View(new List<RaportAngajati>());
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
