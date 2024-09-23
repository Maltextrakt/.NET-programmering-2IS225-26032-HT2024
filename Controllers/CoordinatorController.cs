using Microsoft.AspNetCore.Mvc;

namespace Miljoboven.Controllers
{
    public class CoordinatorController : Controller
    {
        public ViewResult StartCoordinator() 
        { 
            return View(); 
        }

		public ViewResult CrimeCoordinator()
		{
			return View();
		}

        public ViewResult ReportCrime()
        {
            return View();
        }

		public ViewResult Validate()
		{
			return View();
		}

        public ViewResult Thanks()
        {
            return View();
        }
	}
}
