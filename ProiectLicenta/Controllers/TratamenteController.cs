﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProiectLicenta.Data;
using ProiectLicenta.Models;
using ProiectLicenta.ViewModels;

namespace ProiectLicenta.Controllers
{
    [Authorize]
    public class TratamenteController : Controller
    {
        private ApplicationDbContext context;
        public TratamenteController(ApplicationDbContext dbcontext)
        {
            this.context = dbcontext;
        }
        [HttpGet]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            List<Tratament> tratament = SortData(context.Tratament.ToList(),sortOrder);
            return View(tratament);
        }

        [HttpGet]
        public IActionResult Adaugare()
        {
            AddTratamentViewModel addTratamentViewModel = new AddTratamentViewModel();
            return View(addTratamentViewModel);
        }
        [HttpPost]
        public IActionResult Adaugare(AddTratamentViewModel addTratamentViewModel)
        {
            if (ModelState.IsValid)
            {
                Tratament newTratament = new Tratament(
                    addTratamentViewModel.CodTratament,
                    addTratamentViewModel.Denumire,
                    addTratamentViewModel.Cantitate,
                    addTratamentViewModel.Perioada
                    
                );

                context.Tratament.Add(newTratament);
                context.SaveChanges();
                return Redirect("/Tratamente");
            }
            return Adaugare();
        }
        public IActionResult Stergere()
        {
            ViewBag.stergere = context.Tratament.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Stergere(int[] tratamentId)
        {
            foreach (int Id in tratamentId)
            {
                Tratament tratament = context.Tratament.Find(Id);
                context.Tratament.Remove(tratament);

            }
            context.SaveChanges();
            return Redirect("/Tratamente");
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
