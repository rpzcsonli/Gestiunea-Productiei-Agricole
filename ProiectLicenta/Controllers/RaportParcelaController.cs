using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using System.Linq;
using System.Collections.Generic;

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
        public IActionResult Index(int? parcelaSelectata)
        {
            var parcele = context.Parcela.ToList();
            ViewBag.Parcele = parcele.Select(p => new SelectListItem
            {
                Value = p.CodParcela.ToString(),
                Text = p.Locatie,
                Selected = p.CodParcela == parcelaSelectata
            }).ToList();

            if (parcelaSelectata.HasValue && parcelaSelectata.Value != 0)
            {
                var reportData = context.RegistruRecoltare
                    .Where(r => r.CodParcela == parcelaSelectata.Value)
                    .Select(r => new
                    {
                        r.CodRecoltare,
                        r.CantitateRecoltata,
                        r.DataRecoltare,
                        Parcela = r.Parcela.Locatie
                    }).ToList();

                return View(reportData);
            }

            return View(new List<dynamic>());
        }
    }
}
