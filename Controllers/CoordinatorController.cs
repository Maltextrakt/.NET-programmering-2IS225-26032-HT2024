using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Models.POCO;
using Miljoboven.Infrastructure;

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
            var errand = HttpContext.Session.Get<Errand>("CoordinatorErrand");

            if (errand == null)
            { errand = new Errand
            {
                DateOfObservation = DateTime.Today
            };
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

            HttpContext.Session.Set("CoordinatorErrand", errand);

            return View(errand);
        }

        public ViewResult Thanks()
        {
            // Återskapa errandet från sessionsdatan
            var errandToSave = HttpContext.Session.Get<Errand>("CoordinatorErrand");


            if (errandToSave != null)
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

        // låter en coordinator assigna departments till ett ärende
        public IActionResult AssignDepartment(string departmentId, int errandId)
        {
            if (departmentId == null)
            {
                ModelState.AddModelError("", "Ingen avdelning är vald.");
                return RedirectToAction("CrimeCoordinator", new { id = errandId });
            }

            var errand = errandRepository.GetErrandById(errandId);

            errand.DepartmentId = departmentId;
            errandRepository.SaveErrand(errand);

            return RedirectToAction("StartCoordinator", new {id = errandId});
        }
	}
}
