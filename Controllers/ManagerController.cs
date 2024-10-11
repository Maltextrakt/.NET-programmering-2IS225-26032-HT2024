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
        public IActionResult CrimeManager(string id)
		{

            if (string.IsNullOrEmpty(id))
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
	}
}
