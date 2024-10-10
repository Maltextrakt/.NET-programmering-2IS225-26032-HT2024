using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository _errandRepository;
        // Konstruktor för att dependency injecta IErrandRepository
        public CoordinatorController(IErrandRepository errandRepository)
		{
			_errandRepository = errandRepository;
		}

        // Visar startvyn för samordnare med en lista över alla ärenden
        public ViewResult StartCoordinator()
        {
            // Hämtar alla ärenden från repositoryt och skickar dem till vyn
            var errands = _errandRepository.GetErrands();

            ViewBag.Statuses = _errandRepository.GetErrandStatuses();


			return View(errands);
        }

        // Visar detaljer för ett specifikt errand baserat på dess ID
        public IActionResult CrimeCoordinator(string id)
        {
            // Om ärende-ID inte anges eller är null returneras ett felmeddelande BadRequest
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Errand ID.");
            }

            var errand = _errandRepository.GetErrandById(id);

            if (errand == null)
            {
                return NotFound("Errand not found.");
            }

            // hämta datan dynamiskt
            ViewBag.Statuses = _errandRepository.GetErrandStatuses();
            ViewBag.Employees = _errandRepository.GetEmployees();
            ViewBag.Departments = _errandRepository.GetDepartments();

            return View(errand);
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
