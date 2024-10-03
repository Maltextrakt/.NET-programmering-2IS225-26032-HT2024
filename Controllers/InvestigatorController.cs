using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class InvestigatorController : Controller
	{
		private readonly IErrandRepository _errandRepository;

		public InvestigatorController(IErrandRepository errandRepository)
		{
			_errandRepository = errandRepository;
		}

		public IActionResult StartInvestigator()
		{
			var errands = _errandRepository.GetErrands();
			return View(errands);
		}

		public IActionResult CrimeInvestigator(string id)
		{
			var errand = _errandRepository.GetErrandById(id);
			return View(errand);
		}
	}
}
