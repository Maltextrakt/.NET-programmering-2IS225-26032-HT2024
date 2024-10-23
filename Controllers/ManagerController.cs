using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class ManagerController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera ärenden
        private readonly IErrandRepository errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public ManagerController(IErrandRepository errandRepository)
		{
			this.errandRepository = errandRepository;
		}

        // Visar detaljer för ett specifikt ärende för en manager baserat på ärende-ID
        public IActionResult CrimeManager(int id)
		{

            if (id < 0)
            {
                return BadRequest("Invalid Errand ID.");
            }

            ViewBag.ErrandId = id;

            return View(errandRepository.Employees);
        }

        //Visar startvyn för en manager med en lista över alla errands
        public ViewResult StartManager()
		{

            return View(errandRepository);
        } 

        public IActionResult HandleErrand(int errandId, bool noAction, string reason, string? employeeId)
        {
            var errand = errandRepository.GetErrandById(errandId);

            if (errand == null)
            {
                return NotFound(); // om inget errand hittas
            }

            if (noAction)
            {
                if (string.IsNullOrEmpty(reason))
                {
                    ModelState.AddModelError("", "Ange en motivering till varför ärendet inte ska utredas.");
                    return RedirectToAction("CrimeManager", new { id = errandId });
                }

                errand.StatusId = "S_B"; // sätt statusId till S_B för ingen åtgärd
                errand.InvestigatorInfo = reason;
                errand.EmployeeId = null; // ingen utredare tillsatt
            }
            else if (employeeId != null)
            {
                errand.EmployeeId = employeeId;
                errand.StatusId = "S_A";
                errand.InvestigatorInfo = "";
            }
            else
            {
                // om varken checkboxen är iklickad eller en employee är vald, returnera errormeddelandet
                ModelState.AddModelError("", "Ingen åtgärd är vald och ingen handläggare är tilldelad.");
                return RedirectToAction("CrimeManager", new { id = errandId });
            }

            errandRepository.SaveErrand(errand);
            return RedirectToAction("CrimeManager", new { id = errandId });
        }
        
	}
}
