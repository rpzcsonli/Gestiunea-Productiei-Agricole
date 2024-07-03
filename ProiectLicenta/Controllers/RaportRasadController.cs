using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
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
        public IActionResult Index(int? rasadSelectat, string registruSelectat, string sortOrder, bool filtrareData, DateTime dataInceput, DateTime dataSfarsit)
        {
            ViewBag.RasadSelectata = rasadSelectat;
            ViewBag.RegistruSelectat = registruSelectat;
            ViewBag.SortOrder = sortOrder;
            ViewBag.FiltrareData = filtrareData;
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

            if (rasadSelectat.HasValue && rasadSelectat.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var rasad = context.Rasad.First(r => r.CodRasad == rasadSelectat.Value);
                var parcele = context.Parcela.Where(p => p.CodRasad == rasadSelectat.Value)
                    .ToList();
                var raportData = new RaportRasad
                {
                    CodRasad = rasadSelectat.Value,
                    Denumire = rasad.Denumire,
                    Planta = rasad.Planta,
                    Cantitate = rasad.Cantitate,
                    filtrareData = filtrareData,
                    DataInceput = dataInceput,
                    DataSfarsit = dataSfarsit,
                    Parcele = new List<Parcela>(),
                    RegistruCopilire = new List<RegistruCopilire>(),
                    RegistruFertilizare = new List<RegistruFertilizare>(),
                    RegistruIrigare = new List<RegistruIrigare>(),
                    RegistruPalisare = new List<RegistruPalisare>(),
                    RegistruRecoltare = new List<RegistruRecoltare>(),
                    RegistruTratamente = new List<RegistruTratamente>()
                };
                raportData.Parcele.AddRange(parcele);
                if (filtrareData == true)
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportData.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataCopilire >= dataInceput && p.DataCopilire <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportData.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataFertilizare >= dataInceput && p.DataFertilizare <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportData.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataIrigare >= dataInceput && p.DataIrigare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportData.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataPalisare >= dataInceput && p.DataPalisare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportData.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataRecoltare >= dataInceput && p.DataRecoltare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportData.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).Where(p => p.Parcela.CodRasad == rasadSelectat.Value && p.DataAplicare >= dataInceput && p.DataAplicare <= dataSfarsit).ToList(), sortOrder));
                            break;
                    }
                }
                else
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportData.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportData.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportData.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportData.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportData.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportData.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).Where(p => p.Parcela.CodRasad == rasadSelectat.Value).ToList(), sortOrder));
                            break;
                    }
                }
                return View(raportData);
            }

            return View(new RaportRasad{DataInceput = DateTime.Now, DataSfarsit = DateTime.Now});
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
