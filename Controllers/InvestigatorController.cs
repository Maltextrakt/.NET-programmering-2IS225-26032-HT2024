using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class InvestigatorController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public InvestigatorController(IErrandRepository errandRepository)
		{
			this.errandRepository = errandRepository;
		}

		public IActionResult StartInvestigator()
		{
            // Hämtar alla ärenden från repositoryt och skickar dem till vyn
            
            return View(errandRepository);
		}

        // Visar detaljer för ett specifikt ärende baserat på dess ID
        public IActionResult CrimeInvestigator(string id)
		{

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Errand ID.");
            }

            ViewBag.ErrandId = id; 

            return View(errandRepository.Statuses);
        }
	}
}
