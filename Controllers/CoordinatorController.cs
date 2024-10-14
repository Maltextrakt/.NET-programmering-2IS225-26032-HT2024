using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Models.POCO;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository errandRepository;
        
        // Konstruktor för att dependency injecta IErrandRepository
        public CoordinatorController(IErrandRepository errandRepository)
		{
			this.errandRepository = errandRepository;
		}

        // Visar startvyn för samordnare med en lista över alla ärenden
        public ViewResult StartCoordinator()
        {

			return View(errandRepository);
        }

        // Visar detaljer för ett specifikt errand baserat på dess ID
        public IActionResult CrimeCoordinator(int id)
        {
            // Om ärende-ID inte anges eller är null returneras ett felmeddelande BadRequest
            if (id < 0)
            {
                return BadRequest("Invalid Errand ID.");
            }

            ViewBag.ErrandId = id;  

            return View(errandRepository.Departments);  
        }

        public ViewResult ReportCrime()
        {
            var errand = new Errand();

            errand.Place = HttpContext.Session.GetString("Place");
            errand.TypeOfCrime = HttpContext.Session.GetString("TypeOfCrime");
            errand.InformerName = HttpContext.Session.GetString("InformerName");
            errand.InformerPhone = HttpContext.Session.GetString("InformerPhone");
            errand.Observation = HttpContext.Session.GetString("Observation");

            var dateString = HttpContext.Session.GetString("DateOfObservation");
            if (!string.IsNullOrEmpty(dateString))
            {
                errand.DateOfObservation = DateTime.Parse(dateString);
            }
            else
            {
                errand.DateOfObservation = DateTime.Today;
            }

            return View(errand);
        }

        // Hanterar formulärinlämning och validering av det inrapporterade ärendet
        [HttpPost]
        public IActionResult Validate(Errand errand)
        {
            if (!ModelState.IsValid)
            {
                // Om validering misslyckas, visas ReportCrime-vyn igen med det inskickade ärendet
                return View("ReportCrime", errand);
            }

            HttpContext.Session.SetString("Place", errand.Place);
            HttpContext.Session.SetString("TypeOfCrime", errand.TypeOfCrime);
            HttpContext.Session.SetString("InformerName", errand.InformerName);
            HttpContext.Session.SetString("InformerPhone", errand.InformerPhone);
            HttpContext.Session.SetString("Observation", errand.Observation);

            // For DateTime, serialize it to a string
            HttpContext.Session.SetString("DateOfObservation", errand.DateOfObservation.ToString("o"));


            return View(errand);
        }

        public ViewResult Thanks()
        {
            // Återskapa ärendet från sessionsdatan
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

            //spara errandet i databasen
            errandRepository.SaveErrand(errandToSave);

            ViewBag.RefNumber = errandToSave.RefNumber;

            HttpContext.Session.Clear();

            return View();
        }
	}
}
