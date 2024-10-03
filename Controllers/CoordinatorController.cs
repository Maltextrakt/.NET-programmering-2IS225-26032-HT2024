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

		public ViewResult StartCoordinator()
        {
            var errands = _errandRepository.GetErrands();
            return View(errands);
        }

        public IActionResult CrimeCoordinator(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Errand ID.");
            }

            var errand = _errandRepository.GetErrandById(id);

            if (errand == null)
            {
                return NotFound("Errand not found.");
            }

            return View(errand);
        }

        public ViewResult ReportCrime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(Errand errand)
        {
            if (!ModelState.IsValid)
            {
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
