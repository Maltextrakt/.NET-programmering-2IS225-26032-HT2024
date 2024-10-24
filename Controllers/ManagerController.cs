using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;
using Miljoboven.Models.ViewModels;

namespace Miljoboven.Controllers
{
    [Authorize(Roles = "Manager")]
	public class ManagerController : Controller
	{
        // Fält för att lagra referensen till IErrandRepository, används för att hämta och manipulera ärenden
        private readonly IErrandRepository errandRepository;
        // lagra referense till IHttpContextAccessor, används för att hämta data om inloggade användare
        private readonly IHttpContextAccessor contextAcc;

        // Konstruktor för att dependency injecta IErrandRepository och IHttpContextAccessor
        public ManagerController(IErrandRepository errandRepository, IHttpContextAccessor httpContextAccessor)
		{
			this.errandRepository = errandRepository;
            contextAcc = httpContextAccessor;
		}

        // Visar detaljer för ett specifikt ärende för en manager baserat på ärende-ID
        public IActionResult CrimeManager(int id)
		{
            var employeeId = contextAcc.HttpContext.User.Identity.Name;
            var manager = errandRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (manager == null)
            {
                return BadRequest("Invalid Errand ID");
            }

            var errand = errandRepository.GetErrandById(id);
            if (errand == null)
            {
                return NotFound();
            }

            var investigators = errandRepository.GetDepartmentInvestigators(manager.DepartmentId);
            var statuses = errandRepository.Statuses.ToList();

            var viewModel = new ManagerViewModel
            {
                Errand = errand,
                Investigators = investigators,
                Statuses = statuses,
                Departments = errandRepository.Departments,
                ErrandId = errand.ErrandId
            };

            return View(viewModel);
        }


        //Visar startvyn för en manager med en lista över alla errands
        public IActionResult StartManager(string departmentId, string employeeName, string statusId, string refnumber)
		{
            var employeeId = contextAcc.HttpContext.User.Identity.Name;

            var manager = errandRepository.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (manager == null)
            {
                return Unauthorized(); // se till så att managern existerar
            }

            var errands = errandRepository.GetManagerErrands(manager.DepartmentId, employeeName, statusId, refnumber); // Hämta errands för managerns department
            var statuses = errandRepository.Statuses.ToList();
            var investigators = errandRepository.GetDepartmentInvestigators(manager.DepartmentId).ToList(); // visa bara investigators med samma department som managern

            var viewModel = new ManagerViewModel
            {
                Errands = errands,
                Statuses = statuses,
                Investigators = investigators,
                Departments = errandRepository.Departments // Assuming you might need departments
            };

            return View(viewModel); // Pass the view model to the view
        } 


        public IActionResult HandleErrand(int errandId, bool noAction, string reason, string? employeeId)
        {

            if (noAction)
            {
                if (string.IsNullOrEmpty(reason))
                {
                    ModelState.AddModelError("", "Ange en motivering till varför ärendet inte ska utredas.");
                    return RedirectToAction("CrimeManager", new { id = errandId });
                }

                errandRepository.SetNoAction(errandId, reason);
            }
            else if (!string.IsNullOrEmpty(employeeId))
            {
                errandRepository.AssignInvestigator(errandId, employeeId);
            }
            else
            {
                // om varken checkboxen är iklickad eller en employee är vald, returnera errormeddelandet
                ModelState.AddModelError("", "Ingen åtgärd är vald och ingen handläggare är tilldelad.");
                return RedirectToAction("CrimeManager", new { id = errandId });
            }

            
            return RedirectToAction("CrimeManager", new { id = errandId });
        }
        
	}
}
