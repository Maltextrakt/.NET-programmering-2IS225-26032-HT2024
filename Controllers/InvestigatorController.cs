using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class InvestigatorController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera errands
        private readonly IErrandRepository _errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public InvestigatorController(IErrandRepository errandRepository)
		{
			_errandRepository = errandRepository;
		}

		public IActionResult StartInvestigator()
		{
            // Hämtar alla ärenden från repositoryt och skickar dem till vyn
            var errands = _errandRepository.GetErrands();

            ViewBag.Statuses = _errandRepository.GetErrandStatuses();
            ViewBag.Departments = _errandRepository.GetDepartments();
            ViewBag.Employees = _errandRepository.GetEmployees();

            return View(errands);
		}

        // Visar detaljer för ett specifikt ärende baserat på dess ID
        public IActionResult CrimeInvestigator(string id)
		{
            // Hämtr ett specifikt ärende baserat på ID
            var errand = _errandRepository.GetErrandById(id);

            ViewBag.Statuses = _errandRepository.GetErrandStatuses();

            //visar ärendet i vyn
            return View(errand);
		}
	}
}
