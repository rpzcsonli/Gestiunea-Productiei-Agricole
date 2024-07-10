using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    public class SarciniController : Controller
    {
        private readonly ApplicationDbContext context;

        public SarciniController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index(int? angajatSelectat, string sortOrder, bool filtrareData, DateTime dataInceput, DateTime dataSfarsit, int? id, string? registru)
        {
            ViewBag.AngajatSelectata = angajatSelectat;
            ViewBag.SortOrder = sortOrder;
            ViewBag.FiltrareData = filtrareData;
            List<Angajat> angajati = context.Angajat.ToList();
            ViewBag.Angajati = angajati.Select(p => new SelectListItem
            {
                Value = p.CodAngajat.ToString(),
                Text = p.Nume + " " + p.Prenume,
                Selected = p.CodAngajat == angajatSelectat
            }).ToList();
            if (angajatSelectat.HasValue && angajatSelectat.Value != 0)
            {
                var angajat = context.Angajat.FirstOrDefault(p => p.CodAngajat == angajatSelectat.Value);
                var raportAngajati = new RaportAngajati
                {
                    CodAngajat = angajat.CodAngajat,
                    Nume = angajat.Nume,
                    Prenume = angajat.Prenume,
                    Functie = angajat.Functie,
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
                if (id.HasValue)
                {
                    switch (registru)
                    {
                        case "RegistruCopilire":
                            var copilire = context.RegistruCopilire.Find(id);
                            if (copilire != null)
                            {
                                copilire.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                        case "RegistruFertilizare":
                            var fertilizare = context.RegistruFertilizare.Find(id);
                            if (fertilizare != null)
                            {
                                fertilizare.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                        case "RegistruIrigare":
                            var irigare = context.RegistruIrigare.Find(id);
                            if (irigare != null)
                            {
                                irigare.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                        case "RegistruPalisare":
                            var palisare = context.RegistruPalisare.Find(id);
                            if (palisare != null)
                            {
                                palisare.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                        case "RegistruRecoltare":
                            var recoltare = context.RegistruRecoltare.Find(id);
                            if (recoltare != null)
                            {
                                recoltare.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                        case "RegistruTratamente":
                            var tratament = context.RegistruTratamente.Find(id);
                            if (tratament != null)
                            {
                                tratament.Stare = true;
                                context.SaveChanges();
                            }
                            break;
                    }
                }
                if (filtrareData == true)
                {
                    
                            raportAngajati.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataCopilire >= dataInceput && p.DataCopilire <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataFertilizare >= dataInceput && p.DataFertilizare <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataIrigare >= dataInceput && p.DataIrigare <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataPalisare >= dataInceput && p.DataPalisare <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataRecoltare >= dataInceput && p.DataRecoltare <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodAngajat == angajatSelectat.Value && p.DataAplicare >= dataInceput && p.DataAplicare <= dataSfarsit && p.Stare == false).ToList(), sortOrder));
                    
                }
                else
                {
                   
                            raportAngajati.RegistruCopilire.AddRange(SortData(context.RegistruCopilire.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruFertilizare.AddRange(SortData(context.RegistruFertilizare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruIrigare.AddRange(SortData(context.RegistruIrigare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruPalisare.AddRange(SortData(context.RegistruPalisare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruRecoltare.AddRange(SortData(context.RegistruRecoltare.Include(r => r.Parcela).Include(r => r.Angajat).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));
                            raportAngajati.RegistruTratamente.AddRange(SortData(context.RegistruTratamente.Include(r => r.Parcela).Include(r => r.Angajat).Include(r => r.Daunatori).ThenInclude(r => r.Tratament).Where(p => p.CodAngajat == angajatSelectat.Value && p.Stare == false).ToList(), sortOrder));

                }

                return View(raportAngajati);
            }
            return View(new RaportAngajati { DataInceput = DateTime.Now, DataSfarsit = DateTime.Now });
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

