using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Models.POCO;

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
            var errandToSave = new Errand
            {
                Place = HttpContext.Session.GetString("Place"),
                TypeOfCrime = HttpContext.Session.GetString("TypeOfCrime"),
                InformerName = HttpContext.Session.GetString("InformerName"),
                InformerPhone = HttpContext.Session.GetString("InformerPhone"),
                Observation = HttpContext.Session.GetString("Observation"),
                DateOfObservation = DateTime.Parse(HttpContext.Session.GetString("DateOfObservation")),
                StatusId = "S_A" 
            };

            
            errandRepository.SaveErrand(errandToSave);

            ViewBag.RefNumber = errandToSave.RefNumber;

            HttpContext.Session.Clear();

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

            HttpContext.Session.SetString("Place", errand.Place);
            HttpContext.Session.SetString("TypeOfCrime", errand.TypeOfCrime);
            HttpContext.Session.SetString("InformerName", errand.InformerName);
            HttpContext.Session.SetString("InformerPhone", errand.InformerPhone);
            HttpContext.Session.SetString("Observation", errand.Observation);

            
            HttpContext.Session.SetString("DateOfObservation", errand.DateOfObservation.ToString("o"));

            return View("Validate", errand);
        }

                    
    }
}
