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
        public IActionResult Index(int? angajatSelectat, string registruSelectat)
        {
            List<Angajat> angajati = context.Angajat.ToList();
            ViewBag.Angajati = angajati.Select(p => new SelectListItem
            {
                Value = p.CodAngajat.ToString(),
                Text = p.Nume + " " + p.Prenume,
                Selected = p.CodAngajat == angajatSelectat
            }).ToList();

            ViewBag.Registers = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "RegistruCopilire", Text = "Registru Copilire" },
                new SelectListItem { Value = "RegistruFertilizare", Text = "Registru Fertilizare" },
                new SelectListItem { Value = "RegistruIrigare", Text = "Registru Irigare" },
                new SelectListItem { Value = "RegistruPalisare", Text = "Registru Palisare" },
                new SelectListItem { Value = "RegistruRecoltare", Text = "Registru Recoltare" },
                new SelectListItem { Value = "RegistruTratamente", Text = "Registru Tratamente" }
            }, "Value", "Text", registruSelectat);

            if (angajatSelectat.HasValue && angajatSelectat.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                List<RaportAngajati> reportData = context.Angajat
                    .Where(p => p.CodAngajat == angajatSelectat.Value)
                    .Select(p => new RaportAngajati
                    {
                        CodAngajat = p.CodAngajat,
                        Nume = p.Nume,
                        Prenume = p.Prenume,
                        Functie = p.Functie,
                        RegistruCopilire = registruSelectat == "RegistruCopilire" ? p.RegistruCopilire.Select(r => new RegistruCopilireDate
                        {
                            CodCopilire = r.CodCopilire,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataCopilire = r.DataCopilire
                        }).ToList() : null,
                        RegistruFertilizare = registruSelectat == "RegistruFertilizare" ? p.RegistruFertilizare.Select(r => new RegistruFertilizareDate
                        {
                            CodFertilizare = r.CodFertilizare,
                            CodParcela = r.CodParcela,
                            Suprafata = r.Suprafata,
                            DataFertilizare = r.DataFertilizare
                        }).ToList() : null,
                        RegistruIrigare = registruSelectat == "RegistruIrigare" ? p.RegistruIrigare.Select(r => new RegistruIrigareDate
                        {
                            CodIrigare = r.CodIrigare,
                            CodParcela = r.CodParcela,
                            DurataIrigare = r.DurataIrigare,
                            DataIrigare = r.DataIrigare
                        }).ToList() : null,
                        RegistruPalisare = registruSelectat == "RegistruPalisare" ? p.RegistruPalisare.Select(r => new RegistruPalisareDate
                        {
                            CodPalisare = r.CodPalisare,
                            CodParcela = r.CodParcela,
                            NumarPlante = r.NumarPlante,
                            DataPalisare = r.DataPalisare
                        }).ToList() : null,
                        RegistruRecoltare = registruSelectat == "RegistruRecoltare" ? p.RegistruRecoltare.Select(r => new RegistruRecoltareDate
                        {
                            CodRecoltare = r.CodRecoltare,
                            CodParcela = r.CodParcela,
                            CantitateRecoltata = r.CantitateRecoltata,
                            DataRecoltare = r.DataRecoltare
                        }).ToList() : null,
                        RegistruTratamente = registruSelectat == "RegistruTratamente" ? p.RegistruTratamente.Select(r => new RegistruTratamenteDate
                        {
                            CodTratamentAplicat = r.CodTratamentAplicat,
                            CodParcela = r.CodParcela,
                            CodDaunator = r.CodDaunator,
                            Suprafata = r.Suprafata,
                            DataAplicare = r.DataAplicare
                        }).ToList() : null
                    }).ToList();

                return View(reportData);
            }

            return View(new List<RaportAngajati>());
        }
    }
}
