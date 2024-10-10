using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class ManagerController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera ärenden
        private readonly IErrandRepository _errandRepository;

        // Konstruktor för att dependency injecta IErrandRepository
        public ManagerController(IErrandRepository errandRepository)
		{
			_errandRepository = errandRepository;
		}

        // Visar detaljer för ett specifikt ärende för en manager baserat på ärende-ID
        public ViewResult CrimeManager(string id)
		{
			var errand = _errandRepository.GetErrandById(id);

            ViewBag.Employees = _errandRepository.GetEmployees();

            return View(errand);
		}

        //Visar startvyn för en manager med en lista över alla errands
        public ViewResult StartManager(string id)
		{
            // Hämtar alla ärenden från repositoryt och skickar dem till vyn
            var errands = _errandRepository.GetErrands();

            ViewBag.Statuses = _errandRepository.GetErrandStatuses();
            ViewBag.Departments = _errandRepository.GetDepartments();
            ViewBag.Employees = _errandRepository.GetEmployees();

            return View(errands);
		} 
	}
}
