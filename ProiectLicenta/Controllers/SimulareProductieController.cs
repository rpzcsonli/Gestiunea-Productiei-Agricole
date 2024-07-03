using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class SimulareProductieController : Controller
    {
        private readonly ApplicationDbContext context;

        public SimulareProductieController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Parcela> parcele = context.Parcela.ToList();
            ViewBag.Parcele = parcele.Select(p => new SelectListItem
            {
                Value = p.CodParcela.ToString(),
                Text = p.Locatie
            }).ToList();
            return View(new DateRecoltareViewModel{dataInceput = DateTime.Today, dataSfarsit = DateTime.Today});
        }

        [HttpPost]
        public IActionResult Index(int parcelaSelectata, DateTime dataInceput, DateTime dataSfarsit)
        {
            List<Parcela> Dateparcele = context.Parcela.ToList();
            ViewBag.Parcele = Dateparcele.Select(p => new SelectListItem
            {
                Value = p.CodParcela.ToString(),
                Text = p.Locatie,
                Selected = p.CodParcela == parcelaSelectata
            }).ToList();
            var recoltareData = context.RegistruRecoltare
                .Include(r => r.Parcela)
                .Where(p => p.CodParcela == parcelaSelectata && p.DataRecoltare >= dataInceput && p.DataRecoltare <= dataSfarsit).OrderBy(p=>p.DataRecoltare)
                .ToList();
            var viewModel = new DateRecoltareViewModel
            {
                RecoltareData = recoltareData,
                parcelaSelectata = parcelaSelectata,
                dataInceput = dataInceput,
                dataSfarsit = dataSfarsit,
            };

            return View(viewModel);
        }
      
        
    }
}
