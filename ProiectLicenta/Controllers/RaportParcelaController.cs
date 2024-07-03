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
    public class RaportParcelaController : Controller
    {
        private readonly ApplicationDbContext context;

        public RaportParcelaController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index(int? parcelaSelectata, string registruSelectat, string sortOrder, bool filtrareData, DateTime dataInceput, DateTime dataSfarsit)
        {
            ViewBag.ParcelaSelectata = parcelaSelectata;
            ViewBag.RegistruSelectat = registruSelectat;
            ViewBag.SortOrder = sortOrder;
            ViewBag.FiltrareData = filtrareData;
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
            if (parcelaSelectata.HasValue && parcelaSelectata.Value != 0 && !string.IsNullOrEmpty(registruSelectat))
            {
                var parcela = context.Parcela.FirstOrDefault(p => p.CodParcela == parcelaSelectata.Value);
                var raportParcele = new RaportParcele
                {
                    CodParcela = parcela.CodParcela,
                    Locatie = parcela.Locatie,
                    Tip = parcela.Tip,
                    Suprafata = parcela.Suprafata,
                    NumarPlante = parcela.NumarPlante,
                    filtrareData = filtrareData,
                    DataInceput = dataInceput,
                    DataSfarsit = dataSfarsit,
                    RegistruCopilire = new List<RegistruCopilire>(),
                    RegistruFertilizare = new List<RegistruFertilizare>(),
                    RegistruIrigare = new List<RegistruIrigare>(),
                    RegistruPalisare = new List<RegistruPalisare>(),
                    RegistruRecoltare = new List<RegistruRecoltare>(),
                    RegistruTratamente = new List<RegistruTratamente>()
                };
                if (filtrareData == false)
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportParcele.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportParcele.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportParcele.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportParcele.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportParcele.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportParcele.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodParcela == parcelaSelectata.Value).ToList(), sortOrder));
                            break;
                    }
                }
                else
                {
                    switch (registruSelectat)
                    {
                        case "RegistruCopilire":
                            raportParcele.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataCopilire >= dataInceput && p.DataCopilire <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruFertilizare":
                            raportParcele.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataFertilizare >= dataInceput && p.DataFertilizare <= dataSfarsit).ToList(), sortOrder));
                            break;
                        case "RegistruIrigare":
                            raportParcele.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataIrigare >= dataInceput && p.DataIrigare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruPalisare":
                            raportParcele.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataPalisare >= dataInceput && p.DataPalisare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruRecoltare":
                            raportParcele.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataRecoltare >= dataInceput && p.DataRecoltare <= dataSfarsit).ToList(), sortOrder));

                            break;
                        case "RegistruTratamente":
                            raportParcele.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodParcela == parcelaSelectata.Value && p.DataAplicare >= dataInceput && p.DataAplicare <= dataSfarsit).ToList(), sortOrder));
                            break;
                    }
                }
                return View(raportParcele);
            }
            return View(new RaportParcele { DataInceput = DateTime.Now, DataSfarsit = DateTime.Now });
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
