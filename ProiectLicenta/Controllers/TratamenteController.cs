using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Index()
        {
            List<Tratament> tratament = context.Tratament.ToList();
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
            return View(addTratamentViewModel);
        }
        public IActionResult Stergere()
        {
            ViewBag.events = context.Tratament.ToList();
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
    }
}
