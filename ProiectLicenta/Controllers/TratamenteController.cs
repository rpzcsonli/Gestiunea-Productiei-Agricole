using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<Tratament> dateTratament = SortData(context.Tratament.Where(a => a.Denumire.Contains(searchString) || a.Cantitate == index || a.Perioada == index).ToList(), sortOrder);
                    return View(dateTratament);
                }
                else
                {
                    List<Tratament> dateTratament = SortData(context.Tratament.Where(a => a.Denumire.Contains(searchString)).ToList(), sortOrder);
                    return View(dateTratament);
                }
            }
            List<Tratament> tratament = SortData(context.Tratament.ToList(), sortOrder);
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
        public IActionResult Stergere(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int index))
                {
                    List<Tratament> dateTratament = context.Tratament.Where(a => a.Denumire.Contains(searchString) || a.Cantitate == index || a.Perioada == index).ToList();
                    return View(dateTratament);
                }
                else
                {
                    List<Tratament> dateTratament = context.Tratament.Where(a => a.Denumire.Contains(searchString)).ToList();
                    return View(dateTratament);
                }
            }
            List<Tratament> tratament = context.Tratament.ToList();
            return View(tratament);
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
        [HttpGet]
        public IActionResult Editare(int id)
        {
            var tratament = context.Tratament.Find(id);
            if (tratament == null)
            {
                return NotFound();
            }

            var editTratamentViewModel = new AddTratamentViewModel
            {
                CodTratament = tratament.CodTratament,
                Denumire = tratament.Denumire,
                Cantitate = tratament.Cantitate,
                Perioada = tratament.Perioada
            };

            return View(editTratamentViewModel);
        }
        [HttpPost]
        public IActionResult Editare(AddTratamentViewModel editTratamentViewModel)
        {
            if (ModelState.IsValid)
            {
                var tratament = context.Tratament.Find(editTratamentViewModel.CodTratament);
                if (tratament == null)
                {
                    return NotFound();
                }

                tratament.Denumire = editTratamentViewModel.Denumire;
                tratament.Cantitate = editTratamentViewModel.Cantitate;
                tratament.Perioada = editTratamentViewModel.Perioada;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
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
