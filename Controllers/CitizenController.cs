using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Models.POCO;
using Miljoboven.Infrastructure;

namespace Miljoboven.Controllers
{
    public class CitizenController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public CitizenController(IErrandRepository errandRepository)
        {
            this.errandRepository = errandRepository;
        }

        public ViewResult Faq()
		{
			return View();
		}

		public ViewResult Contact()
		{
			return View();
		}

		public ViewResult Services()
		{
			return View();

		}

        public ViewResult Thanks()
        {
            // Återskapa errandet från sessionsdatan
            var errandToSave = HttpContext.Session.Get<Errand>("CitizenErrand");


            if(errandToSave != null)
            {
                // spara alla nya errand status IDs som S_A
                errandToSave.StatusId = "S_A";

                //spara ärendet i databasen
                errandRepository.SaveErrand(errandToSave);

                ViewBag.RefNumber = errandToSave.RefNumber;
                //stäng ner sessionen
                HttpContext.Session.Clear();
            }
            

            return View();
        }



        // Metod för att validera formulärdata från användaren
        // Endast POST-begäran tillåts eftersom det handlar om att skicka data från ett formulär
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            // Kontrollera om modellen (Errand) inte är giltig baserat på de valideringsregler som finns
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index", errand);
            }

            HttpContext.Session.Set("CitizenErrand", errand);
            return View("Validate", errand);
        }

                    
    }
}
