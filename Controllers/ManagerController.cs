using Microsoft.AspNetCore.Mvc;
using Miljoboven.Models;

namespace Miljoboven.Controllers
{
	public class ManagerController : Controller
	{
        private readonly IErrandRepository _errandRepository;

		public ManagerController(IErrandRepository errandRepository)
		{
			_errandRepository = errandRepository;
		}

		public ViewResult StartCoordinator()
		{
			var errands = _errandRepository.GetErrands();
			return View(errands);	
		}

        public ViewResult CrimeCoordinator(string id)
        {
            var errand = _errandRepository.GetErrandById(id);
            return View(errand);
        }

        public ViewResult CrimeManager(string id)
		{
			var errand = _errandRepository.GetErrandById(id);
			return View(errand);
		}

		public ViewResult StartManager(string id)
		{
			var errands = _errandRepository.GetErrands();
			return View(errands);
		} 
	}
}
