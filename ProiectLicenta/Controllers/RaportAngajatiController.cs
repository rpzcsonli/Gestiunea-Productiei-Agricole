using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

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
        public IActionResult Index(int? angajatSelectat, string registruSelectat, string sortOrder, bool filtrareData, DateTime dataInceput, DateTime dataSfarsit)
        {
            ViewBag.AngajatSelectata = angajatSelectat;
            ViewBag.RegistruSelectat = registruSelectat;
            ViewBag.SortOrder = sortOrder;
            ViewBag.FiltrareData = filtrareData;
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
            if (angajatSelectat.HasValue && angajatSelectat.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var angajat = context.Angajat.FirstOrDefault(p => p.CodAngajat == angajatSelectat.Value);
                var raportAngajati = new RaportAngajati
                {
                    CodAngajat = angajat.CodAngajat,
                    Nume = angajat.Nume,
                    Prenume = angajat.Prenume,
                    Functie = angajat.Functie,
                    filtrareData=filtrareData,
                    DataInceput = dataInceput,
                    DataSfarsit = dataSfarsit,
                    RegistruCopilire = new List<RegistruCopilire>(),
                    RegistruFertilizare = new List<RegistruFertilizare>(),
                    RegistruIrigare = new List<RegistruIrigare>(),
                    RegistruPalisare = new List<RegistruPalisare>(),
                    RegistruRecoltare = new List<RegistruRecoltare>(),
                    RegistruTratamente = new List<RegistruTratamente>()
                };
                if (filtrareData == true)
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportAngajati.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataCopilire >= dataInceput && p.DataCopilire <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportAngajati.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataFertilizare >= dataInceput && p.DataFertilizare <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportAngajati.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataIrigare >= dataInceput && p.DataIrigare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportAngajati.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataPalisare >= dataInceput && p.DataPalisare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportAngajati.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataRecoltare >= dataInceput && p.DataRecoltare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportAngajati.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataAplicare >= dataInceput && p.DataAplicare <= dataSfarsit).ToList(), sortOrder));
                            break;
                    }
                } else
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportAngajati.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportAngajati.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportAngajati.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportAngajati.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportAngajati.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportAngajati.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodAngajat == angajatSelectat.Value).ToList(), sortOrder));
                            break;
                    }
                }
                return View(raportAngajati);
            }
            return View(new RaportAngajati{DataInceput = DateTime.Now,DataSfarsit=DateTime.Now});
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
