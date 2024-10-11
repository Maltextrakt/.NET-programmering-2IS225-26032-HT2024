using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

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
        public IActionResult CrimeCoordinator(string id)
        {
            // Om ärende-ID inte anges eller är null returneras ett felmeddelande BadRequest
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Errand ID.");
            }

            ViewBag.ErrandId = id;  

            return View(errandRepository.Departments);  
        }

        public ViewResult ReportCrime()
        {
            return View();
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

            return View(errand);
        }

        public ViewResult Thanks()
        {
            return View();
        }
	}
}
